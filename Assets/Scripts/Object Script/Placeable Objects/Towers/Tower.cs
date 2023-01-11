using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    //This script will be responsible for the placement and delete part of the tower
    //TODO: For now, the tower stats will be added in this script, but will be moved to a Stat script in future
    
    public Tile tile;   //The tile it is placed on


    [SerializeField] private float health;
    public Action OnTowerDestroyEvent;
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
        if (InputManager.Instance.isUI) return;
        RemoveTower();
    }


    //Tower Stats

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            OnTowerDestroyEvent?.Invoke();
            RemoveTower();
        }
    }



}
