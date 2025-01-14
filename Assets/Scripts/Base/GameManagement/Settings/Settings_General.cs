using Base.ControlPanelManagement;
using Base.Helpers;
using Game.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.GameManagement.Settings
{
    [InfoBox("This asset have to be in the Resources folder")]
    [InlineEditor()]
    [CreateAssetMenu(fileName = "Settings_General", menuName = "YufisBase/Settings_General")]
    public class Settings_General : ScriptableSingletonMono<Settings_General>, IControlPanelAsset
    {
        [field: SerializeField, TabGroup("GeneralSettings")]
        public float GameSpeed { get; private set; } = 1;

        [field: SerializeField, TabGroup("GeneralSettings")]
        public bool DebugActivity { get; private set; }

        [field: SerializeField, TabGroup("CoinManager")]
        public CoinManagerSettings CoinManagerSettings { get; private set; }
        
        [field: SerializeField, TabGroup("Camera")]
        public Settings_Camera SettingsCamera { get; private set; }

        [field: SerializeField]
        public GameSettings GameSettings { get; private set; }

        public int ControlPanelPriority { get; } = 100;
        public string TreeParentPath { get; }
    }
}