using System;
using Base.Events;
using Base.GameManagement;
using Base.Helpers;
using Base.Ui;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Color = System.Drawing.Color;

namespace Base.Managers
{
    public class Manager_GameState : ManagerBase
    {
        [field: SerializeField, ReadOnly]
        public UIScreenType ActiveState { get; private set; }
        private Tween _changeStateTween;

        public bool IsPlaying => ActiveState == UIScreenType.Playing;
        protected override void OnDisable()
        {
            base.OnDisable();
            DisableActiveState();
        }

        public void SetState(UIScreenType type, float delay = 0.1f)
        {
            if (CheckIfSameState(type)) return;
            DisableActiveState();
            _changeStateTween?.Kill();
            _changeStateTween = DOVirtual.DelayedCall(delay, () => EnableState(type));
        }

        private bool CheckIfSameState(UIScreenType type)
        {
            return ActiveState == type;
        }

        private void DisableActiveState()
        {
            EventsGame.OnGameStateChanged?.Invoke(ActiveState, false);
        }

        private void EnableState(UIScreenType type)
        {
            ActiveState = type;
            ManagersAccess.UIManager.ChangeScreen(ActiveState);
            EventsGame.OnGameStateChanged?.Invoke(ActiveState, true);
        }
    }
}