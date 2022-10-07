using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EditorTile : EditorWindow
{
    Editor gameObjectEditor;
    public static TileEditor tileEditor;
    GameObject tile;

    public static void ShowWindow(TileEditor tileEdit)
    {
        EditorTile editerPage = GetWindow<EditorTile>();
        editerPage.titleContent = new GUIContent("Add a Tile");
        tileEditor = tileEdit;
    }

    void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Pick a Tile", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        tile = EditorGUILayout.ObjectField(tile, typeof(GameObject), false) as GameObject;

        if (tile != null)
        {
            Object.DestroyImmediate(gameObjectEditor);
            gameObjectEditor = Editor.CreateEditor(tile);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(250, 250), EditorStyles.whiteLabel);
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Add"))
        {
            tileEditor.AddToTilePalette(tile);
            this.Close();
        }
    }
}
