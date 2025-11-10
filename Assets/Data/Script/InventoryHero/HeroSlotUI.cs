using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(Image))]
public class HeroSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private HeroData _heroData;
    private Image _imageHero;
    private Button _buttonInParent;
    //Drag
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    public static event Action<HeroData> ShowInformationHero;
    //image của button
    private Image _imageParent;
    private static HeroSlotUI _selectedHeroSlot;
    //event
    public static event Action<HeroData> SignSellHero;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        if (_canvasGroup == null)
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        _imageHero = GetComponent<Image>();
        _buttonInParent = GetComponentInParent<Button>();
        if (_buttonInParent != null)
        {

            _imageParent = _buttonInParent.gameObject.GetComponent<Image>();
            _buttonInParent.onClick.AddListener(() => ButtonHeroClick());
        }
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
    public HeroData GetHeroData()
    {
        return _heroData;
    }
    private void ButtonHeroClick()
    {
        if (_selectedHeroSlot != null && _selectedHeroSlot != this)
            _selectedHeroSlot.Deselect();
        _selectedHeroSlot = this;
        if (_heroData != null)
            SignSellHero?.Invoke(_heroData);
        Select();
        ShowInformation();
    }

    //Hiển thị thông tin hero được chọn
    private void ShowInformation()
    {
        ShowInformationHero?.Invoke(_heroData);
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
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        //SlotUi dang kéo
        HeroSlotUI draggedSlot = eventData.pointerDrag?.GetComponent<HeroSlotUI>();
        if (draggedSlot == null || draggedSlot == this) return;
        HeroData draggedHero = draggedSlot._heroData;
        HeroData targetHero = _heroData;
        //swap
        draggedSlot.SetHero(targetHero);
        SetHero(draggedHero);
    }
    //Select and Deselect
    private void Select()
    {
        if (_imageParent != null)
        {
            _imageParent.color = new Color(246f / 255f, 210f / 255f, 210f / 255f);
        }
    }

    private void Deselect()
    {
        if (_imageParent != null)
        {
            _imageParent.color = Color.white;
        }
    }
}
