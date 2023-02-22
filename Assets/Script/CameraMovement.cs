using System;
using Script.tile;
using UnityEngine;

namespace Script {
    // TODO Gesture-Detection for Touch Screens
    public class CameraMovement : MonoBehaviour {
        private Camera _camera;
        private float CAMERA_MIN_HEIGHT = 2f;
        private float CAMERA_MAX_HEIGHT = 6.5f;

        private Plane _ground = new Plane(Vector3.up, new Vector3(0f, 0.5f, 0f));

        Vector3 _previousScreenPoint;
        private Vector3 _rotateAroundPoint;
        private TileMap _map;

        private void Awake() {
            _map = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<TileMap>();
        }

        private void Start() {
            _camera = Camera.main;
        }

        void Update() {
            ScrollCamera();
            MoveCamera();
            RotateCamera();
        }

        private void ScrollCamera() {
            var amount = Input.GetAxis("Mouse ScrollWheel");
            if (amount != 0f) {
                var oldPosition = _camera.transform.position;
                _camera.transform.Translate(_camera.transform.forward * amount * 3f, Space.World);

                if (_camera.transform.position.y < CAMERA_MIN_HEIGHT || _camera.transform.position.y > CAMERA_MAX_HEIGHT) {
                    _camera.transform.position = oldPosition;
                }
            }
        }

        private void MoveCamera() {
            bool isDown = Input.GetMouseButton(1);
            bool wentDown = Input.GetMouseButtonDown(1);

            if (isDown) {
                Vector3 screenPosition = Input.mousePosition;

                var ray = _camera.ScreenPointToRay(screenPosition);
                float distance = 0.0f;

                if (_ground.Raycast(ray, out distance)) {
                    Vector3 worldPoint = ray.GetPoint(distance);

                    if (!wentDown) {
                        ray = _camera.ScreenPointToRay(_previousScreenPoint);
                        _ground.Raycast(ray, out distance);
                        Vector3 previousWorldPoint = ray.GetPoint(distance);
                        Vector3 worldDelta = worldPoint - previousWorldPoint;

                        var newPosition = _camera.transform.position - worldDelta;

                        ray = new Ray(newPosition, _camera.transform.forward);
                        _ground.Raycast(ray, out distance);
                        var lookAtPoint = ray.GetPoint((distance));

                        if (lookAtPoint.x >= _map.XMin && lookAtPoint.x <= _map.XMax && lookAtPoint.z >= _map.YMin && lookAtPoint.z <= _map.YMax) {
                            _camera.transform.position = newPosition;
                        }
                    }

                    _previousScreenPoint = screenPosition;
                }
            }
        }

        private void RotateCamera() {
            bool isDown = Input.GetMouseButton(2);
            bool wentDown = Input.GetMouseButtonDown(2);

            if (isDown) {
                Vector3 screenPosition = Input.mousePosition;

                var ray = _camera.ScreenPointToRay(screenPosition);
                float distance = 0.0f;

                if (_ground.Raycast(ray, out distance)) {
                    Vector3 worldPoint = ray.GetPoint(distance);

                    if (wentDown) {
                        ray = new Ray(_camera.transform.position, _camera.transform.forward);
                        _ground.Raycast(ray, out distance);
                        _rotateAroundPoint = ray.GetPoint((distance));
                    }
                    else {
                        ray = _camera.ScreenPointToRay(_previousScreenPoint);
                        _ground.Raycast(ray, out distance);
                        Vector3 previousWorldPoint = ray.GetPoint(distance);
                        var angle = Vector3.SignedAngle(worldPoint - _rotateAroundPoint, previousWorldPoint - _rotateAroundPoint, Vector3.up);
                        _camera.transform.RotateAround(_rotateAroundPoint, Vector3.up, angle);
                    }

                    _previousScreenPoint = screenPosition;
                }
            }
        }
    }
}