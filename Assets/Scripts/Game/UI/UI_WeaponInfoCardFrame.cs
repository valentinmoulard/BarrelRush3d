using Game.FightSystem;
using Game.Weapons;

namespace Game.Ui
{
    public class UI_WeaponInfoCardFrame : UI_EquipmentInfoCardFrame<WeaponDataSo>
    {
        /*
        protected override void UpdateStatsText(int level)
        {
            base.UpdateStatsText(level);

            var firePowerAsset = settingsIconHolder.FireDamage;
            var fireSpeedAsset = settingsIconHolder.FireSpeed;
            stringBuilder.Clear();
            stringBuilder.Append(firePowerAsset);
            stringBuilder.Append(dataSo.FightData.GetFightDataValue(FightDataValueType.AttackDamage, level).ToString("F1"));
            stringBuilder.Append("   ");
            stringBuilder.Append(fireSpeedAsset);
            stringBuilder.Append(dataSo.FightData.GetFightDataValue(FightDataValueType.AttackSpeed, level).ToString("F1"));
            statsText.text = stringBuilder.ToString();
        }
        */
    }
}