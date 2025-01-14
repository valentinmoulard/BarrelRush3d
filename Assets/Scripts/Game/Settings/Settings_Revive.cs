using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_Revive
    {
        [field: SerializeField]
        public float ReviveTime { get; private set; } = 10f;
    }
}