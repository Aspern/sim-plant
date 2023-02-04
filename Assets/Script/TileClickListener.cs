using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileClickListener : MonoBehaviour
{
    private Camera _camera;
    private MapData _mapData; 
    private readonly List<Button> _actionButtons = new();

    
    private void Awake()
    {
        // _mapData = GameObject.Find("Map").GetComponent<MapData>();
        _camera = Camera.main;
        for (var i = 1; i < 5; i++)
        {
            _actionButtons.Add( GameObject.Find("ActionButton" + i).GetComponent<Button>());
        }
    }

    private void Update()
    {
        DetectObjectWithRaycast();
    }

    private void DetectObjectWithRaycast()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        _actionButtons.ForEach(button => button.interactable = false);
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var hit)) return;
        
        Debug.Log(hit.collider.name);
        
        if (!hit.collider.name.Contains("corner")) return;
        
        _actionButtons.ForEach(button => button.interactable = true);

        var gameObj = hit.collider.gameObject;
        var gameObjPos = gameObj.transform.position;
        
        // var tileData = _mapData.getTileDataByPos(gameObjPos.x, gameObjPois.y)
        // TODO: Change Actions dependent on tileData
    }


}
