using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Bosses
{
    [Serializable]
    public class BossData
    {
        [field: SerializeField, PreviewField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public int HealthPoints { get; private set; }
    }
}