using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class Settings_IconHolder
    {
        [field: SerializeField]
        public string UpgradeArrow { get; private set; }

        [field: SerializeField]
        public string FireDamage { get; private set; }

        [field: SerializeField]
        public string FireSpeed { get; private set; }

        [field: SerializeField]
        public string IncomeMultiplier { get; private set; }
    }
}