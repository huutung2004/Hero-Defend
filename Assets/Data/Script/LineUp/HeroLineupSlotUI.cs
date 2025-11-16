using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroLineupSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private HeroData _heroData;
    private Transform _originalParent;
    private CanvasGroup _canvasGroup;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Bắt đầu Kéo");
        _originalParent = transform.parent;
        transform.SetParent(transform.root);
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Bắt đầu kéo
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        bool placed = TilemapDropHandler.Instance.TrySpawnHero(eventData.position, _heroData);
        if (placed)
        {
            Debug.Log("Spawned Hero");
        }
        transform.SetParent(_originalParent);
        transform.localPosition = Vector3.zero;
        _canvasGroup.blocksRaycasts = true;
    }
    public void SetHero(HeroData heroData)
    {
        _heroData = heroData;
    }
}
