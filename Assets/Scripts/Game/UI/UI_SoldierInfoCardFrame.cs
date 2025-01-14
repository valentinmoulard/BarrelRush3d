using Game._Soldier;
using Game.FightSystem;

namespace Game.Ui
{
    public class UI_SoldierInfoCardFrame : UI_InfoCardFrame<SoldierDataSo>
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
        protected override void UpdateLevelText(string levelString)
        {
            stringBuilder.Clear();
            stringBuilder.Append("Level ");
            stringBuilder.Append(levelString);
            levelText.text = stringBuilder.ToString();
        }
    }
}