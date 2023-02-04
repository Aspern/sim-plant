using System;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private Tile[,] _tiles;
    private Tile _selection;

    private void Awake()
    {
        var mapData = GameObject.Find("Map").GetComponent<MapData>();
        _tiles = new Tile [mapData.data.GetLength(0), mapData.data.GetLength(1)];
    }

    public void AddTile(int x, int y, Tile tile)
    {
        _tiles[x, y] = tile;
    }

    public void SelectTile(Tile tile)
    {
        _selection = tile;
        Debug.Log($"Selected tile  {tile.type.id}, {tile.type.name}");
    }

    public void UnselectTile()
    {
        _selection = null;
        Debug.Log("Unselected tile");
    }
}