using UnityEditor;
using UnityEngine;

public class ExtendedEditor : Editor
{
    SerializedProperty item;

    private void OnEnable()
    {
        item = serializedObject.FindProperty("item");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(item);
        serializedObject.ApplyModifiedProperties();
    }
}
