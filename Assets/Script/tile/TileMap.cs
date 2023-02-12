using System;
using System.Collections.Generic;
using System.Linq;
using Script.plant;
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
                [Compass.LabelNorth] = GetAt(new Vector3(position.x, position.y, position.z + TileSize)),
                [Compass.LabelNorthWest] = GetAt(new Vector3(position.x - TileSize, position.y, position.z + TileSize)),
                [Compass.LabelWest] = GetAt(new Vector3(position.x - TileSize, position.y, position.z)),
                [Compass.LabelSouthWest] = GetAt(new Vector3(position.x - TileSize, position.y, position.z - TileSize)),
                [Compass.LabelSouth] = GetAt(new Vector3(position.x, position.y, position.z - TileSize)),
                [Compass.LabelSouthEast] = GetAt(new Vector3(position.x + TileSize, position.y, position.z - TileSize)),
                [Compass.LabelEast] = GetAt(new Vector3(position.x + TileSize, position.y, position.z)),
                [Compass.LabelNorthEast] = GetAt(new Vector3(position.x + TileSize, position.y, position.z + TileSize))
            };

            return neighbors;
        }

        public GameObject RandomPlantableTile()
        {
            var plantableTiles = PlantableTiles();

            if (plantableTiles.Count == 0) return null;

            var index = new Random().Next(0, plantableTiles.Count - 1);
            return plantableTiles[index];
        }

        public GameObject RandomPollinableTile()
        {
            var pollinableTiles = PlantableTiles()
                .FindAll(t => t.GetComponent<PlantableTile>().Plant != null)
                .FindAll(t => t.GetComponent<PlantableTile>().Plant.GetComponent<Plant>().IsBloomed());
            
            if (pollinableTiles.Count == 0) return null;
            
            var index = new Random().Next(0, pollinableTiles.Count - 1);
            return pollinableTiles[index];
        }

        public List<GameObject> PlantableTiles()
        {
            return _tiles.FindAll(e => e.Tile.GetComponent<PlantableTile>() != null)
                .Select(e => e.Tile)
                .ToList();
        }
        
        public List<GameObject> PlantedTiles()
        {
            return PlantableTiles()
                .FindAll(t => t.GetComponent<PlantableTile>().IsPlanted());
        }
    }
}