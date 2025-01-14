using System;
using Base.GameManagement;
using Base.Helpers;
using Game._Hero;
using Game.Barrels.DataSos;
using UnityEngine;

namespace Game.Barrels.Modules
{
    public class Module_Barrel_Boss: Module_Barrel
    {
        private Transform HeroTransform => HeroAccess.Hero.transform;
        public void SetSkin(BarrelDataSo_Boss barrelDataSo)
        {
            var bossType = barrelDataSo.BossType;
            var bossPrefab = ManagersAccess.PoolManager.PoolGameSpecific.PoolBosses.GetObject(bossType);
            bossPrefab.transform.SetParent(transform);
            bossPrefab.transform.ResetTransformLocals();
            AdjustColliderSize();
        }

        private void AdjustColliderSize()
        {
            var boxCollider = gameObject.GetComponent<BoxCollider>();
            var size = boxCollider.size;
            size.x = 10;
            boxCollider.size = size;
        }

        public override void Module_Update()
        {
            base.Module_Update();
            if (!HeroTransform) return;
            MoveBossToHeroXPosition();
        }

        private void MoveBossToHeroXPosition()
        {
            var position = transform.position;
            var heroPositionX = HeroTransform.position.x;
            position.x = Mathf.Lerp(position.x, heroPositionX, Time.deltaTime * 2);
            transform.position = position;
        }
    }
}