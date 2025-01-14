using Base.GameManagement;
using Base.Particles;
using Game.Pools.Particle;
using UnityEngine;

namespace Base.Managers
{
    public class Manager_Particle: ManagerBase
    {
        public void PlayParticle(ParticleType particleType, Transform position)
        {
            var particle = ManagersAccess.PoolManager.PoolParticle.GetObject((int)particleType);
            if(particle == null) return;
            SetUpParticle(particleType, particle, position);
            particle.Play();
        }

        private void SetUpParticle(ParticleType particleType, ParticleSystem particle, Transform position)
        {
            particle.transform.position = position.position;
            var particleReturner = TrySetParticleReturner(particle);
            particleReturner.Init(() => ManagersAccess.PoolManager.PoolParticle.ReturnObject(particle, (int)particleType));
        }

        private static ParticleReturner TrySetParticleReturner(ParticleSystem particle)
        {
            var particleReturner = particle.GetComponent<ParticleReturner>();

            if (particleReturner == null)
                particleReturner = particle.gameObject.AddComponent<ParticleReturner>();
            return particleReturner;
        }
    }
}