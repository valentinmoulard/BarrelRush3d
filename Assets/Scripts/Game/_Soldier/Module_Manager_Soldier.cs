using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Particles;
using DG.Tweening;
using Game._Hero;
using Game._Slots;
using Game.Animations;
using Game.Modules;
using Game.Pools;
using Game.Settings;
using UnityEngine;

namespace Game._Soldier
{
    public class Module_Manager_Soldier: Module_Manager, IPoolable
    {
        [field: SerializeField]
        public SoldierDataSo SoldierDataSo { get; private set; }
        
        [field: SerializeField]
        public bool IsDrone { get; private set; }

        [field: SerializeField]
        public Transform Gfx { get; private set; }
        public int Index { get; private set; }
        private Sequence _setTransformSequence;
        private Settings_Soldier _settingsSoldier;
        private Module_UnitHolder_Hero _unitHolderHero;
        private Module_Health_Soldier _healthModule;
        private Module_Animation_Soldier _animationModule;
        public void SetUp(Module_UnitHolder_Hero unitHolder, Slot_Base slot)
        {
            _settingsSoldier ??= Settings_General.Instance.GameSettings.SettingsSoldier;
            _healthModule ??= GetModule<Module_Health_Soldier>(ModuleType.Health);
            _animationModule ??= GetModule<Module_Animation_Soldier>(ModuleType.Animation);
            ChangeCanDie(false);
            _unitHolderHero ??= unitHolder;
            SetIndex(slot.Index);
            SetTransform(slot.transform);
        }

        public void SetIndex(int newIndex)
        {
            Index = newIndex;
        }

        private void SetTransform(Transform slotTransform)
        {
            transform.SetParent(slotTransform);
            _setTransformSequence?.Kill();
            _setTransformSequence = SetTransformTween();
            _setTransformSequence.Play();
        }

        private Sequence SetTransformTween()
        {
            var duration = _settingsSoldier.SoldierJoinAnimationDuration;
            var jumpPower = _settingsSoldier.SoldierJoinJumpPower;
            _animationModule.PlayAnimation(AnimationType.Fly);
            _setTransformSequence = DOTween.Sequence();
            _setTransformSequence.Append(transform.DOLocalJump(Vector3.zero, jumpPower, 1, duration));
            _setTransformSequence.AppendCallback(() =>
            {
                ChangeCanDie(true);
                ManagersAccess.ParticleManager.PlayParticle(ParticleType.AddSoldier, transform);
                transform.localScale = Vector3.zero;
                transform.rotation = Quaternion.identity;
                _animationModule.PlayAnimation(AnimationType.Walk);
            });
            _setTransformSequence.Join(transform.DOScale(Vector3.one, duration / 2));
            return _setTransformSequence;
        }

        public void ReOrganizePosition(Transform slotTransform)
        {
            _setTransformSequence?.Kill();
            _animationModule.PlayAnimation(AnimationType.Walk);
            transform.SetParent(slotTransform);
            transform.localScale = Vector3.one;
            transform.rotation = Quaternion.identity;
            ChangeCanDie(true);
            var duration = _settingsSoldier.SoldierJoinAnimationDuration;
            _setTransformSequence = DOTween.Sequence();
            _setTransformSequence.Append(transform.DOLocalJump(Vector3.zero, 0, 1, duration));
        }

        public override void Die()
        {
            base.Die();
            _unitHolderHero.RemoveSoldier(this);
            this.ReturnToPool();
        }

        public void DieFromHero()
        {
            base.Die();
            this.ReturnToPool();
        }

        private void ChangeCanDie(bool canDie)
        {
            _healthModule.ChangeCanDie(canDie);
        }
    }
}