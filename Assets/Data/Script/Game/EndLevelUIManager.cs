using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelUIManager : MonoBehaviour
{
    [SerializeField] private Image _imageEndLevel;
    [SerializeField] private TMP_Text _headerText;
    [SerializeField] private TMP_Text _diamondText;
    [SerializeField] private Image _listHeroReward;
    [SerializeField] private GameObject _prefabImageHero;
    [SerializeField] private Button _buttonConfirm;
    private bool isComplete = false;
    //
    private void Awake()
    {
        _imageEndLevel.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        LevelController.OnFailLevel += IsFailed;
        EnemyManager.OnEnemyClear += IsComplete;
    }
    private void OnDisable()
    {
        LevelController.OnFailLevel -= IsFailed;
        EnemyManager.OnEnemyClear -= IsComplete;
    }
    private void IsFailed()
    {
        isComplete = false;
        ShowNotifyEnd();
    }
    private void IsComplete()
    {
        isComplete = true;
        ShowNotifyEnd();
    }
    private void ShowNotifyEnd()
    {
        Time.timeScale = 0f;
        _imageEndLevel.gameObject.SetActive(true);
        if (isComplete)
        {
            _headerText.text = "Complete";
            _diamondText.text = $"Diamond-{LevelController.Instance._totalDiamondReward}";
            foreach (var hero in LevelController.Instance._heroRewards)
            {
                //add to inventory
                HeroInventory.Instance.AddHero(hero);
                //display on Notify
                var obj = Instantiate(_prefabImageHero, _listHeroReward.gameObject.transform);
                var img = obj.GetComponent<Image>();
                img.sprite = hero._previewImage;
            }
            MapSelectionManager.Instance.UnlockNewMap();
        }
        else
        {
            _headerText.text = "Failed";
            _diamondText.text = $"Diamond-{LevelController.Instance._totalDiamondReward / 3}";
        }

        _buttonConfirm.onClick.AddListener(() => BackToMain());
    }
    private void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene");
    }
}
