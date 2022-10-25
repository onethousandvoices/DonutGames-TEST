using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DonutLabTest
{
    public class Tab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private string _tabId;
        [SerializeField]
        private string _tabName;
        [SerializeField]
        private Tab _brother;
        [SerializeField]
        private GameObject _group;
        private Image _image;
        private Color _baseColor;
        private bool _isSelected;
        public event Action<Tab> PlayerClickEvent;

        public string GroupName => _tabName;
        public string Id => _tabId;

        [ContextMenu("Generate ID")]
        private void GenerateId() => _tabId = Guid.NewGuid().ToString();

        private void Start()
        {
            _image = GetComponent<Image>();
            _baseColor = _image.color;
        }

        public void ShowGroup()
        {
            _group.SetActive(true);
            _brother._group.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData) => Colorize(true);

        public void Colorize(bool isColorize) => _image.color = isColorize ? Color.green : _baseColor;

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_isSelected) return;
            Colorize(false);
            SetSelectionState(false);
        }

        public void SetSelectionState(bool state) => _isSelected = state;

        public void OnPointerClick(PointerEventData eventData)
        {
            SetSelectionState(true);
            PlayerClickEvent?.Invoke(this);
            if (!_brother._isSelected) return;
            _brother.Colorize(false);
            _brother.SetSelectionState(false);
        }
    }
}