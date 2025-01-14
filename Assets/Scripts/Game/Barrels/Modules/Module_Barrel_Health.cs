using System;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Particles;
using DG.Tweening;
using Game.Pools;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Barrels.Modules
{
    public class Module_Barrel_Health: Module_Barrel
    {
        [SerializeField]
        private TMP_Text healthText;
 
        [SerializeField]
        [ReadOnly]
        private bool canExplode = true;
        
        [CanBeNull]
        private Sequence _takeDamageSequence;

        private Tween _explodeDelayTween;
        private float _explosionRadius;
        private readonly Collider[] _explosionResults = new Collider[9];

        private float Health {
            get => _health;
            set
            {
                if (Math.Abs(_health - value) > 0.01f)
                {
                    _health = value;
                    healthText.text = _health > 0 ? Mathf.Max(1, (int)_health).ToString() : string.Empty;
                }
            }
        }
        private float _health;
        private int _coinToEarn;

        public void SetUp(int hP, bool explosive)
        {
            canExplode = explosive;
            Health = hP;
            _coinToEarn = hP / 10;
            _explosionRadius = Settings_General.Instance.GameSettings.SettingsBarrel.BarrelExplodeRadius;
        }
        
        public void TakeDamage(float damage)
        {
            Health -= damage;
            CheckIfExplode();
            _takeDamageSequence?.Kill(true);
            _takeDamageSequence = TakeDamageSequence();
        }

        private void CheckIfExplode()
        {
            if (Health > 0) return;
            Death();
        }

        private void Death()
        {
            if (canExplode)
            {
                ManagersAccess.ParticleManager.PlayParticle(ParticleType.BarrelExplode, transform);
                _explodeDelayTween?.Kill();
                _explodeDelayTween = DOVirtual.DelayedCall(0.1f, Explode);
            }
            ReturnToPool();
            ManagersAccess.ParticleManager.PlayParticle(ParticleType.BarrelDeath, transform);
            BarrelBase.ActivateAddOn();
        }

        private void Explode()
        {
            var size = Physics.OverlapSphereNonAlloc(transform.position, _explosionRadius, _explosionResults);
            if(size < 1) return;
            for (var i = 0; i < size; i++)
            {
                var result = _explosionResults[i];
                result.TryGetComponent(out Module_Barrel_Health barrel);
                if (barrel && barrel != this)
                {
                    ForceToDeath(barrel);
                }
            }
        }

        private void ForceToDeath(Module_Barrel_Health barrel)
        {
            barrel.TakeDamage(barrel.Health);
        }

        private void ReturnToPool()
        {
            AddCoin();
            BarrelBase.ReturnToPool();
            BarrelBase.Death();
        }

        private void AddCoin()
        {
            if(_coinToEarn > 0)
                ManagersAccess.CoinManager.AddCoin(_coinToEarn);
        }

        private Sequence TakeDamageSequence()
        {
            var sequence = DOTween.Sequence();
            var punchScale = Vector3.one * 0.1f;
            var duration = 0.2f;
            sequence.Append(healthText.transform.DOPunchScale(punchScale, duration));
            var getHitAnimationTransform = BarrelBase.GetHitAnimationTransform();
            if (getHitAnimationTransform != null)
            {
                sequence.Join(getHitAnimationTransform.DOPunchScale(punchScale, duration));
            }
            return sequence;
        }
        
        public void ChangeTextActivity(bool isActive)
        {
            if(healthText.enabled == isActive) return;
            healthText.enabled = isActive;
        }
    }
}