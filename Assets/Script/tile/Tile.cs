using System;
using UnityEngine;

public class Tile: MonoBehaviour
{
    public GameObject plantPrefab;

    public TileType type;

    public bool planted;

    private void InstantiatePlant()
    {
        var plantGameObj = Instantiate(plantPrefab);
        plantGameObj.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
        if (planted)
        {
            InstantiatePlant();
        }
    }
}