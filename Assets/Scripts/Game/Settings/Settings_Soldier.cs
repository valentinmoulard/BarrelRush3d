using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_Soldier
    {
        [field: SerializeField]
        public float SoldierJoinAnimationDuration { get; private set; } = 0.25f;

        [field: SerializeField]
        public float SoldierJoinJumpPower { get; private set; } = 3f;
    }
}