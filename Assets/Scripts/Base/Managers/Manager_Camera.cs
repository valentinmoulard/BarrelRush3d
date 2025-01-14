using Base.Cameras;
using Base.GameManagement.Settings;
using UnityEngine;

namespace Base.Managers
{
    public sealed class Manager_Camera : ManagerBase
    {
        [SerializeField]
        private CameraController currentCamera;

        #region CameraSettings

        private Settings_Camera _cameraSettings;
        private Settings_Camera CameraSettings => _cameraSettings ??= Settings_General.Instance.SettingsCamera;
        
        #endregion

        private Vector3 _velocity;
        private Vector3 _previousMousePosition;
        private bool _isDragging;
        private bool _isMouseMoved;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousMousePosition = Input.mousePosition;
                _isDragging = false;
                _isMouseMoved = false;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 mouseDeltaPosition = Input.mousePosition - _previousMousePosition;

                // Check if the mouse has moved beyond the threshold to start dragging
                if (!_isMouseMoved && mouseDeltaPosition.magnitude > CameraSettings.DragThreshold)
                {
                    _isMouseMoved = true;
                    _isDragging = true;
                }

                _previousMousePosition = Input.mousePosition;

                if (_isDragging)
                {
                    // Apply the movement speed multiplier to the velocity calculation
                    _velocity += new Vector3(0, 0, -mouseDeltaPosition.y * CameraSettings.MovementSpeed);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isDragging = false;
            }

            if (!_isDragging)
            {
                _velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime * CameraSettings.Friction);
            }

            Vector3 newPosition = currentCamera.transform.position + (_velocity * Time.deltaTime);
            newPosition.z = Mathf.Clamp(newPosition.z, CameraSettings.CameraLimits.x, CameraSettings.CameraLimits.y);
            currentCamera.transform.position = newPosition;
        }
    }
}
