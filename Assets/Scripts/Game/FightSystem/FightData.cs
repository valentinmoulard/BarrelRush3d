using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.FightSystem
{
    [Serializable]
    public class FightData
    {
        [SerializeField, ListDrawerSettings(ShowIndexLabels = true), ReadOnly]
        private List<FightDataValue> values = new();
        
        public float GetFightDataValue(FightDataValueType value, int level)
        {
            var clampedLevel = Mathf.Clamp(level, 0, values.Count - 1);
            switch (value)
            {
                case FightDataValueType.AttackDamage:
                    return values[clampedLevel].AttackDamage;
                case FightDataValueType.AttackSpeed:
                    return values[clampedLevel].AttackSpeed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

#if UNITY_EDITOR
        [Button]
        private void SetValues(int howManyValues, float minDamage, float maxDamage, float minAttackSpeed, float maxAttackSpeed)
        {
            values = new List<FightDataValue>();
            for (int i = 0; i < howManyValues; i++)
            {
                var value = new FightDataValue
                {
                    AttackDamage = Mathf.Lerp(minDamage, maxDamage, (float)i / (howManyValues - 1)),
                    AttackSpeed = Mathf.Lerp(minAttackSpeed, maxAttackSpeed, (float)i / (howManyValues - 1))
                };
                values.Add(value);
            }
        }
#endif
    }

    [Serializable]
    public class FightDataValue
    {
        [field: SerializeField]
        public float AttackDamage { get; set; }

        [field: SerializeField]
        public float AttackSpeed { get; set; }
    }
    
    public enum FightDataValueType
    {
        AttackDamage,
        AttackSpeed
    }
}