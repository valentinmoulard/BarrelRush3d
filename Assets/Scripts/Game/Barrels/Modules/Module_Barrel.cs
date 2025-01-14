using System;
using UnityEngine;

namespace Game.Barrels.Modules
{
    public abstract class Module_Barrel: MonoBehaviour
    {
        protected Barrel_Base BarrelBase;
        protected bool IsActivated = false;

        public virtual void SetUpBarrelBoss(Barrel_Base bC)
        {
            BarrelBase = bC;
            IsActivated = true;
        }

        public virtual void Module_Update()
        {
            
        }
    }
}