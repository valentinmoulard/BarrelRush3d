using Base.Events;
using Base.GameManagement;
using Base.Level;
using Base.Level.LevelData;
using Base.SaveSystem;
using Base.SaveSystem.SaveableScriptableObject;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Managers
{
    public class Manager_Level: ManagerBase
    {
        #region LevelDataHolder

        private LevelDataHolder _levelDataHolder;
        private LevelDataHolder LevelDataHolder => _levelDataHolder ??= Resources.Load<LevelDataHolder>(nameof(LevelDataHolder));

        #endregion
        
        private LevelDataBase _currentLevelData;
        
        private LevelController _currentLevelController;
        
        private int LevelIndex {
            get => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).LevelIndex;
            set => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).LevelIndex = value;
        }

        public override void SetUp()
        {
            base.SetUp();
            CreateLevel();
        }

        private void CreateLevel()
        {
            _currentLevelData = LevelDataHolder.GetLevelData(LevelIndex);
            _currentLevelController = Instantiate(_currentLevelData.LevelPrefab);
            _currentLevelController.SetUp(_currentLevelData.BarrelLevelData);
        }

        public void RestartScene(bool isWin = false)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            if (isWin)
            {
                LevelIndex++;
            }
        }
    }
}