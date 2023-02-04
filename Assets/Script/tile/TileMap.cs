using System;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    private readonly int directions = 4;
    private Tile[,] _tiles;

    private void Awake()
    {
        var mapData = GameObject.Find("Map").GetComponent<MapData>();
        _tiles = new Tile [mapData.data.GetLength(0), mapData.data.GetLength(1)];
        
        for (var i = 0; i < mapData.data.GetLength(0); i++)
        {
            for (var j = 0; j < mapData.data.GetLength(1); j++)
            {
                var tileId = mapData.data[i, j] / directions;
                try
                {
                    var tile = new Tile
                    {
                        type = TileTypes.findById(tileId)
                    };
                    _tiles[i, j] = tile;
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
    }

    public Tile getByPosition(int x, int y)
    {
        return _tiles[x, y];
    }
}