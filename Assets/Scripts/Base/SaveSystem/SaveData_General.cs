using Base.GameManagement.Settings;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Base.SaveSystem
{
    [CreateAssetMenu(fileName = "GeneralGameSaveData", menuName = "Save/General Game Save Data", order = 0)]
    [InlineEditor]
    public class SaveData_General: SerializedSaveData
    {
        [field: SerializeField]
        public float TotalCoin { get; set; }

        [field: SerializeField]
        public float HighScore { get; set; }

        [field: SerializeField]
        public float CurrentScore { get; set; }

        [field: SerializeField]
        public int LevelIndex { get; set; }

        public override void Delete()
        {
            base.Delete();
            TotalCoin = Settings_General.Instance.CoinManagerSettings.DefaultCoinCount;
            LevelIndex = 0;
            HighScore = 0;
            CurrentScore = 0;
        }
    }
}
