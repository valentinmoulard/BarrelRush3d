using System;
using Base.Events;
using Base.Helpers;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Color = System.Drawing.Color;

namespace Base.Fader
{
    public class Fader: EventListener
    {
        [field: SerializeField]
        public Image FaderImage { get; private set; }

        [SerializeField]
        private float faderDuration = 0.25f;

        private Tween _fadeTween;
        
        private bool _currentStatus;

        protected override void SubscribeEvents()
        {
            EventsFader.DoFade += DoFade;
            DoFade(true);
        }

        protected override void UnsubscribeEvents()
        {
            EventsFader.DoFade -= DoFade;
        }
        
        /// <summary>
        /// Fade in means alpha goes to 0, fade out means alpha goes to 1
        /// </summary>
        /// <param name="isIn"></param>
        /// <param name="onComplete"></param>
        private void DoFade(bool isIn, Action onComplete = null)
        {
            if (_currentStatus == isIn)
            {
                YufisDebug.Log($"[Fader] Returned", Color.Red);
                return;
            }
            YufisDebug.Log($"[Fader] Fade {isIn}", Color.Aqua);
            _currentStatus = isIn;
            _fadeTween?.Complete();
            _fadeTween = FadeTween(isIn);
            _fadeTween.onComplete += () =>
            {
                onComplete?.Invoke();
            };
        }
        
        private Tween FadeTween(bool isIn)
        {
            var startValue = isIn ? 1f : 0f;
            var endValue = isIn ? 0f : 1f;
            return FaderImage.DOFade(endValue, faderDuration).From(startValue);
        }
    }
}