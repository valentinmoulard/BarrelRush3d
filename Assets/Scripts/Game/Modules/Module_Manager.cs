using System.Linq;
using Base.GameManagement;
using Base.Helpers;
using Game.Animations;
using UnityEditor;
using UnityEngine;

namespace Game.Modules
{
    public abstract class Module_Manager : MonoBehaviour
    {
        [SerializeField]
        private UnitySerializedDictionary<ModuleType, ModuleBase> modules = new ();
        private Module_Animation _animationModule;
        protected T GetModule<T>(ModuleType moduleType) where T : ModuleBase
        {
            if (modules.TryGetValue(moduleType, out var module) && module is T typedModule)
            {
                return typedModule;
            }
            return null;
        }
        
        protected virtual void Awake()
        {
            foreach (var module in modules.Values)
            {
                module.Initialize(this);
                module.ModuleAwake();
            }
            _animationModule = GetModule<Module_Animation>(ModuleType.Animation);
        }
        
        protected virtual void OnEnable()
        {
            foreach (var module in modules.Values)
            {
                module.ModuleEnable();
            }
        }

        protected virtual void Start()
        {
            foreach (var module in modules.Values)
            {
                module.ModuleStart();
            }
        }

        protected virtual void Update()
        {
            if (!ManagersAccess.GameStateController.IsPlaying) return;
            foreach (var module in modules.Values)
            {
                if(module.HasUpdate && module.IsActivate)
                    module.ModuleUpdate();
            }
        }
        
        protected virtual void FixedUpdate()
        {
            if (!ManagersAccess.GameStateController.IsPlaying) return;
            foreach (var module in modules.Values)
            {
                if(module.HasFixedUpdate && module.IsActivate)
                    module.ModuleFixedUpdate();
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var module in modules.Values)
            {
                module.ModuleDisable();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var module in modules.Values)
            {
                module.ModuleDestroy();
            }
        }
        
        public virtual void ResetAllModules()
        {
            foreach (var module in modules.Values)
            {
                module.ResetModule();
            }
        }

        protected void SetAllModulesActive(bool b, params ModuleType[] excludeModuleTypes)
        {
            foreach (var module in modules.Values)
            {
                var isExcluded = excludeModuleTypes.Contains(module.GetModuleType());
                module.IsActivate = isExcluded ? !b : b;
            }
        }

        public void Attack()
        {
            _animationModule?.PlayAnimation(AnimationType.Walk);
            GetModule<Module_Attack>(ModuleType.Attack).Attack();
        }
        
        private void StopAttack()
        {
            _animationModule?.PlayAnimation(AnimationType.Idle);
            GetModule<Module_Attack>(ModuleType.Attack).StopAttack();
        }
        
        public virtual void Die()
        {
            ResetAllModules();
        }

        public void Idle()
        {
            StopAttack();
        }
        
        public void Celebrate()
        {
            StopAttack();
        }
        
        public void WaitForRevive()
        {
            StopAttack();
        }

        #region UNITY_EDITOR

#if UNITY_EDITOR

        [ContextMenu("GetModules")]
        private void GetModules()
        {
            this.modules.Clear();
            var modules = GetComponentsInChildren<ModuleBase>(true);
            foreach (var module in modules)
            {
                this.modules.Add(module.GetModuleType(), module);
            }
            EditorUtility.SetDirty(gameObject);
            EditorUtility.SetDirty(this);
        }
#endif

        #endregion
    }
}