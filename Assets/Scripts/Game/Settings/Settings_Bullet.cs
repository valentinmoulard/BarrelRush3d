using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_Bullet
    {
        [field: SerializeField]
        public float Distance { get; private set; } = 15f;

        [field: SerializeField]
        public float Duration { get; private set; } = 5f;
    }
}