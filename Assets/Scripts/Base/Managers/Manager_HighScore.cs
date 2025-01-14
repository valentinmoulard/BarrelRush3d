using Base.Events;
using Base.GameManagement;
using Base.SaveSystem;
using Base.SaveSystem.SaveableScriptableObject;
using Base.Ui;
using UnityEngine;

namespace Base.Managers
{
    public class Manager_HighScore : ManagerBase
    {
        public System.Action<float, bool> OnSendHighScore;

        private float m_timer = 0f;
        private bool m_canTrackTime = false;

        public float Timer { get => m_timer; }

        private float HighScore
        {
            get => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).HighScore;
            set => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).HighScore = value;
        }

        private float CurrentScore
        {
            get => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).CurrentScore;
            set => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General).CurrentScore = value;
        }


        protected override void OnEnable()
        {
            base.OnEnable();
            EventsGame.OnGameStateChanged += OnGameStateChanged;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventsGame.OnGameStateChanged -= OnGameStateChanged;
        }


        public override void SetUp()
        {
            base.SetUp();

            if (CurrentScore > 0)
                m_timer = CurrentScore;
            else
                m_timer = 0;

            m_canTrackTime = false;
        }

        private void Update()
        {
            if (m_canTrackTime)
            {
                m_timer += Time.deltaTime;
            }
        }

        private void OnGameStateChanged(UIScreenType screenType, bool state)
        {
            bool isNewHighScore = false;

            switch (screenType)
            {
                case UIScreenType.Playing:
                    m_canTrackTime = true;
                    break;
                case UIScreenType.Win:
                    CurrentScore = m_timer;
                    m_canTrackTime = false;

                    isNewHighScore = CurrentScore > HighScore;

                    if (isNewHighScore)
                        OnSendHighScore?.Invoke(CurrentScore, isNewHighScore);
                    else
                        OnSendHighScore?.Invoke(HighScore, isNewHighScore);
                    break;
                case UIScreenType.Lose:
                    CurrentScore = 0;
                    isNewHighScore = m_timer > HighScore;

                    if (isNewHighScore)
                        HighScore = m_timer;

                    OnSendHighScore?.Invoke(HighScore, isNewHighScore);

                    m_canTrackTime = false;
                    break;
                default:
                    break;
            }



        }
    }
}