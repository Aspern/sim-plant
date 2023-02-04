using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickListener : MonoBehaviour
{
    private Camera _camera;
    private readonly List<Button> _actionButtons = new();
    private TileMap _tileMap;

    private void Awake()
    {
        _tileMap = GameObject.Find("Map").GetComponent<TileMap>();
        _camera = Camera.main;

        for (var i = 1; i < 5; i++)
        {
            _actionButtons.Add(GameObject.Find("ActionButton" + i).GetComponent<Button>());
        }
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
                _actionButtons.ForEach(button => button.interactable = true);
                _tileMap.SelectTile(gameObj);
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