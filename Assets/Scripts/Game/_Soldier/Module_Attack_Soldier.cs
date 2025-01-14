using Game.FightSystem;
using Game.Modules;

namespace Game._Soldier
{
    public class Module_Attack_Soldier: Module_Attack
    {
        public override void Attack()
        {
            var soldier = GetModuleManager<Module_Manager_Soldier>();
            var soldierDataSo = soldier.SoldierDataSo;
            var fightData = soldierDataSo.FightData;
            var level = soldierDataSo.UpgradeIndex;
            SetAttackVariables(fightData.GetFightDataValue(FightDataValueType.AttackSpeed, level), fightData.GetFightDataValue(FightDataValueType.AttackDamage, level));
            base.Attack();
        }
    }
}