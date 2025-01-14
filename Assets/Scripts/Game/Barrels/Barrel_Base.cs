using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Helpers;
using Game.AddOns;
using Game.Barrels.DataSos;
using Game.Barrels.Modules;
using Game.Events;
using Game.Pools;
using Game.Settings;
using UnityEngine;

namespace Game.Barrels
{
    public class Barrel_Base: MonoBehaviour, IPoolable
    {
        [SerializeField]
        private MeshRenderer barrelRenderer;

        [SerializeField]
        private Module_Barrel_Health health;

        [SerializeField]
        private Module_Barrel_Collide barrelCollision;

        public bool IsBoss { get; private set; }
        private bool _isActivated;
        private int _healthPoints;
        private bool _hasAddOn;
        private AddOn_Base _addOnObject;
        private int _barrelRotateSpeed = 100;
        private BarrelDataSo _barrelDataSo;
        private Settings_Barrel _barrelSettings;
        private Transform _addOnObjectTransform;
        private Module_Barrel_Boss _bossModule;

        private T BarrelDataSo<T>() where T : BarrelDataSo
        {
            return _barrelDataSo as T;
        }

        public void SetData(Transform parent, BarrelDataSo barrelDataSo, int healthPoints)
        {
            _barrelSettings ??= Settings_General.Instance.GameSettings.SettingsBarrel;
            _barrelRotateSpeed = _barrelSettings.BarrelRotateSpeed;
            
            _barrelDataSo = barrelDataSo;
            IsBoss = barrelDataSo is BarrelDataSo_Boss;
            _healthPoints = healthPoints;
            
            transform.SetParent(parent);
            SetAddOn();
            SetUpModules();
            SetBarrelRenderer();
            SetIfHasBoss(barrelDataSo);
        }

        private void SetIfHasBoss(BarrelDataSo barrelDataSo)
        {
            if (!IsBoss) return;
            Destroy(barrelRenderer.gameObject);
            
            var bossData = barrelDataSo as BarrelDataSo_Boss;
            if (!_bossModule)
            {
                _bossModule = gameObject.AddComponent<Module_Barrel_Boss>();
            }
            _bossModule.SetUpBarrelBoss(this);
            _bossModule.SetSkin(bossData);
        }

        private void SetAddOn()
        {
            if(IsBoss) return;
            var barrelDataSo = BarrelDataSo<BarrelDataSo_Default>();
            _hasAddOn = barrelDataSo.HasAddOn;
            if (!_hasAddOn) return;
            _addOnObjectTransform = Instantiate(_barrelSettings.AddOnObject.transform, transform);
            _addOnObject = Instantiate(barrelDataSo.AddOnObject, _addOnObjectTransform);
            _addOnObject.transform.ResetTransformLocals();
            _addOnObject.ChangeSize(true);
        }
        
        public void ActivateAddOn()
        {
            if (!_hasAddOn) return;
            Events_AddOn.OnAddActivated?.Invoke(_addOnObject);
        }

        private void SetBarrelRenderer()
        {
            if(IsBoss) return;
            var barrelDataSo = BarrelDataSo<BarrelDataSo_Default>();
            if (barrelDataSo.CanExplode)
            {
                barrelRenderer.material = _barrelSettings.ExplodeMaterial;
                Instantiate(_barrelSettings.ExplodeSkullPrefab, transform);
            }
        }

        private void SetUpModules()
        {
            var modules = GetComponentsInChildren<Module_Barrel>();
            foreach (var module in modules)
            {
                module.SetUpBarrelBoss(this);
            }
            var canExplode = !IsBoss && BarrelDataSo<BarrelDataSo_Default>().CanExplode;
            health.SetUp(_healthPoints, canExplode);
            barrelCollision.SetUp();
        }

        private void Update()
        {
            if (!ManagersAccess.GameStateController.IsPlaying) return;
            if (!_isActivated) return;
            UpdateModules();
            RotateTheBarrel();
        }

        private void UpdateModules()
        {
            if(_bossModule)
                _bossModule.Module_Update();
            if(barrelCollision)
                barrelCollision.Module_Update();
        }

        private void RotateTheBarrel()
        {
            if (IsBoss) return;
            barrelRenderer.transform.Rotate(Vector3.left * (Time.deltaTime * _barrelRotateSpeed));
        }

        public void Death()
        {
            if(IsBoss)
                Events_Boss.OnBossDefeated?.Invoke();
        }
        
        public Transform GetHitAnimationTransform()
        {
            return IsBoss ? null : barrelRenderer.transform;
        }
        
        public void ChangeTextActivity(bool isActive)
        {
            if(IsBoss) return;
            health.ChangeTextActivity(isActive);
        }

        public void SetActive(bool isActive)
        {
            _isActivated = isActive;
        }
    }
}