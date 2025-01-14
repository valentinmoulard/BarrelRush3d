using System;
using System.Collections;
using System.Collections.Generic;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Helpers;
using Game._Slots;
using Game._Soldier;
using Game.Modules;
using UnityEditor;
using UnityEngine;

namespace Game._Hero
{
    public class Module_UnitHolder_Hero : ModuleBase
    {
        [field: SerializeField]
        public UnitySerializedDictionary<int, Slot_Base> Slots { get; private set; }

        [field: SerializeField]
        public UnitySerializedDictionary<int, Slot_Base> DroneSlots { get; private set; }

        private List<Module_Manager_Soldier> _activeSoldiers = new List<Module_Manager_Soldier>();

        private Coroutine _boundariesCoroutine;
        private Coroutine _squatBehaviourCoroutine;
        private float _roadLimit;
        public Vector2 SoldiersMaxXBoundaries { get; private set; }
        private float SoldierJumpDuration => Settings_General.Instance.GameSettings.SettingsSoldier.SoldierJoinAnimationDuration * 1.5f;
        private Module_Manager_Hero _hero;

        public override ModuleType GetModuleType()
        {
            return ModuleType.UnitHolder;
        }

        public override void ModuleAwake()
        {
            base.ModuleAwake();
            _hero = HeroAccess.Hero;
        }

        private void Start()
        {
            StartSquatLenghtOperations();
        }

        private void StartSquatLenghtOperations()
        {
            _roadLimit = Settings_General.Instance.GameSettings.SettingsRoad.RoadLimit;
            SoldiersMaxXBoundaries = new Vector2(-_roadLimit, _roadLimit);
            RestartTheBoundariesRoutine();
        }

        public void AddSoldier(Module_Manager_Soldier soldier)
        {
            var lowestIndexEmptySlot = soldier.IsDrone ? GetLowestIndexEmptySlot(DroneSlots) : GetLowestIndexEmptySlot(Slots);
            if (lowestIndexEmptySlot == null)
            {
                Debug.LogWarning("Attempting to add a soldier to a full holder.", this);
                return;
            }

            _activeSoldiers.Add(soldier);
            lowestIndexEmptySlot.SetSoldier(soldier);
            soldier.SetUp(this, lowestIndexEmptySlot);
            RestartTheBoundariesRoutine();
            RestartTheSquatBehaviourRoutine();
        }

        private Slot_Base GetLowestIndexEmptySlot(UnitySerializedDictionary<int, Slot_Base> slots)
        {
            var lowestIndex = int.MaxValue;
            Slot_Base lowestIndexSlot = null;

            foreach (var slotPair in slots)
            {
                if (!slotPair.Value.IsFull && slotPair.Key < lowestIndex)
                {
                    lowestIndex = slotPair.Key;
                    lowestIndexSlot = slotPair.Value;
                }
            }

            return lowestIndexSlot;
        }

        public void RemoveSoldier(Module_Manager_Soldier soldier)
        {
            if (soldier == null || !Slots.ContainsKey(soldier.Index))
            {
                Debug.LogWarning("Attempting to remove a soldier that doesn't exist.", this);
                return;
            }
            _activeSoldiers.Remove(soldier);
            var slot = Slots[soldier.Index];
            slot.SetSoldier(null);
            ReorganizeSquad();

            RestartTheBoundariesRoutine();
            RestartTheSquatBehaviourRoutine();

        }

        private void RestartTheBoundariesRoutine()
        {
            if (_boundariesCoroutine != null)
            {
                StopCoroutine(_boundariesCoroutine);
            }
            if(gameObject.activeInHierarchy)
                _boundariesCoroutine = StartCoroutine(CalculateMaxXPoseBoundariesCoroutine());
        }
        
        public void RestartTheSquatBehaviourRoutine(bool immediate = false)
        {
            if (_squatBehaviourCoroutine != null)
            {
                StopCoroutine(_squatBehaviourCoroutine);
            }
            if(gameObject.activeInHierarchy)
                _squatBehaviourCoroutine = StartCoroutine(ChangeSoldierBehaviourRoutine(immediate));
        }

