using System;
using Game.Settings;
using UnityEngine;

namespace Base.GameManagement.Settings
{
    [Serializable]
    public class GameSettings
    {
        [field: SerializeField]
        public Settings_Road SettingsRoad { get; private set; }

        [field: SerializeField]
        public Settings_Barrel SettingsBarrel { get; private set; }

        [field: SerializeField]
        public Settings_Bullet SettingsBullet { get; private set; }

        [field: SerializeField]
        public Settings_HeroMovement SettingsHeroMovement { get; private set; }

        [field: SerializeField]
        public Settings_IconHolder SettingsIconHolder { get; private set; }

        [field: SerializeField]
        public Settings_Soldier SettingsSoldier { get; private set; }

        [field: SerializeField]
        public Settings_Revive SettingsRevive { get; private set; }

        [field: SerializeField]
        public LayerMask UnitLayerMask { get; private set; }

        [field: SerializeField]
        public LayerMask BarrelLayerMask { get; private set; }
    }
}