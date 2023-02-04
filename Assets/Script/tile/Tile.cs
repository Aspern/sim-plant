using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject seedPrefab;
    public TileType type;
    public bool planted;
    public bool plantGrown;
    public bool flourished;
    public bool Pollinated { get; set; }
    public Action<ActionType, bool> ActionHandler { get; set; }

    private GameObject _plantGameObj;

    private MapData _mapData;
    private Compass _compass;

    private void Awake()
    {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
        _compass = GameObject.Find("Compass").GetComponent<Compass>();
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
        if (!Pollinated) return;


        var parentTransform = transform;
        var position = parentTransform.position;
        position.y += 1;
        var seedGameObj = Instantiate(
            seedPrefab,
            position,
            Quaternion.identity,
            parentTransform
        );


        var neighborsTiles = FindNeighborsTiles();
        var neighbour = neighborsTiles[_compass.CurrentDirection];

        if (!neighbour) return;

        ActionHandler?.Invoke(ActionType.BEE, false);
        ActionHandler?.Invoke(ActionType.SEED, false);
        ActionHandler?.Invoke(ActionType.NECTAR, false);
        Pollinated = false;
        flourished = false;

        LeanTween.move(seedGameObj, neighbour.transform.position, 3).setOnComplete(() =>
        {
            if (neighbour.type == TileType.PLAIN)
            {
                neighbour.planted = true;
                neighbour.InstantiatePlant();
            }
        
            var plant = _plantGameObj.GetComponent<TreePlant>();
            plant.Pollinated = false;
            plant.RemoveFlower();
            ActionHandler?.Invoke(ActionType.NECTAR, true);
        });
    }

    private List<Tile> FindNeighborsTiles()
    {
        var neighbors = new List<Tile>();
        var position = gameObject.transform.position;
        var x = (int) (position.x);
        var y = (int) (position.z);

        // Add neighbours in order to the wind direction
        neighbors.Add(_mapData.GetTileAt(x, y + 1));
        neighbors.Add(_mapData.GetTileAt(x - 1, y + 1));
        neighbors.Add(_mapData.GetTileAt(x - 1, y));
        neighbors.Add(_mapData.GetTileAt(x - 1, y - 1));
        neighbors.Add(_mapData.GetTileAt(x, y - 1));
        neighbors.Add(_mapData.GetTileAt(x + 1, y - 1));
        neighbors.Add(_mapData.GetTileAt(x + 1, y));
        neighbors.Add(_mapData.GetTileAt(x + 1, y + 1));

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
        ActionHandler?.Invoke(ActionType.NECTAR, true);
    }

    public void OnFlourished()
    {
        flourished = true;
        ActionHandler?.Invoke(ActionType.BEE, true);
    }

    public void OnPollinated()
    {
        Pollinated = true;
        ActionHandler?.Invoke(ActionType.SEED, true);
    }
}