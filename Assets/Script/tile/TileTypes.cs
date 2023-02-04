using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class TileTypes
{
    public static TileType Water = new(0, "water");
    public static TileType Edge = new(1, "edge");
    public static TileType Corner = new(2, "corner");
    public static TileType InnerCorner = new(3, "inner_corner");
    public static TileType Plain = new(4, "plain");

    
    public static TileType findById(int id)
    {
        return id switch
        {
            0 => Water,
            1 => Edge,
            2 => Corner,
            3 => InnerCorner,
            4 => Plain,
            _ => throw new Exception("Cannot find tile for id " + id)
        };
    }
}