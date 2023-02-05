using Script.tile;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script
{
    public class EnvironmentClickListener : MonoBehaviour
    {
        private Camera _camera;
        private Environment _environment;
        private HudController _controller;

        private void Awake()
        {
            _camera = Camera.main;
            _environment = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<Environment>();
            _controller = GameObject.Find(HudController.HudControllerComponentName).GetComponent<HudController>();
        }

        private void Update()
        {
            DetectTileWithRaycast();
        }

        private void DetectTileWithRaycast()
        {
            // only left mouse butto
            if (!Input.GetMouseButtonDown(0)) return;
            // ignore UI clicks
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            var ray = _camera.ScreenPointToRay(Input.mousePosition);

            // Ignore if no raycast hit was found
            if (!Physics.Raycast(ray, out var hit)) return;
            
            var tileGameObj = hit.collider.gameObject;

            if (tileGameObj.GetComponent<Tile>())
            {
                HandleTileMouseClick(tileGameObj);
            }
        }

        private void HandleTileMouseClick(GameObject tileGameObj)
        {
            _environment.SelectionChange(tileGameObj);
            var plantableTile = tileGameObj.GetComponent<PlantableTile>();

            if (plantableTile)
            {
                _controller.ActionMenu.CheckActionButtonsInteractable(plantableTile);
                plantableTile.OnAnimationFinished = _controller.ActionMenu.CheckActionButtonsInteractable;
            }
            else
            {
                // Disable all action buttons if tile is not from type plantable.
                _controller.ActionMenu.ResetButtonInteractable();
            }
        }
    }
}