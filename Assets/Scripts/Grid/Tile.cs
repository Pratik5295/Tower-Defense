using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    //TODO:
    //Change walkable to unwalkable, add it as an obstacle to nav mesh agent
    public enum Type
    {
        UNPLACED = 0,
        PLACED = 1,     //Set placed for tiles with turrets or obstacles, these tile will be unwalkable
        WALKABLE = 2
    }

    [SerializeField] private Guid tileId;
    [SerializeField] private Type type;

    [SerializeField] private GameObject tileCanvas;

    private void Awake()
    {
        //Generate a new Guid each time game is started, would need to change it afterwards?

        tileId = Guid.NewGuid();
    }

    private void Start()
    {
        if (TileManager.Instance != null)
            TileManager.Instance.AddTile(this);

        if (tileCanvas == null) return;
        tileCanvas.SetActive(false);
    }

    public Guid GetId()
    {
        return tileId;
    }
    private void SetType(Type state)
    {
        type = state;
    }

    public void OnMouseDown()
    {
        if (InputManager.Instance.isUI) return;

        if (type == Type.PLACED) return;

        if(type == Type.WALKABLE)
        {
            MoveHero();
        }
        //TODO: Future, if not in build mode, return (dont do anything)

        if(type == Type.UNPLACED)
        {
            //Open the options menu
            tileCanvas.SetActive(true);
        }
    }

    public void CloseBuildMenu()
    {
        tileCanvas.SetActive(false);
    }


    //Building Manager
    public void BuildTower(GameObject item)
    {
        if (BuildingManager.Instance == null) return;

        BuildingManager.Instance.SetBuildingItem(item);
        BuildingManager.Instance.BuildItem(this);
        SetType(Type.PLACED);
        tileCanvas.SetActive(false);
        BuildingManager.Instance.SetBuildingItem(null);
    }

    public void ChangeToUnplaced()
    {
        //This method will be called at different times,
        //For eg. When a tower is moved, or destroyed or sold,
        //      we need to change the tile back to being placeable to hold items

        if (type != Type.PLACED) return;

        SetType(Type.UNPLACED);
    }


    #region Hero Movement

    public void MoveHero()
    {
        if (InputManager.Instance == null) return;

        if (InputManager.Instance.selectedHeroObject == null) return;

        Hero hero = InputManager.Instance.selectedHeroObject;

        hero.MoveHero(this.gameObject);
    }

    #endregion
}
