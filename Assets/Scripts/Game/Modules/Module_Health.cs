using Base.GameManagement;
using Base.Particles;

namespace Game.Modules
{
    public abstract class Module_Health: ModuleBase
    {
        public bool CanDie { get; private set; }
        
        public void ChangeCanDie(bool canDie)
        {
            CanDie = canDie;
        }
        
        public virtual void Die()
        {
            if (!CanDie) return;
            ChangeCanDie(false);
            gameObject.SetActive(false);
            PlayDeathParticle();
        }
        
        protected void Revive()
        {
            gameObject.SetActive(true);
            ChangeCanDie(true);
        }
        
        private void PlayDeathParticle()
        {
            var particleTransform = transform;
            var position = particleTransform.position;
            position.y += 1;
            particleTransform.position = position;
            ManagersAccess.ParticleManager.PlayParticle(ParticleType.HeroDeath, particleTransform);
        }
        
        public override ModuleType GetModuleType()
        {
            return ModuleType.Health;
        }
    }
}