using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardButton : MonoBehaviour
{
    private Button _button;
    private TMP_Text _time;
    private Animator _anim;
    private double _timeToday;
    private TimeReward? _timeReward;
    private void Awake()
    {
        _button = GetComponent<Button>();
        _time = GetComponentInChildren<TMP_Text>();
        _anim = GetComponent<Animator>();
    }
    private void Start()
    {
        // RewardRuntime.Instance.ResetRewardsToday();
        _button.onClick.AddListener(() => CheckReward());
        _timeReward = RewardRuntime.Instance.GetCurrentReward();

    }
    private void CheckReward()
    {
        if (RewardRuntime.Instance.CheckRewards())
        {
            _anim.Play("reward");
        }
    }
    void Update()
    {
        if (_timeReward != null)
        {
            TimeReward _reward = (TimeReward)_timeReward;
            int remaining = (int)Mathf.Max(0, _reward._time - (int)RewardRuntime.Instance.GetTodayPlayTime());
            _time.text = FormatTime(remaining);
            if (remaining == 0) _timeReward = RewardRuntime.Instance.GetCurrentReward();
        }
    }
    private string FormatTime(int seconds)
    {
        int min = seconds / 60;
        int sec = seconds % 60;
        return $"{min:00}:{sec:00}";
    }

}
