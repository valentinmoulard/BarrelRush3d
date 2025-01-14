using System;

namespace Base.Events
{
    public static class EventsAds
    {
        public static Action<Action,Action> ShowRewardedAd;
        public static Action ShowInterstitialAd;
    }
}