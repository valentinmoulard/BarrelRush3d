using Game.Clothes;
using Base.GameManagement;

namespace Game.Ui
{
    public class UI_ClotheInfoCardFrame : UI_EquipmentInfoCardFrame<ClothesDataSo>
    {
        protected override void UpdateStatsText(int level)
        {
            base.UpdateStatsText(level);

            var firePowerAsset = settingsIconHolder.FireDamage;
            var fireSpeedAsset = settingsIconHolder.FireSpeed;
            var profitMultiplierAsset = settingsIconHolder.IncomeMultiplier;
            stringBuilder.Clear();
            stringBuilder.Append(firePowerAsset);
            stringBuilder.Append(dataSo.ClothesData.GetClotheDataValue(ClotheDataValueType.BonusAttackDamage, level).ToString("F1"));
            stringBuilder.Append("   ");
            stringBuilder.Append(fireSpeedAsset);
            stringBuilder.Append(dataSo.ClothesData.GetClotheDataValue(ClotheDataValueType.BonusAttackSpeed, level).ToString("F1"));
            stringBuilder.Append(" ");
            stringBuilder.Append(profitMultiplierAsset);
            stringBuilder.Append(" x");
            stringBuilder.Append(dataSo.ClothesData.GetClotheDataValue(ClotheDataValueType.ProfitMultiplier, level).ToString("F1"));
            statsText.text = stringBuilder.ToString();
        }
    }
}