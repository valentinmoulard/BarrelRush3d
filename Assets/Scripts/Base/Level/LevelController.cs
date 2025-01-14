using System.Collections.Generic;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Ui;
using Game._LevelData;
using Game.Barrels;
using Game.Events;
using UnityEngine;

namespace Base.Level
{
    public class LevelController: MonoBehaviour
    {
        [SerializeField]
        private Spawner_Barrel barrelSpawner;

        [SerializeField]
        private Transform road;

        private int _bossCount;
        private float _roadSpeed;

        private void OnEnable()
        {
            Events_Boss.OnBossSpawned += OnBossSpawned;
            Events_Boss.OnBossDefeated += OnBossDefeated;
        }

        private void OnDisable()
        {
            Events_Boss.OnBossSpawned -= OnBossSpawned;
            Events_Boss.OnBossDefeated -= OnBossDefeated;
        }
        
        private void OnBossSpawned()
        {
            _bossCount++;
        }

        private void OnBossDefeated()
        {
            _bossCount--;
            if (_bossCount == 0)
            {
                ManagersAccess.GameStateController.SetState(UIScreenType.Win);
            }
            _roadSpeed += Settings_General.Instance.GameSettings.SettingsRoad.RoadSpeedIncreasePerBoss;
            DisableCurrentSection();
            ActivateCurrentSection();
        }

        private void DisableCurrentSection()
        {
            var currentSection = barrelSpawner.Sections.Dequeue();
            currentSection.ChangeBarrelActivity(false);
        }

        public void SetUp(List<BarrelLevelData> barrelLevelData)
        {
            barrelSpawner.SetUp(barrelLevelData);
            ActivateCurrentSection();
            _roadSpeed = Settings_General.Instance.GameSettings.SettingsRoad.RoadSpeed;
        }

        private void Update()
        {
            if (!ManagersAccess.GameStateController.IsPlaying) return;
            MoveObjects();
        }

        private void MoveObjects()
        {
            road.transform.position += Vector3.forward * (Time.deltaTime * -_roadSpeed);
        }
        
        private void ActivateCurrentSection()
        {
            if (barrelSpawner.Sections.Count < 1) return;
            var currentSection = barrelSpawner.Sections.Peek();
            currentSection.ChangeBarrelActivity(true);
        }
    }
}