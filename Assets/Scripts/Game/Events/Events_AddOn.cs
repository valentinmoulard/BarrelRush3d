using System;
using Game.AddOns;
using Game.SaveableSos;
namespace Game.Events
{
    public static class Events_AddOn
    {
        public static Action<AddOn_Base> OnAddActivated;
        public static Action<SaveableSo_Equipment> OnEquipmentEquipped;
        public static Action OnUpdateEquipmentState;
        public static Action OnInventoryUpdated;
    }
}