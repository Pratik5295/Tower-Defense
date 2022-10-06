using log4net.Util;
using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using static Unity.VisualScripting.Metadata;
using Transform = UnityEngine.Transform;

public class TileEditor : EditorWindow
{
    static bool active;

    GameObject selectedTile;
    GameObject gridTile;
    Transform tileParent;
    Transform gridParent;
    int gridSize;

    List<GameObject> gridTiles;
    List<GameObject> gameTiles;     //Will hold a reference to all game tiles, in case you want to delete them all

    [MenuItem("Tools/Level Editor/Tile Editor")]
    public static void ShowWindow()
    {
        GetWindow<TileEditor>();
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
        GUILayout.Label("Active:" + active);

        gridTile = EditorGUILayout.ObjectField("Grid Tile", gridTile, typeof(GameObject), false) as GameObject;

        selectedTile = EditorGUILayout.ObjectField("Selected Tile",selectedTile, typeof(GameObject), false) as GameObject;

        tileParent = EditorGUILayout.ObjectField("Tile Parent",tileParent, typeof(Transform), true) as Transform;

        gridParent = EditorGUILayout.ObjectField("Tile Parent", gridParent, typeof(Transform), true) as Transform;

        gridSize = EditorGUILayout.IntField("Size of Grid", gridSize);

        if (GUILayout.Button("Start Tile Editor"))
        {
            active = !active;
            if (active)
                CreateGrid();
            else
                DestroyGrid();
        }
    }

    private GameObject CreateTile()
    {
        GameObject tile = Instantiate(selectedTile);
       

        if (tileParent != null)
            tile.transform.SetParent(tileParent);
        return tile;
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

        foreach(GameObject tile in gridTiles)
            DestroyImmediate(tile);

        gridTiles.Clear();
    }

}
