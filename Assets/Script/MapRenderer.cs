using UnityEngine;

public class MapRenderer : MonoBehaviour
{
    private GameObject[] _tiles = new GameObject[7];
    private TileMap _tileMap;
    private MapData _mapData;

    private void Awake() {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
        _tileMap = GameObject.Find("Map").GetComponent<TileMap>();

        _tiles[0] = Resources.Load<GameObject>("fbx/water");
        _tiles[1] = Resources.Load<GameObject>("fbx/edge");
        _tiles[2] = Resources.Load<GameObject>("fbx/corner");
        _tiles[3] = Resources.Load<GameObject>("fbx/inner_corner");
        _tiles[4] = Resources.Load<GameObject>("fbx/plain");
        _tiles[5] = Resources.Load<GameObject>("fbx/rock");
        _tiles[6] = Resources.Load<GameObject>("fbx/trees");
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
        var tileId = mapData / 4;

        var tileGameObj = Instantiate(_tiles[tileId]);
        tileGameObj.AddComponent<BoxCollider>();
        tileGameObj.AddComponent<Tile>();
        tileGameObj.AddComponent<Highlight>();

        var type = TileTypes.findById(tileId);
        var tile = tileGameObj.GetComponent<Tile>();
        
        _tileMap.AddTile(x, y, tile);
        
        tileGameObj.GetComponent<Tile>().type = type;

        switch (mapData % 4) {
            case 1:
                tileGameObj.transform.localEulerAngles = new Vector3(0, 0, 90);
                break;
            case 2:
                tileGameObj.transform.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case 3:
                tileGameObj.transform.localEulerAngles = new Vector3(0, 0, 270);
                break;
        }

        if(tileGameObj != null) tileGameObj.transform.position = new Vector3(x + 0.5f, 0 , y + 0.5f);
    }

    void Update()
    {
        
    }
}
