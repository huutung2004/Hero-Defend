using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineupSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
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
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<LineupSlotUI>() == null)
        {
            if (_heroData != null)
            {
                HeroLineup.Instance.RemoveHeroFromLineup(_heroData);
                // SetHero(null);
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        HeroSlotUI draggedSlot = eventData.pointerDrag?.GetComponent<HeroSlotUI>();
        if (draggedSlot == null) return;
        HeroData heroData = draggedSlot.GetHeroData();
        HeroLineup.Instance.AddHeroInLineup(heroData);
        // SetHero(heroData);
    }
}
