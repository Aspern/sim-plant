using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    private Dictionary<string, GameObject> _tiles;
    private MapData _mapData;

    private void Awake() {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();

        _tiles = new Dictionary<string, GameObject> { { "inner_corner", Resources.Load<GameObject>("fbx/inner_corner") } };
    }

    void Start() {
        var innerCorner1 = Instantiate(_tiles["inner_corner"]) as GameObject;
        innerCorner1.transform.position = new Vector3(0, 0, 0);
        innerCorner1.transform.localEulerAngles = new Vector3(0, 0, -90);

        var innerCorner2 = Instantiate(_tiles["inner_corner"]) as GameObject;
        innerCorner2.transform.position = new Vector3(1, 0, 0);
        
        var innerCorner3 = Instantiate(_tiles["inner_corner"]) as GameObject;
        innerCorner3.transform.position = new Vector3(0, 1, 0);
        innerCorner3.transform.localEulerAngles = new Vector3(0, 0, 180);

        var innerCorner4 = Instantiate(_tiles["inner_corner"]) as GameObject;
        innerCorner4.transform.position = new Vector3(1, 1, 0);
        innerCorner4.transform.localEulerAngles = new Vector3(0, 0, 90);

    }

    void Update()
    {
        
    }
}
