using System.Collections.Generic;
using System.Linq;
using Base.Managers;
using Game._Hero;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Base.GameManagement
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private Manager_Save saveManager;
        
        [SerializeField]
        private Manager_Pool poolManager;
        
        [SerializeField]
        private Manager_GameState gameStateManager;
        
        [SerializeField]
        private Manager_Camera cameraManager;
        
        [SerializeField]
        private Manager_Particle particleManager;
        
        [SerializeField]
        private Manager_Coin coinManager;

        [SerializeField]
        private Manager_UI UIManager;

        [SerializeField]
        private Manager_Joystick joystickManager;
        
        [SerializeField]
        private Manager_Level levelManager;

        [SerializeField]
        private Manager_HighScore highScoreManager;

        [SerializeField]
        private Module_Manager_Hero hero;

        [SerializeField, ReadOnly]
        private List<ManagerBase> managers;

        public override void InstallBindings()
        {
            Application.targetFrameRate = 60;
            SetUpManagers();
        }
        
        private void SetUpManagers()
        {
            ManagersAccess.SetUpManagers(
                saveManager,
                poolManager,
                gameStateManager,
                cameraManager,
                particleManager,
                coinManager,
                UIManager,
                joystickManager,
                levelManager,
                highScoreManager);
            
            managers.ForEach(manager => manager.SetUp());
            
            HeroAccess.SetUpHero(hero);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                saveManager.Save();
            }
        }

        private void OnDestroy()
        {
            saveManager.Save();
        }
        
        public override void Start()
        {
            base.Start();
#if UNITY_EDITOR
            SRDebug.Instance.Settings.UIScale = 2;
            
#else
            SRDebug.Instance.Settings.UIScale = 1;
#endif
        }
        
        #if UNITY_EDITOR
        [Button]
        private void SetManagers()
        {
            managers.Clear();
            managers = GetComponentsInChildren<ManagerBase>().ToList();
            managers.Add(UIManager);
        }
        #endif
    }
}