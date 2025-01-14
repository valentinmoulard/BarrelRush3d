using Game._Soldier;
using UnityEngine;

namespace Game.AddOns
{
    public class AddOn_Soldier: AddOn_Base
    {
        [field: SerializeField]
        public Module_Manager_Soldier Soldier { get; private set; }
        
        protected override Transform GfxTransform()
        {
            return Soldier.Gfx;
        }
    }
}