using UnityEngine;

public class Tile: MonoBehaviour
{
    public GameObject plantPrefab;
    public TileType type;
    public bool planted;
    public bool PlantGrown { get; set; }

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
        Instantiate(
            plantPrefab,
            parentTransform.position,
            Quaternion.identity,
            parentTransform
        );
    }
}