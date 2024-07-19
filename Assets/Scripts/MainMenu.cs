using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text lastScoreText;
    [SerializeField] TMP_Text energyText;
    [SerializeField] TMP_Text playButtonText;
    [SerializeField] Color deactivatedPlayButtonTextColor;
    [SerializeField] AndroidNotificationHandler androidNotificationHandler;
    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeTimeInSeconds;

    int energy;

    public const string ENERGY_KEY = "Energy";
    public const string DATETORESETENERGY_KEY = "EnergyReady";

    Color currentPlayButtonTextColor;

    private void Awake()
    {
        currentPlayButtonTextColor = playButtonText.color;
        Application.targetFrameRate = 120;
    }

    void Start()
    {
        OnApplicationFocus(true);
    }


    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            return;
        }

        CancelInvoke();

        UpdateLastAndHighScoreTexts();

        HandleEnergyRecharge();

    }


    private void UpdateLastAndHighScoreTexts()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.HIGHSCORE_KEY, 0);
        int lastScore = PlayerPrefs.GetInt(ScoreSystem.LASTSCORE_KEY, 0);

        highScoreText.text = "High Score:\n" + highScore;
        lastScoreText.text = "Last Score:\n" + lastScore;
    }


    void HandleEnergyRecharge()
    {
        energy = PlayerPrefs.GetInt(ENERGY_KEY, maxEnergy);

        if (energy >= maxEnergy)
        {
            energyText.text = "Energy:\n" + energy;
            return;
        }

        string dateToResetEnergy_String = PlayerPrefs.GetString(DATETORESETENERGY_KEY, string.Empty);

        if (dateToResetEnergy_String == string.Empty)
        {
            return;
        }

        DateTime dateToResetEnergy_Datetime = DateTime.Parse(dateToResetEnergy_String);
        double secondsSinceLastEnergyReset = (DateTime.Now - dateToResetEnergy_Datetime).TotalSeconds;

        if (secondsSinceLastEnergyReset >= 0)
        {
            double totalSecondsIncludingRechargeTime = energyRechargeTimeInSeconds + secondsSinceLastEnergyReset;
            double numberOfFullEnergyCharges = totalSecondsIncludingRechargeTime / energyRechargeTimeInSeconds;
            energy += Mathf.FloorToInt((float)numberOfFullEnergyCharges);
            double remainingSecondsAfterFullCharges = totalSecondsIncludingRechargeTime % energyRechargeTimeInSeconds;
            double secondsUntilNextEnergyReset = energyRechargeTimeInSeconds - remainingSecondsAfterFullCharges;

            if (energy >= maxEnergy)
            {
                energy = maxEnergy;
                secondsUntilNextEnergyReset = energyRechargeTimeInSeconds;
            }

            PlayerPrefs.SetInt(ENERGY_KEY, energy);
            DateTime dateToResetEnergy_DateTime = DateTime.Now.AddSeconds(secondsUntilNextEnergyReset);
            PlayerPrefs.SetString(DATETORESETENERGY_KEY, dateToResetEnergy_DateTime.ToString());
            Invoke(nameof(HandleEnergyRecharge), (float)secondsUntilNextEnergyReset);
        }
        else
        {
            Invoke(nameof(HandleEnergyRecharge), (dateToResetEnergy_Datetime - DateTime.Now).Seconds);
        }
        energyText.text = "Energy:\n" + energy;
        HandlePlayButtonColor();
    }
  

    public void Play()
    {
        if (energy < 1)
        {
            return;
        }

        if (energy >= maxEnergy)
        {

            DateTime dateToResetEnergy_DateTime = DateTime.Now.AddSeconds(energyRechargeTimeInSeconds);
            PlayerPrefs.SetString(DATETORESETENERGY_KEY, dateToResetEnergy_DateTime.ToString());
        }

        energy--;

        PlayerPrefs.SetInt(ENERGY_KEY, energy);

        SendNotifications();

        HandlePlayButtonColor();

        SceneManager.LoadScene(1);
    }


    void SendNotifications()
    {
#if UNITY_ANDROID
        if (energy == 0)
        {
            string message = "Your energy has recharged, come back to play again!";
            DateTime dateWhenAllEnergyRecharged = DateTime.Now.AddSeconds(energyRechargeTimeInSeconds * maxEnergy);
            androidNotificationHandler.ScheduleNotification(dateWhenAllEnergyRecharged,message);

            var halfEnergy = Mathf.RoundToInt(maxEnergy / 2);
            string message2 = $"{halfEnergy} available charges, come back to play again!";
            var halfEnergyRechargeTime = energyRechargeTimeInSeconds * halfEnergy;
            DateTime dateWhenHalfEnergyRecharged = DateTime.Now.AddSeconds(halfEnergyRechargeTime);
            androidNotificationHandler.ScheduleNotification(dateWhenHalfEnergyRecharged, message2);
        }
#endif
    }


    void HandlePlayButtonColor()
    {
        if (energy > 0)
        {
            playButtonText.color = currentPlayButtonTextColor;
        }
        else
        {
            playButtonText.color = deactivatedPlayButtonTextColor;
        }
    }

}
