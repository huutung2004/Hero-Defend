using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    private TMP_Text _waveText;
    void Awake()
    {
        _waveText = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        LevelController.WavePerTotal += RefreshUI;
    }
    private void OnDisable()
    {
        LevelController.WavePerTotal += RefreshUI;
    }
    private void RefreshUI(int _waveCount, int _waveTotal)
    {
        DialogManager.Instance.ShowDialog($"Wave {_waveCount}",1f);
        _waveText.text = $"Wave: {_waveCount}/{_waveTotal}";
    }
}
