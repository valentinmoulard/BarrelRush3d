using System.Text;
using Game.Events;
using TMPro;
using UnityEngine;

namespace Game.Ui
{
    public class UI_StatsHolder: MonoBehaviour
    {
        [SerializeField]
        private TMP_Text damage;
        
        [SerializeField]
        private TMP_Text speed;
        
        [SerializeField]
        private TMP_Text coinMultiplier;
        
        private StringBuilder _stringBuilder = new();

        private void Awake()
        {
            Events_Inventory.OnInventoryUpdated += SetStatsTexts;
        }

        private void OnDestroy()
        {
            Events_Inventory.OnInventoryUpdated -= SetStatsTexts;
        }
        
        private void UpdateTextField(TMP_Text textField, string label, float value, string format, string suffix = "")
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.AppendFormat(format, value);
            _stringBuilder.Append(suffix);
            textField.text = _stringBuilder.ToString();
        }

        private void SetStatsTexts(float attackSpeed, float attackDamage, float profitMultiplier)
        {
            UpdateTextField(speed, "Speed: ", attackSpeed, "{0:F2}", "s");
            UpdateTextField(damage, "Damage: ", attackDamage, "{0:F2}");
            UpdateTextField(coinMultiplier, "Profit Multiplier: ", profitMultiplier, "x{0:F2}");
        }
    }
}