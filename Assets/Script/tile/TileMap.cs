using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Script.tile
{
    public struct TileData
    {
        public readonly int X;
        public readonly int Y;
        public readonly GameObject Tile;

        public TileData(int x, int y, GameObject tile)
        {
            X = x;
            Y = y;
            Tile = tile;
        }
    }

    public class TileMap : MonoBehaviour
    {
        private readonly List<TileData> _tiles = new();
        private const int TileSize = 1;

        public void Add(GameObject tileGameObj)
        {
            if (!tileGameObj.GetComponent<Tile>()) return;

            var position = tileGameObj.transform.position;
            var x = position.x;
            var y = position.z; // ignoring 3rd dimension

            // integer positions are easier for calculation purposes
            _tiles.Add(new TileData(
                (int) x,
                (int) y,
                tileGameObj
            ));
        }

        private GameObject GetAt(Vector3 position)
        {
            return (from tile in _tiles where tile.X == (int) position.x && tile.Y == (int) position.z select tile.Tile)
                .FirstOrDefault();
        }

        public Dictionary<string, GameObject> Neighbors(Vector3 position)
        {
            var neighbors = new Dictionary<string, GameObject>
            {
                ["north"] = GetAt(new Vector3(position.x, position.y, position.z + TileSize)),
                ["north_west"] = GetAt(new Vector3(position.x - TileSize, position.y, position.z + TileSize)),
                ["west"] = GetAt(new Vector3(position.x - TileSize, position.y, position.z)),
                ["south_west"] = GetAt(new Vector3(position.x - TileSize, position.y, position.z - TileSize)),
                ["south"] = GetAt(new Vector3(position.x, position.y, position.z - TileSize)),
                ["south_east"] = GetAt(new Vector3(position.x + TileSize, position.y, position.z - TileSize)),
                ["east"] = GetAt(new Vector3(position.x + TileSize, position.y, position.z)),
                ["north_east"] = GetAt(new Vector3(position.x + TileSize, position.y, position.z + TileSize))
            };

            return neighbors;
        }

        public GameObject RandomPlantableTile()
        {
            var plantableTiles = PlantableTiles();

            var index = new Random().Next(0, plantableTiles.Count - 1);
            return plantableTiles[index];
        }

        public List<GameObject> PlantableTiles()
        {
            return _tiles.FindAll(e => e.Tile.GetComponent<PlantableTile>() != null)
                .Select(e => e.Tile)
                .ToList();
        }
    }
}