﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickListener : MonoBehaviour
{
    private Camera _camera;
    private SimPlant _simPlant;

    private void Awake()
    {
        _simPlant = GameObject.Find("Map").GetComponent<SimPlant>();
        _camera = Camera.main;
    }

    private void Update()
    {
        DetectObjectWithRaycast();
    }

    private void DetectObjectWithRaycast()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        // Ignore UI clicks
        if (EventSystem.current.IsPointerOverGameObject()) return;
        
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit)) // Found a GameObj by Left Mouse Click
        {
            var gameObj = hit.collider.gameObject;
            var tile = gameObj.GetComponent<Tile>();

            if (tile)
            {
                _simPlant.SelectTile(gameObj);

                if (tile.planted)
                {
                    if (tile.PlantGrown)
                    {
                        _simPlant.EnableActionButton(ActionType.NECTAR);
                    }

                    if (tile.Flourished)
                    {
                        _simPlant.EnableActionButton(ActionType.BEE);
                    }

                    if (tile.Pollinated)
                    {
                        _simPlant.EnableActionButton(ActionType.SEED);
                    }
                }
                else
                {
                    _simPlant.DisableAllActionButtons();
                }
            }
            else // Disable actions and remove selection if game obj is not a tile
            {
                _simPlant.UnselectTile();
                _simPlant.DisableAllActionButtons();
            }
        }
        else // Disable actions and remove selection if click anywhere in map
        {
            _simPlant.UnselectTile();
            _simPlant.DisableAllActionButtons();
        }
    }
}