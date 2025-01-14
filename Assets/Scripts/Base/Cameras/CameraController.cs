using Cinemachine;
using UnityEngine;

namespace Base.Cameras
{
    public class CameraController: MonoBehaviour
    {
        [SerializeField]
        private CinemachineVirtualCamera cineMachineVirtualCamera;

        [SerializeField]
        private float focusCameraZOffset = 20;
        
        public void ChangeOrder(int order)
        {
            cineMachineVirtualCamera.Priority = order;
        }
        
        public void ResetXPosition(float x)
        {
            var position = transform.position;
            position.x = x;
            transform.position = position;
        }

        public void SetPosition(Vector3 position)
        {
            position.y = transform.position.y;
            position.z -= focusCameraZOffset;
            transform.position = position;
        }
    }
}