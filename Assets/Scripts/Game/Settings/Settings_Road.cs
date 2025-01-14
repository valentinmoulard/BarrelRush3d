using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_Road
    {
        [field: SerializeField]
        public float RoadLimit { get; private set; } = 4f;

        [field: SerializeField]
        public float RoadSpeed { get; private set; } = 5f;

        [field: SerializeField, ReadOnly]
        public float[] BarrelXPositions { get; private set; }

        [field: SerializeField]
        public float BossBarrelDistanceToLastBarrel { get; private set; } = 10f;

        [field: SerializeField]
        public float GapBetweenSections { get; private set; } = 20;

        [field: SerializeField]
        public float RoadSpeedIncreasePerBoss { get; private set; } = 0.25f;

        [Button]
        private void SetBarrelXPositions(float distance)
        {
            BarrelXPositions = new float[4];
            var halfDistance = distance / 2f;
            BarrelXPositions[0] = -halfDistance * 3f;
            BarrelXPositions[1] = -halfDistance;
            BarrelXPositions[2] = halfDistance;
            BarrelXPositions[3] = halfDistance * 3f;
        }
    }
}