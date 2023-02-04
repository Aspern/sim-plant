using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public const int dimension = 32;

    
    // {10, 6, 6, 6, 6, 6,11}
    // { 5,16,16,16,16,16, 7}
    // { 5,16,15, 4,12,16, 7}
    // { 5,16, 7, 0, 5,16, 7}
    // { 5,16,14, 6,13,16, 7}
    // { 5,16,16,16,16,16, 7}
    // { 9, 4, 4, 4, 4, 4, 8}

    
    public int[,] data = new int[dimension,dimension] { 
        {10, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,11 },
        { 5,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16, 7 },
        { 9, 4,12,16,16,16,20,20,16,16,16,16,16,16,16,16,15, 4,12,16,16,16,16,16,16,16,16,16,15, 4, 4, 8 },
        { 0, 0, 9,12,16,16,24,16,21,16,16,15, 4, 4, 4, 4, 8, 0, 9,12,16,16,22,16,16,16,16,16, 7, 0, 0, 0 },
        { 0, 0, 0, 5,16,16,25,16,24,16,16,14, 6, 6, 6, 6,11, 0, 0, 5,16,16,16,22,16,16,15, 4, 8, 0, 0, 0 },
        { 0,10,11, 9,12,16,16,16,16,16,16,16,16,16,16,16, 7, 0, 0, 5,16,21,16,16,16,16, 7, 0,10, 6,11, 0 },
        {10,13,14,11, 5,16,16,15, 4, 4, 4, 4, 4, 4, 4, 4, 8,10, 6,13,16,16,16,16,16,15, 8,10,13,16, 7, 0 },
        { 5,16,15, 8, 9, 4, 4, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5,16,16,16,15, 4, 4, 4, 8,10,13,16,16, 7, 0 },
        { 5,16, 7, 0, 0, 0, 0,10, 6, 6, 6, 6, 6,11, 0, 0,10,13,16,16,16, 7, 0, 0, 0,10,13,16,16,15, 8, 0 },
        { 5,16,14,11, 0, 0, 0, 5,16,16,16,16,16, 7, 0, 0, 5,16,15, 4, 4, 8,10, 6, 6,13,16,16,16, 7, 0, 0 },
        { 9,12,16,14, 6,11, 0, 5,16,16,16,16,16,14,11, 0, 9, 4, 8, 0, 0, 0, 5,16,16,16,16,16,16, 7, 0, 0 },
        { 0, 5,16,16,16, 7, 0, 9,12,16,16,16,16,16, 7, 0, 0, 0, 0,10, 6, 6,13,16,16,16,16,16,16,14,11, 0 },
        { 0, 5,16,16,16, 7, 0, 0, 5,16,16,16,16,16,14,11, 0, 0, 0, 5,16,16,16,16,16,15, 4,12,16,16,14,11 },
        { 0, 5,16,16,16,14,11, 0, 9, 4, 4, 4,12,16,16, 7, 0,10, 6,13,16,16,16,16,16, 7, 0, 5,16,16,16, 7 },
        { 0, 5,16,16,16,16,14, 6,11, 0, 0, 0, 5,16,16, 7, 0, 5,16,16,16,16,16,15, 4, 8, 0, 5,16,16,16, 7 },
        { 0, 9,12,16,16,16,16,16,14,11, 0, 0, 9,12,16, 7, 0, 5,16,16,16,16,16, 7, 0, 0, 0, 9,12,16,16, 7 },
        { 0, 0, 5,16,16,20,16,16,16,14, 6,11, 0, 5,16,14, 6,13,16,15, 4, 4, 4, 8,10, 6, 6, 6,13,16,15, 8 },
        { 0, 0, 9, 4,12,16,16,16,16,16,16, 7, 0, 5,16,16,16,16,16, 7, 0, 0, 0,10,13,16,16,16,16,16, 7, 0 },
        { 0, 0, 0, 0, 9,12,20,16,16,16,16, 7, 0, 5,16,16,16,16,16,14,11, 0, 0, 5,16,16,16,20,16,16, 7, 0 },
        { 0,10, 6, 6, 6,13,16,16,16,15, 4, 8, 0, 9,12,16,16,16,16,16, 7, 0, 0, 9, 4,12,16,16,16,16,14,11 },
        { 0, 5,16,16,16,16,16,16,15, 8, 0, 0, 0, 0, 5,16,16,16,16,16,14, 6, 6,11, 0, 9,12,16,16,16,16, 7 },
        { 0, 5,16,16,16,16,16,16,14,11, 0, 0, 0, 0, 9, 4, 4,12,16,16,16,16,16, 7, 0, 0, 9,12,16,16,15, 8 },
        { 0, 5,16,16,16,16,16,16,16, 7,10, 6,11, 0, 0, 0, 0, 5,16,16,16,16,16,14, 6, 6,11, 9,12,16, 7, 0 },
        { 0, 9, 4, 4, 4,12,16,16,16, 7, 5,16, 7, 0, 0, 0, 0, 9, 4, 4,12,16,16,16,16,16, 7, 0, 5,16, 7, 0 },
        { 0, 0, 0, 0, 0, 5,16,16,16, 7, 5,16,14,11, 0, 0,10, 6,11, 0, 5,16,16,16,16,16,14, 6,13,16,14,11 },
        {10, 6,11, 0,10,13,16,16,15, 8, 5,16,16, 7, 0, 0, 5,16,14,11, 9, 4, 4,12,16,16,16,16,16,16,16, 7 },
        { 5,16, 7, 0, 5,16,16,16, 7, 0, 5,16,22,14,11, 0, 9,12,16, 7, 0, 0, 0, 5,16,16,16,16,16,16,16, 7 },
        { 5,16, 7, 0, 5,16,15, 4, 8,10,13,16,16,16,14,11, 0, 5,16, 7, 0, 0,10,13,16,16,16,16,16,16,16, 7 },
        { 5,16, 7, 0, 5,16, 7, 0, 0, 5,16,16,16,16,21,14, 6,13,16, 7, 0, 0, 5,16,16,16,16,16,16,16,15, 8 },
        { 5,16,14, 6,13,16,14, 6, 6,13,16,16,16,20,16,16,16,16,16,14,11, 0, 5,16,16,16,16,16,16,16, 7, 0 },
        { 5,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16,16, 7, 0, 9, 4, 4, 4, 4, 4, 4, 4, 8, 0 },
        { 9, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    struct TileStruct {
        public int x;
        public int y;
        public Tile tile;

        public TileStruct(int x, int y, Tile tile) {
            this.x = x;
            this.y = y;
            this.tile = tile;
        }
    }
    private List<TileStruct> _tiles = new List<TileStruct>();
    
    void Start() { }

    void Update() { }

    public int getDataAt(int x, int y) {
        if (x < 0 || x > dimension - 1) {
            return 0;
        }

        if (y < 0 || y > dimension - 1) {
            return 0;
        }

        return data[y, x];
    }

    public void AddTile(Tile tile)
    {
        var position = tile.gameObject.transform.position;
        _tiles.Add(new TileStruct(((int)position.x), (int)position.z, tile));
    }

    public Tile GetTileAt(int x, int y) {
        foreach (var tileStruct in _tiles) {
            if (tileStruct.x == x && tileStruct.y == y) {
                return tileStruct.tile;
            }
        }
        return null;
    }
}
