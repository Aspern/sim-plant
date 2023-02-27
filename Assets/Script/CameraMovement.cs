using System;
using Script.tile;
using UnityEngine;

namespace Script {
    // TODO Gesture-Detection for Touch Screens
    public class CameraMovement : MonoBehaviour {
        private Camera _camera;
        private const float CAMERA_MIN_HEIGHT = 2f;
        private const float CAMERA_MAX_HEIGHT = 6.5f;
        private const float CAMERA_KEYBOAD_MOVE_SPEED = 5.0f;
        private const float CAMERA_KEYBOAD_ZOOM_SPEED = 5.0f;
        private const float CAMERA_KEYBOAD_ROTATE_SPEED = 90.0f;

        private Plane _ground = new Plane(Vector3.up, new Vector3(0f, 0.5f, 0f));

        Vector3 _previousScreenPosition;
        private Vector3 _rotateAroundPoint;
        private TileMap _map;

        private void Awake() {
            _map = GameObject.Find(Environment.EnvironmentComponentName).GetComponent<TileMap>();
        }

        private void Start() {
            _camera = Camera.main;
        }

        void Update() {
            ZoomCamera();
            MoveCamera();
            RotateCamera();
        }

        private void ZoomCamera() {
            ZoomCameraWithKeyboard();
            ZoomCameraWithMouse();
        }
        
        private void ZoomCameraWithKeyboard() {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                var newPosition = _camera.transform.position;
                if(Input.GetKey(KeyCode.DownArrow)) {
                    newPosition -= _camera.transform.forward * CAMERA_KEYBOAD_ZOOM_SPEED * Time.deltaTime;
                }
                if(Input.GetKey(KeyCode.UpArrow))
                {
                    newPosition += _camera.transform.forward * CAMERA_KEYBOAD_ZOOM_SPEED * Time.deltaTime;
                }

                if (newPosition.y >= CAMERA_MIN_HEIGHT && newPosition.y <= CAMERA_MAX_HEIGHT) {
                    _camera.transform.position = newPosition;
                }
            }
        }
        
        private void ZoomCameraWithMouse() {
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
            MoveCameraWithKeyboard();
            MoveCameraWithMouse();
        }

        private void MoveCameraWithKeyboard() {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
                // ctrl-key down -> rotate, not move
                return;
            }

            var newPosition = _camera.transform.position;
            if(Input.GetKey(KeyCode.RightArrow)) {
                newPosition += _camera.transform.right * CAMERA_KEYBOAD_MOVE_SPEED * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.LeftArrow))
            {
                newPosition -= _camera.transform.right * CAMERA_KEYBOAD_MOVE_SPEED * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.DownArrow)) {
                newPosition -= CameraToWorldForward() * CAMERA_KEYBOAD_MOVE_SPEED * Time.deltaTime;
            }
            if(Input.GetKey(KeyCode.UpArrow))
            {
                newPosition += CameraToWorldForward() * CAMERA_KEYBOAD_MOVE_SPEED * Time.deltaTime;
            }

            if (CameraInsideMap(newPosition)) {
                _camera.transform.position = newPosition;
            }
        }

        private Vector3 CameraToWorldForward() {
            var worldForward = _camera.transform.forward;
            worldForward.y = 0;
            return worldForward.normalized;
        }
        
        private void MoveCameraWithMouse() {
            bool isDown = Input.GetMouseButton(1);
            bool wentDown = Input.GetMouseButtonDown(1);

            if (isDown) {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 worldPoint = ScreenToWorldPosition(screenPosition);

                if (!wentDown) {
                    Vector3 previousWorldPoint = ScreenToWorldPosition(_previousScreenPosition);
                    Vector3 worldDelta = worldPoint - previousWorldPoint;

                    var newPosition = _camera.transform.position - worldDelta;

                    if (CameraInsideMap(newPosition)) {
                        _camera.transform.position = newPosition;
                    }
                }

                _previousScreenPosition = screenPosition;
            }
        }

        private bool CameraInsideMap(Vector3 cameraPosition) {
            var cameraCenterWorldPosition = CameraCenterToWorldPosition(cameraPosition);

            if (cameraCenterWorldPosition.x >= _map.XMin && cameraCenterWorldPosition.x <= _map.XMax && cameraCenterWorldPosition.z >= _map.YMin && cameraCenterWorldPosition.z <= _map.YMax) {
                return true;
            }

            return false;
        }

        private Vector3 CameraCenterToWorldPosition(Vector3 cameraPosition) {
            var ray = new Ray(cameraPosition, _camera.transform.forward);
            _ground.Raycast(ray, out var distance);
            return ray.GetPoint((distance));
        }

        private Vector3 ScreenToWorldPosition(Vector3 screenPosition) {
            var ray = _camera.ScreenPointToRay(screenPosition);
            _ground.Raycast(ray, out var distance);
            return ray.GetPoint(distance);
        }

        private void RotateCamera() {
            RotateCameraWithKeyboard();
            RotateCameraWithMouse();
        }

        private void RotateCameraWithKeyboard() {
            bool isDown = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
            bool wentDown = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl);

            if (isDown) {
                if (wentDown) {
                    _rotateAroundPoint = CameraCenterToWorldPosition(_camera.transform.position);
                }

                float angle = 0f;
                if(Input.GetKey(KeyCode.RightArrow)) {
                    angle = CAMERA_KEYBOAD_ROTATE_SPEED * Time.deltaTime;
                }
                if(Input.GetKey(KeyCode.LeftArrow))
                {
                    angle = -CAMERA_KEYBOAD_ROTATE_SPEED * Time.deltaTime;
                }
                _camera.transform.RotateAround(_rotateAroundPoint, Vector3.up, angle);
            }
        }

        private void RotateCameraWithMouse() {
            bool isDown = Input.GetMouseButton(2);
            bool wentDown = Input.GetMouseButtonDown(2);

            if (isDown) {
                Vector3 screenPosition = Input.mousePosition;
                Vector3 worldPoint = ScreenToWorldPosition(screenPosition);

                if (wentDown) {
                    _rotateAroundPoint = CameraCenterToWorldPosition(_camera.transform.position);
                }
                else {
                    Vector3 previousWorldPoint = ScreenToWorldPosition(_previousScreenPosition);
                    var angle = Vector3.SignedAngle(worldPoint - _rotateAroundPoint, previousWorldPoint - _rotateAroundPoint, Vector3.up);
                    _camera.transform.RotateAround(_rotateAroundPoint, Vector3.up, angle);
                }

                _previousScreenPosition = screenPosition;
            }
        }
    }
}