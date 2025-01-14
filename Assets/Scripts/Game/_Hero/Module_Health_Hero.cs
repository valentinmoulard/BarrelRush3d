using Base.GameManagement;
using Base.Ui;
using Game.Events;
using Game.Modules;

namespace Game._Hero
{
    public class Module_Health_Hero : Module_Health
    {
        private bool _canRevive = true;

        public override void ModuleStart()
        {
            base.ModuleStart();
            Events_Revive.OnRevive += OnRevive;
        }

        public override void ModuleDestroy()
        {
            base.ModuleDestroy();
            Events_Revive.OnRevive -= OnRevive;
        }

        public override void Die()
        {
            base.Die();
            if (false) //todo fix
            {
                ManagersAccess.GameStateController.SetState(UIScreenType.WaitingForRevive);
            }
            else
            {
                ManagersAccess.GameStateController.SetState(UIScreenType.Lose);
                var hero = GetModuleManager<Module_Manager_Hero>();
                hero.ChangeState(HeroState.Lose);
            }
        }

        private void OnRevive(bool isRevived)
        {
            _canRevive = false;

            if (isRevived)
            {
                Revive();
            }
            else
            {
                Die();
            }
        }
    }
}