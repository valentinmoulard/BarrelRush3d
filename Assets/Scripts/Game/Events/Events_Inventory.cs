using System;

namespace Game.Events
{
    public static class Events_Inventory
    {
        public static Action<float, float, float> OnInventoryUpdated;
    }
}