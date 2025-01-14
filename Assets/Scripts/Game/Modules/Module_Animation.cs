using System;
using Game._Hero;
using Game.Animations;
using UnityEngine;

namespace Game.Modules
{
    public abstract class Module_Animation: ModuleBase
    {
        private Animator _animator;
        private Animator Animator => _animator ? _animator : _animator = GetComponentInChildren<Animator>();
        private AnimationType _currentAnimationType;
        
        private readonly int _idle = Animator.StringToHash("Idle");
        private readonly int _walk = Animator.StringToHash("Walk");
        private readonly int _attack = Animator.StringToHash("Attack");
        private readonly int _die = Animator.StringToHash("Die");
        private readonly int _attackIndex = Animator.StringToHash("AttackIndex");
        private readonly int _attackSpeed = Animator.StringToHash("AttackSpeed");
        private readonly int _fly = Animator.StringToHash("Fly");

        public void PlayAnimation(AnimationType animationType)
        {
            // if (_currentAnimationType == animationType)
            // {
            //     if (this is not Module_Animation_Hero)
            //     {
            //         Debug.LogWarning("Attempting to play the same animation type. " + _currentAnimationType, this);
            //     }
            //     return;
            // }
            SetBool(_currentAnimationType, false);
            _currentAnimationType = animationType;
            SetBool(_currentAnimationType, true);
        }
        
        //
        // public void PlayAnimation(AnimationType animationType, int attackIndex)
        // {
        //     if (_currentAnimationType == animationType) return;
        //     SetBool(_currentAnimationType, false);
        //     _currentAnimationType = animationType;
        //     SetBool(_currentAnimationType, true);
        //     SetInt(attackIndex);
        // }

        public override ModuleType GetModuleType()
        {
            return ModuleType.Animation;
        }
        
        private void SetBool(AnimationType animationType, bool value)
        {
            var animName = GetAnimationName(animationType);
            Animator.SetBool(animName, value);
        }
        
        private void SetInt(int value)
        {
            Animator.SetInteger(_attackIndex, value);
        }
        
        public void SetFloat(float value)
        {
            Animator.SetFloat(_attackSpeed, value);
        }
        
        private int GetAnimationName(AnimationType animationType) //todo fix
        {
            return animationType switch
            {
                AnimationType.Idle => _idle,
                AnimationType.Walk => _walk,
                AnimationType.Attack => _attack,
                AnimationType.Die => _die,
                AnimationType.Celebrate => _idle,
                AnimationType.Empty => _idle,
                AnimationType.Fly => _fly,
                _ => throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null)
            };
        }
    }
}