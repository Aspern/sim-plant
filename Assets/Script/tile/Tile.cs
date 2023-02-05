using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    public bool BudStarted { get; set; }
    public bool FlowerStarted { get; set; }

    public Action<ActionType, bool> ActionHandler { get; set; }

    private GameObject _plantGameObj;

    public GameObject BeeGameObj { get; set; }

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
        FlowerStarted = true;
        ActionHandler?.Invoke(ActionType.NECTAR, false);
        _plantGameObj.GetComponent<TreePlant>().CreateFlower();
    }

    public void UseBee()
    {
        if (!BeeGameObj || !Flourished) return;
        BudStarted = true;
        ActionHandler?.Invoke(ActionType.BEE, false);
        _plantGameObj.GetComponent<TreePlant>().CreateBud();
        BeeGameObj.GetComponent<Bee>().flowerAttracted = true;
    }
    
    public Vector3 CreateBee(Bee bee, GameObject beeGameObj)
    {
        var moveDirection = Vector3.zero;
        if (!BeeGameObj) return moveDirection;
        var neighborsTiles = FindNeighborsTiles();

        Tile destTile = null;
        foreach (var tile in neighborsTiles)
        {
            if (!tile.Flourished) continue;
            destTile = tile;
            break;
        }

        while (destTile == null) {
            var random = Random.Range(0, neighborsTiles.Count);
            if (neighborsTiles[random].type == TileType.PLAIN) {
                destTile = neighborsTiles[random];
            } 
        }
        
        Debug.Log(neighborsTiles.Count);

        Debug.Log("Inside if");
        var position = gameObject.transform.position;
        var x = (int) (position.x);
        var z = (int) (position.z);
        var positionNeighbor = destTile.transform.position;
        moveDirection = new Vector3((int)positionNeighbor.x - x, 0,
            (int)positionNeighbor.z - z);
        BeeGameObj = null;
        ActionHandler?.Invoke(ActionType.BEE, false);
        bee.OnNewTile(destTile);
        destTile.BeeGameObj = beeGameObj;
        if (destTile.Flourished && !destTile.BudStarted)
        {
            destTile.ActionHandler?.Invoke(ActionType.BEE, true);
        }
        return moveDirection;
    }

    public bool HasBee()
    {
        return BeeGameObj;
    }

    public void BeforeDryPlant()
    {
        Pollinated = false;
        Flourished = false;
        FlowerStarted = false;
        BudStarted = false;
    }

    public void DryPlant()
    {
        PlantDead = true;
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
        BudStarted = false;
        FlowerStarted = false;
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
        Debug.Log(_compass);
        var neighbour = neighborsTiles[_compass.CurrentDirection];

        if (!neighbour) return;

        ActionHandler?.Invoke(ActionType.BEE, false);
        ActionHandler?.Invoke(ActionType.SEED, false);
        ActionHandler?.Invoke(ActionType.NECTAR, false);
        Pollinated = false;
        Flourished = false;

        var neighbourPos = neighbour.transform.position;
        LeanTween.move(seedGameObj, new Vector3(neighbourPos.x, neighbourPos.y - 0.5f, neighbourPos.z), 3).setOnComplete(() =>
        {
            if (neighbour.type == TileType.PLAIN)

            {
                neighbour.planted = true;
                neighbour.InstantiatePlant();
            }
        
            var plant = _plantGameObj.GetComponent<TreePlant>();
            plant.RemoveBud();
            ActionHandler?.Invoke(ActionType.NECTAR, true);
            Destroy(seedGameObj);
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
         BeeGameObj = Instantiate(
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
    }

    public void OnPollinated()
    {
        Pollinated = true;
        ActionHandler?.Invoke(ActionType.SEED, true);
    }
    
}