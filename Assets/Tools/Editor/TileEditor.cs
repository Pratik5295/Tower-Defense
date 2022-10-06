using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.VisualScripting.Metadata;
using Transform = UnityEngine.Transform;

public class TileEditor : EditorWindow
{
    Editor gameObjectEditor;
    static bool active;

    GameObject selectedTile;
    GameObject gridTile;
    Transform tileParent;
    Transform gridParent;
    int gridSize;

    List<GameObject> tilePalette;
    List<GameObject> gridTiles;
    List<GameObject> gameTiles;     //Will hold a reference to all game tiles, in case you want to delete them all


    [MenuItem("Tools/Level Editor/Tile Editor")]
    public static void ShowWindow()
    {
        TileEditor tileEditor = GetWindow<TileEditor>();
        tileEditor.titleContent = new GUIContent("Tile Editor");
    }

    private void OnEnable()
    {

        SceneView.duringSceneGui += HandleMouseEvent;
    }
    private void OnDisable()
    {
        SceneView.duringSceneGui -= HandleMouseEvent;
    }

    private void HandleMouseEvent(SceneView view)
    {
        if (!active)
        {
            return;
        }

        if (Event.current.type == EventType.MouseDown)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            RaycastHit hit;

            // Spawn obj on hit location
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "GridTile")
                {
                    var obj = CreateTile();
                    Vector3 objPosition = hit.collider.gameObject.transform.position;
                    objPosition.y = 0;
                    obj.transform.position = objPosition;
                }
            }
        }
    }

    void OnGUI()
    {
        //GUILayout.Label("Active:" + active);
        EditorGUILayout.Space();
        GUILayout.Label("Grid Details", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        gridSize = EditorGUILayout.IntField("Size of Grid", gridSize);
        gridTile = EditorGUILayout.ObjectField("Grid Tile", gridTile, typeof(GameObject), false) as GameObject;
        gridParent = EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true) as Transform;
        EditorGUILayout.Space();

        if (gridParent != null && gridTile != null)
        {
            GUIContent buttonContent = new GUIContent();

            if (active) buttonContent.text = "Destroy Grid";
            else buttonContent.text = "Create Grid";

            if (GUILayout.Button(buttonContent.text, GUILayout.Width(120), GUILayout.Height(40)))
            {
                active = !active;
                if (active)
                    CreateGrid();
                else
                    DestroyGrid();
            }
        }

        EditorGUILayout.Space();
        selectedTile = EditorGUILayout.ObjectField("Selected Tile", selectedTile, typeof(GameObject), false) as GameObject;
        ShowPreview();


        tileParent = EditorGUILayout.ObjectField("Tile Parent", tileParent, typeof(Transform), true) as Transform;

        

        GameObject toBeRemoved = null;

        if (tilePalette.Count > 0)
        {
            foreach (GameObject tilePaint in tilePalette)
            {
                EditorGUILayout.BeginHorizontal();

                GUILayout.Label(tilePaint.name);
                CreateTilePreview(tilePaint);

                if (GUILayout.Button("Select"))
                {
                    selectedTile = tilePaint;
                }
                if (GUILayout.Button("Remove"))
                {
                    selectedTile = null;
                    toBeRemoved = tilePaint;
                }
                EditorGUILayout.EndHorizontal();
            }
            RemoveFromTilePalette(toBeRemoved);
        }

        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("+", GUILayout.Width(30), GUILayout.Height(30)))
        {
            EditorTile.ShowWindow(this);
        }
        EditorGUILayout.EndHorizontal();
    }

    private GameObject CreateTile()
    {
        GameObject tile = Instantiate(selectedTile);


        if (tileParent != null)
            tile.transform.SetParent(tileParent);
        return tile;
    }

    public void AddToTilePalette(GameObject updatedTile)
    {
        if (tilePalette.Contains(updatedTile))
        {
            selectedTile = updatedTile;
            return;
        }

        tilePalette.Add(updatedTile);
    }
    public void RemoveFromTilePalette(GameObject updatedTile)
    {
        tilePalette.Remove(updatedTile);
    }

    private void ShowPreview()
    {
        if (selectedTile != null)
        {
            Object.DestroyImmediate(gameObjectEditor);
            gameObjectEditor = Editor.CreateEditor(selectedTile);

            gameObjectEditor.OnPreviewGUI(GUILayoutUtility.GetRect(50, 50), EditorStyles.whiteLabel);

            if (tilePalette.Contains(selectedTile)) return;

            AddToTilePalette(selectedTile);
        }
    }

    private void CreateTilePreview(GameObject tilePaint)
    {
        if (tilePaint != null)
        {
            Object.DestroyImmediate(gameObjectEditor);
            gameObjectEditor = Editor.CreateEditor(tilePaint);

            gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(50, 50), EditorStyles.whiteLabel);
        }
    }

    private void CreateGrid()
    {
        Vector3 startPoint = gridParent.transform.position;
        startPoint = new Vector3(startPoint.x + 0.5f, 0, startPoint.z + 0.5f);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j <= gridSize; j++)
            {
                Vector3 tilePosition = new Vector3(startPoint.x + i, -0.3f, startPoint.z + j);
                if (j != gridSize)
                {
                    GameObject tile = Instantiate(gridTile, tilePosition, Quaternion.identity);
                    gridTiles.Add(tile);
                    tile.transform.SetParent(gridParent);
                }

            }
        }
    }

    private void DestroyGrid()
    {
        if (gridParent.childCount == 0) return;

        foreach (GameObject tile in gridTiles)
            DestroyImmediate(tile);

        gridTiles.Clear();
    }

}
