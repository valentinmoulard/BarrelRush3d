using Game._Soldier;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game._Slots
{
    public class Slot_Base : MonoBehaviour
    {
        [field: SerializeField, ReadOnly]
        public int Index { get; private set; }
        
        [field: SerializeField, ReadOnly]
        public Module_Manager_Soldier Soldier { get; private set; }
        public bool IsFull => Soldier != null;

        public void SetSoldier(Module_Manager_Soldier soldier)
        {
            if (IsFull && soldier != null)
            {
                Debug.LogWarning("Attempting to set a soldier to a full slot.", this);
                return;
            }
            if(soldier == null)
                Soldier.transform.SetParent(null);
            Soldier = soldier;
        }

#if UNITY_EDITOR
        [Button]
        public void SetIndex(int index)
        {
            Index = index;
        }
#endif
    }
}