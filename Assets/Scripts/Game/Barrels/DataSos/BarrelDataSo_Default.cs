using Game.AddOns;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Barrels.DataSos
{
    [CreateAssetMenu(fileName = "BarrelDataSo_Default", menuName = "BarrelRush/BarrelSpawner/BarrelDataSo_Default", order = 0)]
    public class BarrelDataSo_Default: BarrelDataSo
    {
        [field: SerializeField, OnValueChanged("SetVariables")]
        public bool CanExplode { get; private set; }

        [field: SerializeField, HideIf("CanExplode")]
        public bool HasAddOn { get; private set; }

        [field: SerializeField, ShowIf("HasAddOn")]
        public AddOn_Base AddOnObject { get; set; }

        #region OdinFunctions

        /// <summary>
        /// Odin function that is called when the value of the variable is changed in the inspector.
        /// </summary>
        public void SetVariables()
        {
            if (CanExplode)
            {
                HasAddOn = false;
                AddOnObject = null;
            }
        }

        #endregion
    }
}