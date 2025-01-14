using System;
using Base.Events;
using Base.GameManagement;
using Base.GameManagement.Settings;
using Base.Helpers;
using Base.Pool;
using Base.SaveSystem;
using Base.SaveSystem.SaveableScriptableObject;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Base.Managers
{
    public class Manager_Coin : ManagerBase
    {
        [field: SerializeField]
        public string CoinAssetPath { get; private set; }
        
        [SerializeField]
        private CoinAnimator coinAnimator;
        
        private SaveData_General SaveDataGeneral => ManagersAccess.SaveManager.GetSaveData<SaveData_General>(SaveDataType.General);
        public float TotalCoins 
        {
            get => SaveDataGeneral.TotalCoin;
            private set
            {
                SaveDataGeneral.TotalCoin = value;
                EventsCoin.OnCoinChanged?.Invoke();
            }
        }

        private const int CoinAnimationCount = 10;
        private float _profitMultiplier = 1f;

        public override void SetUp()
        {
            base.SetUp();
            coinAnimator.Initialize();
        }

        public bool HasEnoughtCoin(float cost)
        {
            return cost <= TotalCoins;
        }

        public void ReduceCoin(float howMany)
        {
            TotalCoins -= howMany;
            SetCoinText();
        }

        public void AddCoin(float howMany)
        {
            TotalCoins += howMany + _profitMultiplier;
            coinAnimator.CreateCoinAnimation(CoinAnimationCount);
        }
        
        private void SetCoinText(float duration = 0.25f)
        {
            coinAnimator.SetCoinText(duration);
        }
        
        protected override void OnDisable()
        {
            coinAnimator.Disable();
        }

        public void SetProfitMultiplier(float profit)
        {
            _profitMultiplier = profit;
        }
    }

    [Serializable]
    public class CoinAnimator
    {
        [SerializeField]
        private Transform coinTarget;

        [SerializeField]
        private TMP_Text coinText;

        #region CoinSettings

        private CoinManagerSettings _coinManagerSettings;

        private CoinManagerSettings CoinManagerSettings =>
            _coinManagerSettings ??= Settings_General.Instance.CoinManagerSettings;

        #endregion

        private Tween _coinTextTween;
        private int _displayedCoins = 0;
        private Pool_Coin PoolCoin => ManagersAccess.PoolManager.PoolCoin;

        public void Initialize()
        {
            _displayedCoins = (int)ManagersAccess.CoinManager.TotalCoins;
            SetCoinText(duration: 0.1f);
        }

        public void CreateCoinAnimation(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var coin = PoolCoin.GetObject();
                coin.transform.ResetTransformLocals();
                var sequence = DOTween.Sequence();
                sequence.Append(coin.transform.DOMove(Random.insideUnitCircle * CoinManagerSettings.MoveToRandomUnitCircleDistance, CoinManagerSettings.MoveToRandomUnitCircleDuration).SetRelative(true));
                sequence.Append(coin.transform.DOJump(coinTarget.position, 20f, 1, CoinManagerSettings.MoveToTargetDuration));
                sequence.Join(coin.transform.DOScale(Vector3.one * CoinManagerSettings.LastScaleAfterMovingToTarget, CoinManagerSettings.MoveToTargetDuration));
                sequence.AppendCallback(() => { PoolCoin.ReturnObject(coin); });
                if(i == amount - 1)
                {
                    sequence.AppendCallback(() => SetCoinText());
                }
            }
        }

        private Tween CoinTextTween(float duration = 0.25f)
        {
            return DOTween.To(() => _displayedCoins, x =>
            {
                _displayedCoins = x;
                coinText.text = ManagersAccess.CoinManager.CoinAssetPath + CoinAbbreviationUtility.AbbreviateNumber(x);
            }, (int)ManagersAccess.CoinManager.TotalCoins, duration);
        }
        
        public void SetCoinText(float duration = 0.25f)
        {
            if (duration <= 0f)
            {
                coinText.text = CoinAbbreviationUtility.AbbreviateNumber(ManagersAccess.CoinManager.TotalCoins);
                return;
            }
            KillTween();
            _coinTextTween = CoinTextTween(duration);
        }

        private void KillTween()
        {
            _coinTextTween?.Kill(true);
        }

        public void Disable()
        {
            KillTween();
        }
    }
}
