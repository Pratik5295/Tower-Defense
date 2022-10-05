using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Tile tile;   //The tile it is placed on

    public void AddTileReference(Tile _tile)
    {
        tile = _tile;
    }

    public void RemoveTower()
    {
        TileManager.Instance.UpdateTileToUnplaced(tile);
        tile = null;
        Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        //It wont be based on mouse click, but a UI in future

        RemoveTower();
    }


  
}
