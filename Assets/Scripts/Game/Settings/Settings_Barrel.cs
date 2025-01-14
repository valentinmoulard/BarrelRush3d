using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_Barrel
    {
        [field: SerializeField]
        public Material ExplodeMaterial { get; private set; }

        [field: SerializeField]
        public GameObject ExplodeSkullPrefab { get; private set; }
        
        [field: SerializeField]
        public float BarrelZGap { get; private set; } = 2.2f;

        [field: SerializeField]
        public float BarrelExplodeRadius { get; private set; } = 4;

        [field: SerializeField]
        public int BarrelRotateSpeed { get; private set; } = 100;

        [field: SerializeField]
        public GameObject AddOnObject { get; private set; }
    }
}