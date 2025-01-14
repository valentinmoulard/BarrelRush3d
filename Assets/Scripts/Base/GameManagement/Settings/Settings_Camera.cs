using System;
using UnityEngine;

namespace Base.GameManagement.Settings
{
    [Serializable]
    public class Settings_Camera
    {
        [field: SerializeField]
        public float CameraMoveDuration { get; private set; } = 0.5f;
        
        [field: SerializeField]
        public float DragThreshold { get; private set; } = 0.1f;

        [field: SerializeField]
        public float MovementSpeed { get; private set; } = 1;

        [field: SerializeField]
        public float Friction { get; private set; } = 0.9f;

        [field: SerializeField]
        public Vector2 CameraLimits { get; private set; } = new Vector2(25, 25);
    }
}