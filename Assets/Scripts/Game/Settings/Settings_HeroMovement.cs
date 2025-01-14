using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_HeroMovement
    {
        [Tooltip("Maximum allowed amplitude for horizontal velocity.")]
        [field: SerializeField, Range(0, 100)]
        public float MaxHorizontalVelocityAmplitude { get; set; } = 55;

        [Tooltip("The smooth time for horizontal movements.")]
        [field: SerializeField, Range(0, 1)]
        public float HorizontalSmoothTime { get; set; } = 0.045f;
        
        [Tooltip("Determines the sensitivity of touches.")]
        [field: SerializeField, Range(0, 1)]
        public float TouchSensitivity { get; set; } = 0.05f;
        
        /*[field: SerializeField]
        public float XPositionMagnitudeClam { get; private set; } = 2.4f;

        [Tooltip("The smooth time for vertical movements.")]
        [field: SerializeField, Range(0, 1)]
        public float VerticalSmoothTime { get; private set; } = 0.55f;

        [Tooltip("The base target vertical velocity.")]
        [field: SerializeField]
        public float BaseTargetVerticalVelocity { get; private set; } = 3;

        [Tooltip("The base target vertical velocity.")]
        [field: SerializeField]
        public float BaseTargetVerticalVelocityMultiplier { get; private set; } = 1f;*/

    }
}