using System;
using UnityEngine;

namespace Game.Modules
{
    public abstract class Module_Movement: ModuleBase
    {
        [field: SerializeField]
        public float Speed { get; private set; }

        [SerializeField]
        protected float touchSensitivity = 5;
        
        protected Vector3 TargetDirection { get; set; }
        
        protected readonly float INPUT_THRESHOLD = Mathf.Epsilon;

        public override void ModuleUpdate()
        {
            base.ModuleUpdate();
            HandleRotation();
        }
        
        private void HandleRotation()
        {
            if (TargetDirection.magnitude > INPUT_THRESHOLD)
            {
                Quaternion rotation = Quaternion.LookRotation(TargetDirection);
                transform.rotation = rotation;
            }
        }

        public override ModuleType GetModuleType()
        {
            return ModuleType.Movement;
        }
    }
}