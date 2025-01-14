using Base.GameManagement;
using Base.GameManagement.Settings;
using Game.Modules;
using Game.Pools;
using UnityEngine;

namespace Game.Barrels.Modules
{
    public class Module_Barrel_Collide: Module_Barrel
    {
        [SerializeField]
        private Vector3 collisionOffset = Vector3.zero;

        [SerializeField]
        private Vector3 collisionScale = Vector3.zero;

        [SerializeField]
        private float textCheckDistance = 7;

        private LayerMask _unitLayerMask;
        private LayerMask _barrelLayerMask;

        public void SetUp()
        {
            _unitLayerMask = Settings_General.Instance.GameSettings.UnitLayerMask;
            _barrelLayerMask = Settings_General.Instance.GameSettings.BarrelLayerMask;
        }

        public override void Module_Update()
        {
            base.Module_Update();
            if (!IsActivated) return;
            CheckForCollision();
        }

        private void CheckForCollision()
        {
            Collider[] results = new Collider[1];
            var position = transform.position;
            CheckCollisionForUnits(position, results);
            CheckCollisionForTextActivity(position);
        }

        private void CheckCollisionForUnits(Vector3 position, Collider[] results)
        {
            int numberOfCollisions = Physics.OverlapBoxNonAlloc(position - collisionOffset, collisionScale / 2, results, Quaternion.identity, _unitLayerMask);
            if (numberOfCollisions > 0)
            {
                Collider col = results[0];
                Module_Health moduleHealth = col.transform.GetComponent<Module_Health>();
                if (moduleHealth != null)
                {
                    HandleCollisionEffects(moduleHealth);
                }
            }
        }
        
        private void HandleCollisionEffects(Module_Health moduleHealth)
        {
            if (BarrelBase.IsBoss)
            {
                HeroAccess.Hero.GetComponent<Module_Health>().Die();
                Die();
                return;
            }
            if (!moduleHealth.CanDie) return;
            moduleHealth.Die();
            Die();
        }

        private void Die()
        {
            BarrelBase.ReturnToPool();
            IsActivated = false;
        }

        private void CheckCollisionForTextActivity(Vector3 position)
        {
            var isThereBarrelOnTheFront = Physics.Raycast(position + Vector3.up, -Vector3.forward, out _, textCheckDistance, _barrelLayerMask);
            BarrelBase.ChangeTextActivity(!isThereBarrelOnTheFront);
        }

#if UNITY_EDITOR
        [SerializeField]
        private bool showGizmos;
        
        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position - collisionOffset, collisionScale);
        }
#endif
    }
}