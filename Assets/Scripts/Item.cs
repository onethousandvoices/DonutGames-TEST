using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DonutLabTest
{
    public class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private GameObject _highlighter;
        [SerializeField]
        private GameObject _checkmark;
        [SerializeField]
        private Image _currentImage;
        [SerializeField]
        private bool _isKnife;

        public Image CurrentImage => _currentImage;
        public string Id => _id;
        public bool IsKnife => _isKnife;
        public event Action<Item> PlayerClickEvent;

        [ContextMenu("Generate ID")]
        private void GenerateId() => _id = Guid.NewGuid().ToString();

        public void ItemSelected(bool isSelected) => _checkmark.SetActive(isSelected);

        public void OnPointerEnter(PointerEventData eventData) => _highlighter.SetActive(true);
        public void OnPointerExit(PointerEventData eventData) => _highlighter.SetActive(false);

        public void OnPointerClick(PointerEventData eventData) => PlayerClickEvent?.Invoke(this);
    }
}