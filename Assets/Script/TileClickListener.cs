using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickListener : MonoBehaviour
{
    private Camera _camera;
    private readonly List<Button> _actionButtons = new();
    private Button _actionButtonNectar;
    private Button _actionButtonBee;
    private Button _actionButtonSeed;
    private Button _actionButtonSun;
    private TileMap _tileMap;

    private void Awake()
    {
        _tileMap = GameObject.Find("Map").GetComponent<TileMap>();
        _camera = Camera.main;

        _actionButtonNectar = GameObject.Find("ActionButtonNectar").GetComponent<Button>();
        _actionButtonBee = GameObject.Find("ActionButtonBee").GetComponent<Button>();
        _actionButtonSeed = GameObject.Find("ActionButtonSeed").GetComponent<Button>();
        _actionButtonSun = GameObject.Find("ActionButtonSun").GetComponent<Button>();
        
        _actionButtons.Add(_actionButtonNectar);
        _actionButtons.Add(_actionButtonBee);
        _actionButtons.Add(_actionButtonSeed);
        _actionButtons.Add(_actionButtonSun);
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
                _tileMap.SelectTile(gameObj);

                if (tile.planted)
                {
                    if (tile.PlantGrown)
                    {
                        _actionButtonNectar.interactable = true;
                    }
                }
                else
                {
                    _actionButtons.ForEach(button => button.interactable = false);
                }
            }
            else // Disable actions and remove selection if game obj is not a tile
            {
                _tileMap.UnselectTile();
                _actionButtons.ForEach(button => button.interactable = false);
            }
        }
        else // Disable actions and remove selection if click anywhere in map
        {
            _tileMap.UnselectTile();
            _actionButtons.ForEach(button => button.interactable = false);
        }
    }
}