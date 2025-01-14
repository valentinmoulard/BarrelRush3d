using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Particles;
using DG.Tweening;
using Game.Barrels.Modules;
using Game.Pools;
using UnityEngine;

namespace Game.Bullets
{
    public class Bullet_Base: MonoBehaviour, IPoolable
    {
        [SerializeField]
        private TrailRenderer[] bulletTrails;
        
        private Tween _moveTween;
        private float _damage;
        private LayerMask _barrelLayer;
        private bool _isReturnedToPool = false;
        private TrailRenderer _activeTrail;
        
        public void SetUp(Vector3 startPosition, float damage, int bulletTrailIndex = 0)
        {
            transform.position = startPosition;
            _damage = damage;
            _barrelLayer = Settings_General.Instance.GameSettings.BarrelLayerMask;
            _isReturnedToPool = false;
            SetBulletTrail(bulletTrailIndex);
        }

        private void SetBulletTrail(int bulletTrailIndex)
        {
            _activeTrail?.transform.parent.gameObject.SetActive(false);
            _activeTrail = bulletTrails[bulletTrailIndex];
            _activeTrail?.transform.parent.gameObject.SetActive(true);
        }

        public void MoveToTarget(Vector3 distance, float speed)
        {
            _moveTween?.Kill(true);
            _moveTween = MoveTween(transform.position + distance, speed);
        }
        
        private Tween MoveTween(Vector3 targetPosition, float speed)
        {
            return transform.DOMove(targetPosition, speed).OnStart(()=> _activeTrail.enabled = true).OnComplete(BackToPool).OnUpdate(CheckIfHit);
        }

        private void BackToPool()
        {
            if (_isReturnedToPool) return;
            _isReturnedToPool = true;
            
            _moveTween?.Kill(false);
            _moveTween = null;
            _activeTrail.enabled = false;
            _activeTrail.Clear();
            this.ReturnToPool();
        }

        private void CheckIfHit()
        {
            var results = new Collider[1];
            var size = Physics.OverlapSphereNonAlloc(transform.position, 0.1f, results, _barrelLayer);
            if (size > 0)
            {
                var barrelHealth = results[0].GetComponent<Module_Barrel_Health>();
                if (barrelHealth != null)
                {
                    PlayHitParticle();
                    barrelHealth.TakeDamage(_damage);
                    BackToPool();
                }
            }
        }

        private void PlayHitParticle()
        {
            ManagersAccess.ParticleManager.PlayParticle(ParticleType.BulletHit, transform);
        }
    }
}