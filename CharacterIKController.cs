// CharacterIKController.cs
// 9/27/2018
// Matthew russell
using UnityEngine;
using UnityEditor;

public class CharacterIKController : ScriptableObject {
	[MenuItem("Tools/MyTool/Do It in C#")]
	static void DoIt() {
		EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
	}
}
