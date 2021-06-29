using System;
using UnityEngine;

namespace SoulFlare.RequiredVariables {
	/// <summary>
	/// GameObject type:
	/// ANY:         any non-null GameObject
	/// SCENE:       any GameObject that is in a scene
	/// PREFAB:      any uninstantiated prefab
	/// INSTANCE:    any instantiated prefab
	/// </summary>
	public enum GameObjectType { ANY, SCENE, PREFAB, INSTANCE };

	/// <summary>
	/// Required Attribute.
	/// Usage:
	/// [Required(GameObjectType.INSTANCE)]
	/// [Required(typeof(Text)]
	/// [Required(new Type[]{typeof(Text), typeof(SCRIPT)})]
	/// [Required(GameObjectType.PREFAB, new Type[]{typeof(Text), typeof(SCRIPT)})]
	/// </summary>
	[AttributeUsage(AttributeTargets.Field)]
	public class Required : PropertyAttribute {
		public GameObjectType RequiredType;
		public Type[] RequiredComponents;
		public Type RequiredComponent {
			set {
				RequiredComponents = new Type[] { value };
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		public Required(GameObjectType type) : base() {
			RequiredType = type;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		/// <param name="component">Component.</param>
		public Required(Type component) : base() {
			RequiredComponent = component;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		/// <param name="components">Components.</param>
		public Required(Type[] components) : base() {
			RequiredComponents = components;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="component">Component.</param>
		public Required(GameObjectType type, Type component) : base() {
			RequiredType = type;
			RequiredComponent = component;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		/// <param name="type">Type.</param>
		/// <param name="components">Components.</param>
		public Required(GameObjectType type, Type[] components) : base() {
			RequiredType = type;
			RequiredComponents = components;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Required"/> class.
		/// </summary>
		public Required() : base() { }
	}
}
