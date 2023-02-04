using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject plantPrefab;
    public TileType type;
    public bool planted;
    public bool plantGrown;
    public bool flourished;
    private bool propagated = false;
    public bool Pollinated { get; set; }

    private GameObject _plantGameObj;

    private MapData _mapData;

    private void Awake()
    {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
        
    }

    public void UseNectar()
    {
        if (!plantGrown || !_plantGameObj) return;
        _plantGameObj.GetComponent<TreePlant>().CreateFlower();
    }

    public void UseBee()
    {
        if (!flourished || !_plantGameObj) return;
        _plantGameObj.GetComponent<TreePlant>().CreateBees();
    }

    public void UseSeed()
    {
        if (!Pollinated || propagated) return;

        var neighborsTiles = FindNeighborsTiles();
        foreach (Tile tile in neighborsTiles)
        {
            if (tile.type == TileType.PLAIN) 
            {
                tile.planted = true;
                tile.InstantiatePlant();
                propagated = true;
                break;
            }
        }
    }

    private List<Tile> FindNeighborsTiles()
    {
        var neighbors = new List<Tile>();
        var position = gameObject.transform.position;
        var x = (int)(position.x);
        var y = (int)(position.z);

        for (var i = x - 1; i < x + 2; i++)
        {
            for (var j = y - 1; j < y + 2; j++)
            {
                if (i == x && j == y) continue;
                var neighborsTile = _mapData.GetTileAt(i, j);
               
                if (neighborsTile != null)
                {
                    neighbors.Add(neighborsTile);
                }
            }
        }

        return neighbors;
    }

    private void Start()
    {
        if (planted)
        {
            InstantiatePlant();
        }

        _mapData.AddTile(this);
    }

    public void InstantiatePlant()
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
        plantGrown = true;
        GameObject.Find("Map").GetComponent<SimPlant>().OnGrowthChanged(this);
    }

    public void OnFlourished()
    {
        flourished = true;
        GameObject.Find("Map").GetComponent<SimPlant>().OnFlourishedChanged(this);
    }

    public void OnPollinated()
    {
        Pollinated = true;
        GameObject.Find("Map").GetComponent<SimPlant>().OnPollinatedChanged(this);
    }
}