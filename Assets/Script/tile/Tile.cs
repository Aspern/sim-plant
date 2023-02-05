﻿using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject plantPrefab;
    public GameObject beePrefab;
    public GameObject seedPrefab;
    public TileType type;
    public bool planted;
    public bool beeed;
    public bool plantGrown;
    public bool Flourished  { get; set; }
    public bool Pollinated { get; set; }
    public bool PlantDead { get; set; }
    public Action<ActionType, bool> ActionHandler { get; set; }

    private GameObject _plantGameObj;

    private GameObject _beeGameObj { get; set; }

    private MapData _mapData;
    private Compass _compass;

    private void Awake()
    {
        _mapData = GameObject.Find("Map").GetComponent<MapData>();
        //_compass = GameObject.Find("Compass").GetComponent<Compass>();
    }

    public void UseNectar()
    {
        if (!plantGrown || !_plantGameObj) return;
        _plantGameObj.GetComponent<TreePlant>().CreateFlower();
    }

    public Vector3 UseBee(Bee bee, GameObject beeGameObj)
    {
        _beeGameObj = beeGameObj;
        Vector3 moveDirection = Vector3.zero;
        if (!_beeGameObj) return moveDirection;
        //_plantGameObj.GetComponent<TreePlant>().CreateBees();
        var neighborsTiles = FindNeighborsTiles();
        Debug.Log(neighborsTiles.Count);
        foreach (Tile neighbor in neighborsTiles)
        {
            if (neighbor.type == TileType.PLAIN) 
            {
                Debug.Log("Inside if");
                var position = gameObject.transform.position;
                var x = (int) (position.x);
                var z = (int) (position.z);
                var positionNeighbor = neighbor.transform.position;
                moveDirection = new Vector3((int)positionNeighbor.x - x, 0,
                    (int)positionNeighbor.z - z);
                bee.OnNewTile(neighbor);
                break;
            }
        }
        return moveDirection;
    }

    public void DryPlant()
    {
        Pollinated = false;
        PlantDead = true;
        Flourished = false;
        ActionHandler?.Invoke(ActionType.BEE, false);
        ActionHandler?.Invoke(ActionType.SEED, false);
        ActionHandler?.Invoke(ActionType.NECTAR, false);
        ActionHandler?.Invoke(ActionType.SCYTHE, true);
    }

    public void KillPlant()
    {
        PlantDead = false;
        Flourished = false;
        Pollinated = false;
        plantGrown = false;
        planted = false;
        ActionHandler?.Invoke(ActionType.SCYTHE, false);
        Destroy(_plantGameObj);
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
        Flourished = false;

        LeanTween.move(seedGameObj, neighbour.transform.position, 3).setOnComplete(() =>
        {
            if (neighbour.type == TileType.PLAIN)
                //TODO resolve
            //if (tile.type == TileType.PLAIN && !tile.planted) 
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

        if (beeed)
        {
            InstantiateBee();
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
    private void InstantiateBee()
     {
         var parentTransform = transform;
         _beeGameObj = Instantiate(
             beePrefab,
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
        Flourished = true;
        ActionHandler?.Invoke(ActionType.BEE, true);
    }

    public void OnPollinated()
    {
        Pollinated = true;
        ActionHandler?.Invoke(ActionType.SEED, true);
    }
}