using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    // Start is called before the first frame update
    private HeroData _heroData;
    private Image _imageHero;
    //Drag
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _imageHero = GetComponent<Image>();
        SetHero(null);
    }
    public void SetHero(HeroData _data)
    {
        if (_imageHero == null) return;
        _heroData = _data;
        if (_data != null)
        {
            _imageHero.sprite = _heroData._previewImage;
            _imageHero.color = Color.white;
        }
        else
        {
            _imageHero.sprite = null;
            //trong suốt
            _imageHero.color = new Color(1f, 1f, 1f, 0f);
        }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        // if (_heroData == null) return;
        _originalParent = transform.parent;
        //tách khỏi parent cũ
        transform.SetParent(transform.root);
        //cho phép bỏ qua raycastblock
        _canvasGroup.blocksRaycasts = false;


    }

    public void OnDrag(PointerEventData eventData)
    {
        // if (_heroData == null) return;
        //di chuyển vị trí theo chuột
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        HeroData oldHero  = _heroData;
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<UpgradeSlotUI>() == null)
        {
            if (_heroData != null)
            {
                HeroUpgrade.Instance.RemoveHeroFromUpgrade(oldHero);
                HeroInventory.Instance.AddHero(oldHero);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        HeroSlotUI draggedSlot = eventData.pointerDrag?.GetComponent<HeroSlotUI>();
        if (draggedSlot == null) return;
        HeroData heroData = draggedSlot.GetHeroData();
        if (HeroUpgrade.Instance.AddHeroInUpgrade(heroData)) HeroInventory.Instance.RemoveHero(heroData);

        // SetHero(heroData);
    }
}
