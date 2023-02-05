using UnityEngine;

namespace Script.tile
{
    /**
     * 
     */
    public class Tile : MonoBehaviour
    {
        private TileMap _tileMap;

        protected virtual void Awake()
        {
            // Each tile adds itself to the tilemap
            _tileMap = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<TileMap>();
        }

        protected virtual void Start()
        {
            _tileMap.Add(gameObject);
        }
    }
}
