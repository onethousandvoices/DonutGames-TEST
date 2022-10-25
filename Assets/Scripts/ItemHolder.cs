using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DonutLabTest
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField]
        private Image _targetImage;
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private TextMeshProUGUI _itemNameText;
        private Item _currentItem;
        private static readonly int Jump = Animator.StringToHash("jump");
        public event Action<Item> ItemSelectEvent;
        private RectTransform _rect;

        public void ShowItem(Item item)
        {
            if (item == _currentItem) return;
            _rect ??= _targetImage.GetComponent<RectTransform>();
            _rect.sizeDelta = item.CurrentImage.rectTransform.sizeDelta;
            _itemNameText.text = item.name;
            _currentItem = item;
            _targetImage.sprite = item.CurrentImage.sprite;
            _targetImage.color = item.CurrentImage.color;
            _animator.SetTrigger(Jump);
        }

        public void Select_UnityEvent()
        {
            ItemSelectEvent?.Invoke(_currentItem);
            _currentItem.ItemSelected(true);
        }
    }
}