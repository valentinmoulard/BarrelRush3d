using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.EconomySystem
{
    [Serializable]
    public class UnlockData
    {
        [field: SerializeField]
        public bool IsUnlockable { get; private set; }

        [field: SerializeField, ShowIf("IsUnlockable")]
        public float UnlockCost { get; private set; }
    }
}
