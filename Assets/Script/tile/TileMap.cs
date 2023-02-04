using System;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private Tile[,] _tiles;
    public GameObject selection;

    private void Awake()
    {
        var mapData = GameObject.Find("Map").GetComponent<MapData>();
        _tiles = new Tile [mapData.data.GetLength(0), mapData.data.GetLength(1)];
    }

    public void AddTile(int x, int y, Tile tile)
    {
        _tiles[x, y] = tile;
    }

    public void SelectTile(GameObject tileGameObj)
    {
        if (selection) // Remove highlight from past selection
        {
            selection.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
        
        selection = tileGameObj;
        tileGameObj.GetComponent<Highlight>()?.ToggleHighlight(true);
    }

    public void UnselectTile()
    {
        if (!selection) return;
        
        selection.GetComponent<Highlight>()?.ToggleHighlight(false);
        selection = null;
    }
}