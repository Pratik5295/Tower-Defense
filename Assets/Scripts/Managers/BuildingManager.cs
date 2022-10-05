using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance = null;

    public GameObject buildingItem; //Prefab selected to be built   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    public void BuildItem(Tile tile)
    {
        if (buildingItem == null) return;

        Vector3 buildPosition = GetBuildPosition(tile.gameObject.transform);
        GameObject building = 
            Instantiate(buildingItem, buildPosition, Quaternion.identity);

        Tower tower = building.GetComponent<Tower>();
        tower.AddTileReference(tile);
    }

    private Vector3 GetBuildPosition(Transform tilePosition)
    {
        Vector3 buildPostion = new Vector3(tilePosition.position.x, tilePosition.position.y, tilePosition.position.z);
        return buildPostion;
    }
}
