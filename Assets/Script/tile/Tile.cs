using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject plantPrefab;
    public TileType type;
    public bool planted;
    public bool PlantGrown { get; set; }
    public bool Flourished { get; set; }
    public bool Pollinated { get; set; }

    private GameObject _plantGameObj;

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
}