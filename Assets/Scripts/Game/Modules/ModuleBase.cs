using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Modules
{
    public abstract class ModuleBase: MonoBehaviour, IModule
    {
        protected Module_Manager ModuleManager;
        protected T GetModuleManager<T>() where T : Module_Manager
        {
            return (T) ModuleManager;
        }

        [field: SerializeField, BoxGroup("Base")]
        public bool IsActivate { get; set; } = true;

        [field: SerializeField, BoxGroup("Base")]
        public bool HasUpdate { get; private set; } = false;

        [field: SerializeField, BoxGroup("Base")]
        public bool HasFixedUpdate { get; private set; } = false;
        
        public virtual void ModuleAwake() { }
        public virtual void ModuleStart() { }
        public virtual void ModuleEnable() { }
        public virtual void ModuleUpdate() { }
        public virtual void ModuleFixedUpdate() { }
        public virtual void ModuleDisable() { }
        public virtual void ModuleDestroy() { }

        public virtual void Initialize(Module_Manager manager)
        {
            ModuleManager = manager;
        }
        
        public virtual void ResetModule()
        {
            IsActivate = false;
        }

        public virtual ModuleType GetModuleType()
        {
            throw new NotImplementedException();
        }
    }
}