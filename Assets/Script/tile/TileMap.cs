using System.Collections.Generic;
using UnityEngine;

namespace Script.tile
{
    public class TileMap : MonoBehaviour
    {
        private Dictionary<int, GameObject> _tilesDict = new();

        public void Add(GameObject tileGameObj)
        {
            if (tileGameObj.GetComponent<Tile>())
            {
                _tilesDict[tileGameObj.transform.position.GetHashCode()] = tileGameObj;
            }
        }
    }
}