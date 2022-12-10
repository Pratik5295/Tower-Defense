using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager Instance = null;

    public GameObject buildingItem; //Prefab selected to be built

    [Header("Tower Prefabs")]
    public GameObject targetTower;
    public GameObject areaTower;
    

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

    public void SetBuildingItem(GameObject item)
    {
        buildingItem = item;
    }

    public void SetTargetTower()
    {
        buildingItem = targetTower;
    }

    public void SetAreaTower()
    {
        buildingItem = areaTower;
    }

    public void BuildItem()
    {
        if (buildingItem == null) return;

        Tile tile = TileManager.Instance.currentTile;

        Vector3 buildPosition = GetBuildPosition(tile.gameObject.transform);
        GameObject building = 
            Instantiate(buildingItem, buildPosition, Quaternion.identity);

        Tower tower = building.GetComponent<Tower>();
        tower.AddTileReference(tile);

        tile.ChangeToPlaced();
    }

    private Vector3 GetBuildPosition(Transform tilePosition)
    {
        Vector3 buildPostion = new Vector3(tilePosition.position.x, tilePosition.position.y, tilePosition.position.z);
        return buildPostion;
    }
}
