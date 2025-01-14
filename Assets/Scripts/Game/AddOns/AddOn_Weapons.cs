using Game.Weapons;
using UnityEngine;

namespace Game.AddOns
{
    public class AddOn_Weapons: AddOn_Base
    {
        [field: SerializeField]
        public Weapon_Base Weapon { get; private set; }
        
        protected override Transform GfxTransform()
        {
            return Weapon.transform;
        }
    }
}