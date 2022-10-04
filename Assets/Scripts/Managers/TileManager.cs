using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance = null;

    [SerializeField]private Dictionary<Guid, Tile> tiles = new Dictionary<Guid, Tile>();

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

    public void AddTile(Tile tile)
    {
        tiles.Add(tile.GetId(), tile);
    }

    public void RemoveTile(Tile tile)
    {
        tiles.Remove(tile.GetId());
    }

    public void UpdateTileToUnplaced(Tile tile)
    {
        Tile t = tiles[tile.GetId()];
        t.ChangeToUnplaced();
    }
}