        private IEnumerator ChangeSoldierBehaviourRoutine(bool immediate = false)
        {
            yield return immediate ? null : CoroutineExtensions.WaitForSeconds(SoldierJumpDuration);
            
            var state = _hero.HeroState;
            for (int i = _activeSoldiers.Count - 1; i >= 0; i--)
            {
                var soldier = _activeSoldiers[i];

                switch (state)
                {
                    case HeroState.Idle:
                        soldier.Idle();
                        break;
                    case HeroState.Attack:
                        soldier.Attack();
                        break;
                    case HeroState.Lose:
                        soldier.DieFromHero();
                        _activeSoldiers.RemoveAt(i);
                        break;
                    case HeroState.Win:
                        soldier.Celebrate();
                        break;
                    case HeroState.WaitForRevive:
                        soldier.WaitForRevive();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(state), state, null);
                }
            }
        }

        private void ReorganizeSquad()
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].Soldier == null)
                {
                    Module_Manager_Soldier nextSoldier = FindNextSoldier(i);

                    if (nextSoldier != null)
                        MoveSoldierToSlot(nextSoldier, Slots[i]);
                }
            }
        }

        private Module_Manager_Soldier FindNextSoldier(int startIndex)
        {
            for (int i = startIndex; i < Slots.Count; i += 2)
            {
                if (Slots[i].Soldier != null)
                    return Slots[i].Soldier;
            }

            if (startIndex + 1 > Slots.Count)
                return null;

            for (int i = startIndex + 1; i < startIndex; i += 2)
            {
                if (Slots[i].Soldier != null)
                    return Slots[i].Soldier;
            }

            return null;
        }

        private void MoveSoldierToSlot(Module_Manager_Soldier soldier, Slot_Base targetSlot)
        {
            var slot = Slots[soldier.Index];
            slot.SetSoldier(null);

            targetSlot.SetSoldier(soldier);

            soldier.SetIndex(targetSlot.Index);
            soldier.ReOrganizePosition(targetSlot.transform);
        }

        private IEnumerator CalculateMaxXPoseBoundariesCoroutine()
        {
            yield return CoroutineExtensions.WaitForSeconds(SoldierJumpDuration);
            CalculateMaxXPoseBoundaries();
        }

        private void CalculateMaxXPoseBoundaries()
        {
            if (_activeSoldiers == null || _activeSoldiers.Count == 0)
                return;

            float leftMaxDistFromHero = 0;
            float rightMaxDistFromHero = 0;
            float xPositionDiff = 0;

            for (int i = 0; i < _activeSoldiers.Count; i++)
            {
                xPositionDiff = _activeSoldiers[i].transform.position.x - transform.position.x;

                switch (xPositionDiff)
                {
                    case < 0:
                        {
                            if (leftMaxDistFromHero > xPositionDiff)
                                leftMaxDistFromHero = xPositionDiff;
                            break;
                        }
                    case > 0:
                        {
                            if (rightMaxDistFromHero < xPositionDiff)
                                rightMaxDistFromHero = xPositionDiff;
                            break;
                        }
                }
            }
            SoldiersMaxXBoundaries = new Vector2(-_roadLimit - leftMaxDistFromHero, _roadLimit - rightMaxDistFromHero);
        }

        #region UNITY_EDITOR

#if UNITY_EDITOR

        [ContextMenu("GetSlots")]
        private void GetSlots()
        {
            Slots.Clear(); // Ensure the dictionary is cleared before refilling
            var index = 0;
            foreach (var slot in GetComponentsInChildren<Slot_Base>(true))
            {
                if (!Slots.ContainsKey(index))
                {
                    Slots.Add(index, slot);
                    slot.SetIndex(index);
                    index++;
                }
            }

            if (Slots.Count > 0)
            {
                EditorUtility.SetDirty(gameObject);
                EditorUtility.SetDirty(this);
            }
        }
#endif

        #endregion
    }
}