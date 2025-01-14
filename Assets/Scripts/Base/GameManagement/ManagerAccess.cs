using System.Collections.Generic;
using Base.Managers;
using Game._Hero;

namespace Base.GameManagement
{
    public static class ManagersAccess
    {
        public static Manager_Save SaveManager { get; private set; }
        public static Manager_Level LevelManager { get; private set; }
        public static Manager_GameState GameStateController { get; private set; }
        public static Manager_Pool PoolManager { get; private set; }
        public static Manager_Camera CameraManager { get; private set; }
        public static Manager_Particle ParticleManager { get; private set; }
        public static Manager_Coin CoinManager {get; private set; }
        public static Manager_UI UIManager {get; private set; }
        public static Manager_Joystick JoystickManager { get; private set; }
        public static Manager_HighScore HighScoreManager { get; private set; }
        private static List<ManagerBase> Managers => new List<ManagerBase>
        {
            SaveManager,
            LevelManager,
            GameStateController,
            PoolManager,
            CameraManager,
            ParticleManager,
            CoinManager,
            UIManager,
            JoystickManager,
            HighScoreManager
        };
        
        public static void SetUpManagers(
            Manager_Save saveManager,
            Manager_Pool poolManager,
            Manager_GameState gameStateController,
            Manager_Camera cameraManager,
            Manager_Particle particleManager,
            Manager_Coin coinManager,
            Manager_UI managerUi,
            Manager_Joystick joystickManager,
            Manager_Level levelManager,
            Manager_HighScore highScoreManager)
        {
            LevelManager = levelManager;
            GameStateController = gameStateController;
            PoolManager = poolManager;
            CameraManager = cameraManager;
            ParticleManager = particleManager;
            CoinManager = coinManager;
            SaveManager = saveManager;
            UIManager = managerUi;
            JoystickManager = joystickManager;
            HighScoreManager = highScoreManager;
        }
    }
    
    public static class HeroAccess
    {
        public static Module_Manager_Hero Hero { get; private set; }

        public static void SetUpHero(Module_Manager_Hero hero)
        {
            Hero = hero;
        }
    }
}