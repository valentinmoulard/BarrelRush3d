using UnityEngine;
using Base.GameManagement;
using Base.Events;
using Base.Ui;
using Base.GameManagement.Settings;
using Game.Events;
using TMPro;

namespace Game.Ui
{
    public class UI_ReviveTimer : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text m_timer = null;

        private float timer = 0f;
        private bool isTimerEnabled = false;
        private float _reviveTime = 0f;

        private void OnEnable()
        {
            EventsGame.OnGameStateChanged += OnGameStateChanged;
            _reviveTime = Settings_General.Instance.GameSettings.SettingsRevive.ReviveTime;
            Events_Revive.OnRevive += OnRevive;
        }

        private void OnDisable()
        {
            EventsGame.OnGameStateChanged -= OnGameStateChanged;
            Events_Revive.OnRevive -= OnRevive;
        }

        private void Update()
        {
            ManageReviveTimer();
        }

        private void ManageReviveTimer()
        {
            if (isTimerEnabled)
            {
                timer += Time.deltaTime;

                m_timer.text = (_reviveTime - timer).ToString("F0");

                if (timer >= _reviveTime)
                {
                    isTimerEnabled = false;
                    Events_Revive.OnRevive?.Invoke(false);
                }
            }
        }

        private void OnRevive(bool state)
        {
            isTimerEnabled = false;
            timer = 0f;
        }

        private void OnGameStateChanged(UIScreenType state, bool isEnabled)
        {
            if (state == UIScreenType.WaitingForRevive && isEnabled)
            {
                isTimerEnabled = true;
            }
        }
    }
}