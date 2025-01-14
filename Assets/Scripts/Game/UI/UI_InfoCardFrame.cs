using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Base.GameManagement.Settings;
using Game.Settings;
using Game.Ui.Buttons;
using Base.Events;
using System.Text;
using Game.SaveableSos;
using Base.GameManagement;

namespace Game.Ui
{
    public abstract class UI_InfoCardFrame<T> : MonoBehaviour where T : SaveableSo
    {
        [SerializeField]
        protected T dataSo = null;

        [SerializeField]
        protected Image icon = null;

        [SerializeField]
        protected TMP_Text nameText = null;

        [SerializeField]
        protected TMP_Text levelText = null;

        [SerializeField]
        protected TMP_Text statsText = null;

        [SerializeField]
        protected Button_PurchaseUpgrade upgradeButton = null;

        [SerializeField]
        protected TMP_Text upgradeButtonText = null;

        protected Settings_IconHolder settingsIconHolder;
        protected StringBuilder stringBuilder = new();

        protected virtual void Awake()
        {
            settingsIconHolder = Settings_General.Instance.GameSettings.SettingsIconHolder;
        }
        
        protected virtual void OnEnable()
        {
            EventsCoin.OnCoinChanged += OnCoinChanged;
            upgradeButton.OnClickPurchaseUpgradeButton += TryPurchaseUpgrade;
            UpdateInfoCard();
        }

        protected virtual void OnDisable()
        {
            EventsCoin.OnCoinChanged -= OnCoinChanged;
            upgradeButton.OnClickPurchaseUpgradeButton -= TryPurchaseUpgrade;
        }

        private void OnCoinChanged()
        {
            UpdateInfoCard();
        }

        private void TryPurchaseUpgrade()
        {
            if (dataSo == null)
            {
                Debug.LogError("No data referenced!", gameObject);
                return;
            }

            float upgradeCost = dataSo.GetCost(dataSo.UpgradeIndex);
            bool canPurchaseUpgrade = ManagersAccess.CoinManager.HasEnoughtCoin(upgradeCost);

            if (canPurchaseUpgrade)
            {
                dataSo.UpgradeIndex++;
                ManagersAccess.CoinManager.ReduceCoin(upgradeCost);
            }
        }

        protected virtual void UpdateInfoCard()
        {
            if (dataSo == null)
            {
                Debug.LogError("No data referenced!", gameObject);
                return;
            }

            stringBuilder.Clear();
            
            icon.sprite = dataSo.Icon;

            int level = dataSo.UpgradeIndex;

            UpdateLevelText((level + 1).ToString("F0"));

            nameText.text = dataSo.Title;

            UpdateStatsText(level);
            
            if (dataSo.IsOnMaxLevel)
            {
                upgradeButtonText.text = "MAX";
                upgradeButton.ChangeActiveState(false);
                return;
            }

            stringBuilder.Clear();
            
            var coinAssetPath = ManagersAccess.CoinManager.CoinAssetPath;
            var upgradeArrowAssetPath = settingsIconHolder.UpgradeArrow;
            stringBuilder.Append(upgradeArrowAssetPath);
            stringBuilder.Append(" ");
            stringBuilder.Append(dataSo.GetCost(level).ToString("F0"));
            stringBuilder.Append(coinAssetPath);
            upgradeButtonText.text = stringBuilder.ToString();

            float upgradeCost = dataSo.GetCost(level);
            bool canPurchaseUpgrade = ManagersAccess.CoinManager.HasEnoughtCoin(upgradeCost);

            upgradeButton.Button.interactable = canPurchaseUpgrade;
        }

        protected virtual void UpdateStatsText(int level)
        {

        }

        protected virtual void UpdateLevelText(string levelString)
        {
            levelText.text = levelString;
        }
    }
}