using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Clothes
{
    [Serializable]
    public class ClothesData
    {
        [SerializeField, ListDrawerSettings(ShowIndexLabels = true), ReadOnly]
        private List<ClothesDataValue> values = new ();
        
        public float GetClotheDataValue(ClotheDataValueType value, int level)
        {
            var clampedLevel = Mathf.Clamp(level, 0, values.Count - 1);
            switch (value)
            {
                case ClotheDataValueType.BonusAttackDamage:
                    return values[clampedLevel].BonusAttackDamage;
                case ClotheDataValueType.BonusAttackSpeed:
                    return values[clampedLevel].BonusAttackSpeed;
                case ClotheDataValueType.ProfitMultiplier:
                    return values[clampedLevel].ProfitMultiplier;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }
        
#if UNITY_EDITOR
        [Button]
        private void SetValues(int howManyValues, float minDamage, float maxDamage, float minAttackSpeed, float maxAttackSpeed, float minProfit, float maxProfit)
        {
            values = new List<ClothesDataValue>();
            for (int i = 0; i < howManyValues; i++)
            {
                var value = new ClothesDataValue
                {
                    BonusAttackDamage = Mathf.Lerp(minDamage, maxDamage, (float)i / (howManyValues - 1)),
                    BonusAttackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, (float)i / (howManyValues - 1)),
                    ProfitMultiplier = Mathf.Lerp(minProfit, maxProfit, (float)i / (howManyValues - 1))
                };
                values.Add(value);
            }
        }
#endif
    }
    
    [Serializable]
    public class ClothesDataValue
    {
        [field: SerializeField]
        public float BonusAttackDamage { get; set; }

        [field: SerializeField]
        public float BonusAttackSpeed { get; set; }

        [field: SerializeField]
        public float ProfitMultiplier { get; set; }
    }

    public enum ClotheDataValueType
    {
        BonusAttackDamage,
        BonusAttackSpeed,
        ProfitMultiplier
    }
}