using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct TimeReward
{
    public string _id;
    public float _time;
    public int _diamond;
}
public class RewardRuntime : MonoBehaviour
{
    public static RewardRuntime Instance { get; private set; }

    public TimeReward[] rewardSteps;
    private double todaySeconds = 0;
    private DateTime lastUpdateTime;
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadDailyData();
        lastUpdateTime = DateTime.Now;
    }
    void Update()
    {
        // tính số giây player đang chơi trong khung hình
        TimeSpan delta = DateTime.Now - lastUpdateTime;
        todaySeconds += delta.TotalSeconds;

        lastUpdateTime = DateTime.Now;

        // lưu định kỳ mỗi 5 giây (tối ưu)
        if (Time.frameCount % 300 == 0)
            SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
    void LoadDailyData()
    {
        string savedDate = PlayerPrefs.GetString("LastPlayDate", "");

        // nếu ngày mới → reset
        if (savedDate != DateTime.Now.ToShortDateString())
        {
            PlayerPrefs.SetString("LastPlayDate", DateTime.Now.ToShortDateString());
            PlayerPrefs.SetFloat("TodayPlayTime", 0);
        }

        todaySeconds = PlayerPrefs.GetFloat("TodayPlayTime", 0);
    }

    void SaveData()
    {
        PlayerPrefs.SetFloat("TodayPlayTime", (float)todaySeconds);
    }

    public double GetTodayPlayTime()
    {
        return todaySeconds;
    }
    public bool CheckRewards()
    {
        double played = GetTodayPlayTime();

        foreach (var reward in rewardSteps)
        {
            // Nếu đã nhận rồi → bỏ qua
            if (PlayerPrefs.GetInt(reward._id, 0) == 1)
                continue;
            // Đủ thời gian → nhận thưởng
            if (played >= reward._time)
            {
                GiveReward(reward);
                PlayerPrefs.SetInt(reward._id, 1);
                return true;
            }
        }
        return false;
    }

    private void GiveReward(TimeReward reward)
    {
        Debug.Log($"Nhận mốc: {reward._time} giây → +{reward._diamond} kc");
        CurrencyManager.Instance.ChangeCurrentDiamond(reward._diamond);
        RewardRuntimeUI.Instance.ShowReward($"{reward._diamond}", 2f);
    }
    public void ResetRewardsToday()
    {
        foreach (var reward in rewardSteps)
            PlayerPrefs.DeleteKey(reward._id);
    }
    public TimeReward? GetCurrentReward()
    {
        foreach (var reward in rewardSteps)
        {
            if (PlayerPrefs.GetInt(reward._id, 0) == 1)
                continue;
            return reward;
        }
        return null;
    }
}
