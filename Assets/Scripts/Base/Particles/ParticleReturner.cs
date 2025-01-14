using System;
using UnityEngine;

namespace Game.Pools.Particle
{
    public class ParticleReturner: MonoBehaviour
    {
        private Action _onDisable;
        public void Init(Action onDisable)
        {
            _onDisable = onDisable;
        }

        private void OnDisable()
        {
            _onDisable?.Invoke();
        }
    }
}