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

        private Plane _ground = new Plane(Vector3.up, new Vector3(0f,0.5f, 0f));
        private TileMap _map;

        private void Awake()
        {
            _camera = Camera.main;
            _environment = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<Environment>();
            _controller = GameObject.Find(HudController.HudControllerComponentName).GetComponent<HudController>();

            _map = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<TileMap>();
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
            
            float distance = 0.0f;

            if (_ground.Raycast(ray, out distance)) {
                Vector3 hitPoint = ray.GetPoint(distance);
                GameObject detectedObject = _map.GetAt(hitPoint);
                if (detectedObject) {
                    HandleTileMouseClick(detectedObject); 
                }
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