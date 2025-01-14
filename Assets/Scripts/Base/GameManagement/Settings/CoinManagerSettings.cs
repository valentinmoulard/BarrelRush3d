using System;
using UnityEngine;

namespace Base.GameManagement.Settings
{
    [Serializable]
    public class CoinManagerSettings
    {
        [field: SerializeField]
        public float MoveToRandomUnitCircleDuration { get; private set; } = 0.2f;

        [field: SerializeField]
        public float MoveToRandomUnitCircleDistance { get; private set; } = 250;

        [field: SerializeField]
        public float MoveToTargetDuration { get; private set; } = 0.3f;

        [field: SerializeField]
        public float JumpPower { get; private set; } = 20;

        [field: SerializeField]
        public float LastScaleAfterMovingToTarget { get; private set; } = 0.5f;

        [field: SerializeField]
        public int DefaultCoinCount { get; private set; } = 0;
    }
}