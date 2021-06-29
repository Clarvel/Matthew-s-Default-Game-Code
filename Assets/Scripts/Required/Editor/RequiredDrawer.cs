using System;
using UnityEngine;
using UnityEditor;
using Boo.Lang;

namespace SoulFlare.RequiredVariables {
	[CustomPropertyDrawer(typeof(Required))]
	public class RequiredDrawer : PropertyDrawer {
		private float textSize = 11f;
		private float margin = 7f;
		private float minSize = 38f;
		//private string errorMessage = "Property is Required";
		private List<string> errors = new List<string>();
		private string cachedGameObjectName = "Null";
		private string cachedGameObjectComponent = "Null";
		private string cachedDisplayName = "Null";
		private SerializedProperty sp;

		public RequiredDrawer() : base() {
			EditorApplication.playModeStateChanged += OnPlayModeStateChage;
		}

		float warningHeight {
			get {
				float height = errors.Count * textSize + margin;
				return Mathf.Max(height, minSize); 
			} 
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			// This is called before OnGUI below, so validateInput here
			// The height for the object assignment is just whatever Unity would do by default.
			sp = property;
			ValidateInput(property);
			return base.GetPropertyHeight(property, label) + (IsValid() ? 0 : warningHeight);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			sp = property;
			ValidateInput(property);
			EditorGUI.BeginProperty(position, label, property);
			if(IsValid()) {
				EditorGUI.PropertyField(position, property, label);
				//errorMessage = "";
			} else {
				//EditorGUI.DrawRect(position, Color.red); // flickers the color
				position.height -= warningHeight;
				EditorGUI.PropertyField(position, property, label);
				position.y += position.height;
				position.height = warningHeight;
				//EditorGUI.HelpBox(position, errorMessage, MessageType.Error);
				EditorGUI.HelpBox(position, errors.Join("\n"), MessageType.Info);
			}
			EditorGUI.EndProperty();
		}

		private void ValidateInput(SerializedProperty serializedProperty) {
			//errorMessage = "";
			//errors.Clear();
			errors = new List<string>();
			Required reqAttr = (Required)attribute;
			GameObjectType got = reqAttr.RequiredType;
			SerializedObject so = serializedProperty.serializedObject;
			
			try { // if script is deleted
				so.Update();
				//so.UpdateIfRequiredOrScript();
			} catch(NullReferenceException) {
				return;
			}
			cachedGameObjectName = so.targetObject.name;
			cachedGameObjectComponent = so.targetObject.GetType().Name;
			cachedDisplayName = serializedProperty.displayName;

			if (serializedProperty.objectReferenceValue == null) {
				//Debug.Log(property.objectReferenceValue);
				//errorMessage = "cannot be Null";
				errors.Add("Cannot be Null");
			}

			ValidateGameObjectType(got, serializedProperty);
			ValidateRequiredComponents(reqAttr, serializedProperty);
			//if (errors.Count > 0)
			//{
				Debug.Log("Validation for [" + cachedGameObjectName + "] script [" + cachedGameObjectComponent + "] value [" + cachedDisplayName + "]: " + errors.Join(", "));
				Debug.Log("SP Name: " + serializedProperty.name);
				if (serializedProperty.objectReferenceValue != null)
				{
					Debug.Log("SP ORV: " + serializedProperty.objectReferenceValue);
					Debug.Log("SP ORV Name: " + serializedProperty.objectReferenceValue.name);
				}
				Debug.Log("SP SO Name: " + serializedProperty.serializedObject.targetObject.name);
				Debug.Log("SP SO Type: " + serializedProperty.serializedObject.targetObject.GetType().Name);

			var targetObject = serializedProperty.serializedObject.targetObject;
			var targetObjectClassType = targetObject.GetType();
			var field = targetObjectClassType.GetField(serializedProperty.propertyPath);
			if (field != null)
			{
				var value = field.GetValue(targetObject);
				Debug.Log(value);
			}

			//}
			/*if(!IsValid()) {
				/*for(int a = 0; a < errors.Count; a++)
				{
					errors[a] = "[" + sp.displayName + "] " + errors[a];
				}
				//errorMessage = "[" + sp.displayName + "] " + errorMessage;
				return false;
			}
			return true*/;
		}

		void ValidateGameObjectType(GameObjectType got, SerializedProperty serializedProperty)
		{
			if (got != GameObjectType.ANY)
			{
				PrefabType pType = serializedProperty.objectReferenceValue == null ? PrefabType.None : PrefabUtility.GetPrefabType(serializedProperty.objectReferenceValue);

				if (pType == PrefabType.None)
				{
					if (got == GameObjectType.PREFAB)
					{
						//errorMessage = "must be a Prefab";
						errors.Add("Must be a Prefab");
					}
					else if(got == GameObjectType.INSTANCE)
					{
						errors.Add("Must be an instance of a Prefab");
					}
				}
				else if (pType == PrefabType.Prefab || pType == PrefabType.ModelPrefab)
				{
					if (got == GameObjectType.SCENE)
					{
						//errorMessage = "must exist in the Scene";
						errors.Add("Must exist in the Scene");
					}
					else if (got == GameObjectType.INSTANCE)
					{
						//errorMessage = "must be an instance of a Prefab";
						errors.Add("Must be an instance of a Prefab");
					}
				}
				else if (got == GameObjectType.PREFAB)
				{
					//errorMessage = "must be an un-instantiated Prefab";
					errors.Add("Must be an un-instantiated Prefab");
				}
			}
		}

		void ValidateRequiredComponents(Required reqAttr, SerializedProperty serializedProperty)
		{
			GameObject go;
			try
			{
				go = (GameObject)serializedProperty.objectReferenceValue;
			}
			catch (InvalidCastException)
			{ // don't care about non gameObjects here
				return;
			}
			Type[] components = reqAttr.RequiredComponents;
			if (components != null && components.Length > 0)
			{
				for (int a = components.Length - 1; a >= 0; a--)
				{
					if (go == null || go.GetComponent(components[a]) == null)
					{
						//errorMessage = "requires component [" + components[a] + "]";
						errors.Add("Requires component [" + components[a] + "]");
						//break;
					}
				}
			}
		}

		bool IsValid() {
			return errors.Count == 0;
			//return errorMessage.Length == 0;
		}

		void OnPlayModeStateChage(PlayModeStateChange state) {
			if (EditorApplication.isPlayingOrWillChangePlaymode)
			{
				ValidateInput(sp);
				if (!IsValid())
				{
					EditorApplication.isPlaying = false;
					//Debug.LogException(new ArgumentException("In GameObject [" + cachedGameObjectName + "], component [" + cachedGameObjectComponent + "] variable " + errorMessage + ". See Inspector."));
					Debug.LogException(new ArgumentException("In GameObject [" + cachedGameObjectName + "], component [" + cachedGameObjectComponent + "] variable [" + cachedDisplayName + "]" + errors.Join(", ") + ". See Inspector."));
				}
			}
		}
	}
}
