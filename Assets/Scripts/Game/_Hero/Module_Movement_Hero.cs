using System;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Managers;
using Game.Modules;
using Game.Settings;
using UnityEngine;

namespace Game._Hero
{
    public class Module_Movement_Hero: Module_Movement
    {
        private Manager_Joystick JoystickManager => ManagersAccess.JoystickManager;
        Settings_HeroMovement MovementSettings => Settings_General.Instance.GameSettings.SettingsHeroMovement;

        [field: SerializeField]
        public Rigidbody Rigidbody { get; private set; }

        private Module_UnitHolder_Hero module_UnitHolder_Hero = null;

        private Vector2 _currentVelocity;
        private Vector2 _targetVelocity;
        private float _smoothHorizontalVelocity;
        private bool _isWaiting = false;

        public override void ModuleStart()
        {
            base.ModuleStart();
            module_UnitHolder_Hero = gameObject.GetComponent<Module_UnitHolder_Hero>();

            if (module_UnitHolder_Hero == null)
                Debug.LogError("Could not retreive Module_UnitHolder_Hero component!");
        }

        public override void ModuleUpdate()
        {
            base.ModuleUpdate();
            ClampPosition();
        }

        public override void ModuleFixedUpdate()
        {
            if (!ManagersAccess.GameStateController.IsPlaying) return;
            if(_isWaiting) return;
            base.ModuleFixedUpdate();
            HandleVelocityChange();
        }
        
        private void ClampPosition()
        {
            var position = transform.position;
            position.x = Mathf.Clamp(position.x, module_UnitHolder_Hero.SoldiersMaxXBoundaries.x, module_UnitHolder_Hero.SoldiersMaxXBoundaries.y);
            position.y = 0;
            transform.position = position;
        }

        private float NextHorizontal()
        {
            _targetVelocity.x = 0;
            if (JoystickManager.InputHold)
            {
                _targetVelocity.x = JoystickManager.HorizontalInput * MovementSettings.MaxHorizontalVelocityAmplitude *
                                    MovementSettings.TouchSensitivity;
                _targetVelocity.x = Mathf.Clamp(_targetVelocity.x, -MovementSettings.MaxHorizontalVelocityAmplitude,
                    MovementSettings.MaxHorizontalVelocityAmplitude);
            }

            var nextHorizontal = Mathf.SmoothDamp(_currentVelocity.x, _targetVelocity.x, ref _smoothHorizontalVelocity,
                MovementSettings.HorizontalSmoothTime);

            return nextHorizontal;
        }
        
        private void HandleVelocityChange()
        {
            if (JoystickManager == null || JoystickManager.IsWaiting) return;
            _currentVelocity = new Vector2()
            {
                x = NextHorizontal(),
            };
            Rigidbody.velocity = _currentVelocity;
        }
        
        public void ChangeOnWaiting(bool isWaiting)
        {
            _isWaiting = isWaiting;
            if (_isWaiting)
            {
                _targetVelocity = Vector3.zero;
                _currentVelocity = Vector3.zero;
            }
            Rigidbody.isKinematic = _isWaiting;
            JoystickManager.ChangeWaiting(_isWaiting);
        }
    }
}