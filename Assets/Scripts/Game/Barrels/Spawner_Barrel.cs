using System.Collections.Generic;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Game._LevelData;
using Game.Barrels.DataSos;
using Game.Events;
using UnityEngine;

namespace Game.Barrels
{
    public class Spawner_Barrel : MonoBehaviour
    {
        private float _lastBarrelZPosition;
        public Queue<Section_Base> Sections { get; set; } = new();
        public void SetUp(List<BarrelLevelData> barrelLevelData)
        {
            CreateBarrels(barrelLevelData);
            foreach (var section in Sections)
            {
                section.SetUp();
            }
        }

        private void CreateBarrels(List<BarrelLevelData> barrelLevelData)
        {
            var gameSettings = Settings_General.Instance.GameSettings;
            var roadSettings = gameSettings.SettingsRoad;
            var barrelSettings = gameSettings.SettingsBarrel;
            var basePosition = transform.position;
            var barrelZGap = barrelSettings.BarrelZGap;
            var barrelXPositions = roadSettings.BarrelXPositions;
            var numXPositions = barrelXPositions.Length;
            var gapBetweenSections = roadSettings.GapBetweenSections;
            var bossBarrelDistanceToLastBarrel = roadSettings.BossBarrelDistanceToLastBarrel;

            for (int sectionIndex = 0; sectionIndex < barrelLevelData.Count; sectionIndex++)
            {
                var sectionHolder = CreateSectionHolder(sectionIndex, basePosition);
                var barrelSpawnData = barrelLevelData[sectionIndex].BarrelSpawnDataSo.DefaultBarrelSquat;
                
                int barrelCount = 0;
                barrelCount = CreateBarrelsInSection(barrelSpawnData, sectionHolder, barrelCount, barrelXPositions, barrelZGap, numXPositions);
                
                CreateBarrelBoss(barrelLevelData, barrelCount, numXPositions, barrelZGap, sectionIndex, sectionHolder, bossBarrelDistanceToLastBarrel);

                _lastBarrelZPosition += gapBetweenSections;
            }
        }

        private void CreateBarrelBoss(List<BarrelLevelData> barrelLevelData, int barrelCount, int numXPositions, float barrelZGap,
            int sectionIndex, GameObject sectionHolder, float bossBarrelDistanceToLastBarrel)
        {
            var group = barrelCount / numXPositions;
            _lastBarrelZPosition += (barrelZGap * group);
            var objectAndCountPairBoss = barrelLevelData[sectionIndex].ObjectAndCountPairBoss;
            var healthPoints = objectAndCountPairBoss.GetRandomHealth;
            var bossBarrel = SpawnBarrel(objectAndCountPairBoss.BarrelDataSo, sectionHolder.transform, healthPoints);
            var sectionHolderZGap = 10;
            bossBarrel.transform.position = new Vector3(0, 0, _lastBarrelZPosition + bossBarrelDistanceToLastBarrel + sectionHolderZGap);
            Events_Boss.OnBossSpawned?.Invoke();
        }

        private int CreateBarrelsInSection(ObjectAndCountPair_Default[] barrelSpawnData, GameObject sectionHolder, int barrelCount,
            float[] barrelXPositions, float barrelZGap, int numXPositions)
        {
            foreach (var barrelDataPair in barrelSpawnData)
            {
                for (int barrelIndex = 0; barrelIndex < barrelDataPair.HowManyObject; barrelIndex++)
                {
                    var healthPoints = barrelDataPair.GetRandomHealth;
                    var barrel = SpawnBarrel(barrelDataPair.BarrelDataSo, sectionHolder.transform, healthPoints);
                    PositionBarrel(barrel, barrelCount, barrelXPositions, barrelZGap, numXPositions);
                    barrelCount++;
                }
            }

            return barrelCount;
        }

        private GameObject CreateSectionHolder(int sectionIndex, Vector3 basePosition)
        {
            var sectionHolder = new GameObject().AddComponent<Section_Base>();
            sectionHolder.name = "Section " + sectionIndex;
            Sections.Enqueue(sectionHolder);
            sectionHolder.transform.SetParent(transform);
            sectionHolder.transform.position = basePosition + new Vector3(0, 0, _lastBarrelZPosition);
            return sectionHolder.gameObject;
        }

        private Barrel_Base SpawnBarrel(BarrelDataSo barrelData, Transform parent, int healthPoints)
        {
            var barrel = ManagersAccess.PoolManager.PoolGameSpecific.PoolBarrel.GetObject();
            barrel.SetData(parent, barrelData, healthPoints);
            return barrel;
        }

        private void PositionBarrel(Barrel_Base barrel, int barrelCount, float[] barrelXPositions, float barrelZGap, int numXPositions)
        {
            var xPosition = barrelXPositions[barrelCount % numXPositions];
            var group = barrelCount / numXPositions;
            var zPosition = barrelZGap * group;
            barrel.transform.localPosition = new Vector3(xPosition, 0, zPosition);
        }
    }

    public class Section_Base: MonoBehaviour
    {
        private Barrel_Base[] _barrels;

        public void SetUp()
        {
            _barrels = GetComponentsInChildren<Barrel_Base>(true);
        }
        
        public void ChangeBarrelActivity(bool active)
        {
            foreach (var barrel in _barrels)
            {
                barrel.SetActive(active);
            }
        }
    }
}
