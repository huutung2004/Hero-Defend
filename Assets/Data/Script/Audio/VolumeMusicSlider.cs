using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMusicSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(ChangeVolume);
    }
    private void Start()
    {
        slider.value = MusicManager.Instance.GetCurrentVolumeMusic();
    }
    // private void OnEnable()
    // {
    //     MusicManager.OnVolumeMusicChanged += ChangeVolume;
    // }
    //  private void OnDisable()
    // {
    //     MusicManager.OnVolumeMusicChanged -= ChangeVolume;
    // }

    private void ChangeVolume(float value)
    {
        MusicManager.Instance.ChangeVolumeMusic(value);
    }
}
