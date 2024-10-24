using UnityEngine.UI;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Sprite _standartBackGround;
    [SerializeField] private Sprite _highligghrBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _LockImage;

    [SerializeField] private Image _selectionText;

    [SerializeField] private IntValueView _priceView;

    private Image _backGroundImage;

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
}
