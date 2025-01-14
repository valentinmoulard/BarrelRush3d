using Game.Modules;

namespace Game._Soldier
{
    public class Module_Health_Soldier: Module_Health
    {
        public override void Die()
        {
            base.Die();
            var soldierController = GetModuleManager<Module_Manager_Soldier>();
            soldierController.Die();
        }
    }
}