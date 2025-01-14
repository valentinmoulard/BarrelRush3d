using System.ComponentModel;
using Base.GameManagement;
using Base.GameManagement.Settings;
using UnityEngine;

public partial class SROptions
{
    [Category("GameOptions")]
    public float GameSpeed
    {
        get => Time.timeScale;
        set => Time.timeScale = value;
    }
    
    [Category("CoinOptions")]
    public float Add1000Coin {
        get => ManagersAccess.CoinManager.TotalCoins;
        set => ManagersAccess.CoinManager.AddCoin(1000);
    } 
    
    [Category("MovementOptions")]
    public float ChangeHorizontalSmoothTime {
        get => Settings_General.Instance.GameSettings.SettingsHeroMovement.HorizontalSmoothTime;
        set
        {
            var clampedValue = Mathf.Clamp(value, 0, 1);
            Settings_General.Instance.GameSettings.SettingsHeroMovement.HorizontalSmoothTime = clampedValue;
        }
    }

    [Category("MovementOptions")]
    public float ChangeMaxHorizontalVelocityAmplitude {
        get => Settings_General.Instance.GameSettings.SettingsHeroMovement.MaxHorizontalVelocityAmplitude;
        set
        {
            var clampedValue = Mathf.Clamp(value, 0, 100);
            Settings_General.Instance.GameSettings.SettingsHeroMovement.MaxHorizontalVelocityAmplitude = clampedValue;
        }
    }

    [Category("MovementOptions")]
    public float ChangeTouchSensitivity {
        get => Settings_General.Instance.GameSettings.SettingsHeroMovement.TouchSensitivity;
        set
        {
            var clampedValue = Mathf.Clamp(value, 0, 1);
            Settings_General.Instance.GameSettings.SettingsHeroMovement.TouchSensitivity = clampedValue;
        }
    }
}