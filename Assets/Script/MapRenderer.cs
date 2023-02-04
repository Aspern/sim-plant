using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    private GameObject[] _tiles = new GameObject[5];
    private MapData _mapData;

    private void Awake() {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();

        _tiles[0] = Resources.Load<GameObject>("fbx/water");
        _tiles[1] = Resources.Load<GameObject>("fbx/edge");
        _tiles[2] = Resources.Load<GameObject>("fbx/corner");
        _tiles[3] = Resources.Load<GameObject>("fbx/inner_corner");
        _tiles[4] = Resources.Load<GameObject>("fbx/plain");
    }

    void Start() {
        for (int x = 0; x < MapData.dimension; x++) {
            for (int y = 0; y < MapData.dimension; y++) {
                InstantiateTileAt(x, y);
            }
        }
    }

    private void InstantiateTileAt(int x, int y) {
        var mapData = _mapData.getDataAt(x, y);

        GameObject tile = Instantiate(_tiles[mapData / 4]) as GameObject;

        switch (mapData % 4) {
            case 1:
                tile.transform.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case 2:
                tile.transform.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case 3:
                tile.transform.localEulerAngles = new Vector3(0, 0, 270);
                break;
        }

        if(tile != null) tile.transform.position = new Vector3(x + 0.5f, y + 0.5f , 0);
    }

    void Update()
    {
        
    }
}
