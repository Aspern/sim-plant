using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject plantPrefab;
    public TileType type;
    public bool planted;
    public bool PlantGrown;
    public bool Flourished;
    public bool Pollinated { get; set; }

    private GameObject _plantGameObj;
    
    private MapData _mapData;

    private void Awake() {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
    }

    public void UseNectar()
    {
        if (!PlantGrown || !_plantGameObj) return;
        _plantGameObj.GetComponent<TreePlant>().CreateFlower();
    }

    public void UseBee()
    {
        if (!Flourished || !_plantGameObj) return;
        _plantGameObj.GetComponent<TreePlant>().CreateBees();
    }

    private void Start()
    {
        if (planted)
        {
            InstantiatePlant();
        }
        _mapData.AddTile(this);
    }

    private void InstantiatePlant()
    {
        var parentTransform = transform;
        _plantGameObj = Instantiate(
            plantPrefab,
            parentTransform.position,
            Quaternion.identity,
            parentTransform
        );
    }

    public void OnFullyGrown()
    {
        PlantGrown = true;
        GameObject.Find("Map").GetComponent<SimPlant>().OnGrowthChanged(this);
    }
    
    public void OnFlourished()
    {
        Flourished = true;
        GameObject.Find("Map").GetComponent<SimPlant>().OnFlourishedChanged(this);
    }
}