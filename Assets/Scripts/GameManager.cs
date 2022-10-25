using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DonutLabTest
{
    public class GameManager : MonoBehaviour, IData
    {
        [SerializeField]
        private Image _mainKnife;
        private Item[] _items;
        private Tab[] _tabs;
        private ItemHolder _holder;
        private TextMeshProUGUI _groupName;
        private GameData _data;

        private void Start()
        {
            _items = GetComponentsInChildren<Item>();
            _tabs = FindObjectsOfType<Tab>();
            _holder = FindObjectOfType<ItemHolder>();
            _groupName = GetComponentInChildren<TextMeshProUGUI>();

            foreach (Item item in _items)
            {
                if (item.Id == _data.SelectedFruitId || item.Id == _data.SelectedKnifeId)
                {
                    item.ItemSelected(true);
                    if (item.IsKnife)
                        _mainKnife.color = item.CurrentImage.color;
                }
                item.PlayerClickEvent += OnPlayerClickEvent;
            }

            foreach (Tab tab in _tabs)
            {
                if (tab.Id == _data.SelectedTabId)
                {
                    tab.Colorize(true);
                    tab.SetSelectionState(true);
                    tab.ShowGroup();
                    _groupName.text = tab.GroupName;
                }
                tab.PlayerClickEvent += OnTabClick;
            }

            _holder.ItemSelectEvent += OnItemSelect;
        }

        private void OnTabClick(Tab tab)
        {
            _data.SelectedTabId = tab.Id;
            _groupName.text = tab.GroupName;
            tab.ShowGroup();
        }

        private void OnItemSelect(Item selectedItem)
        {
            foreach (Item item in _items)
            {
                if (item == selectedItem)
                {
                    if (item.IsKnife)
                        _mainKnife.color = item.CurrentImage.color;
                    continue;
                }
                item.ItemSelected(false);
            }
        }

        private void OnPlayerClickEvent(Item item)
        {
            if (item.IsKnife)
                _data.SelectedKnifeId = item.Id;
            else
                _data.SelectedFruitId = item.Id;

            _holder.ShowItem(item);
        }

        public void LoadData(GameData data) => _data = data;

        public void SaveData(ref GameData data)
        {
            data.SelectedFruitId = _data.SelectedFruitId;
            data.SelectedKnifeId = _data.SelectedKnifeId;
            data.SelectedTabId = _data.SelectedTabId;
        }

        private void OnDestroy()
        {
            foreach (Item item in _items)
                item.PlayerClickEvent -= OnPlayerClickEvent;

            foreach (Tab tab in _tabs)
                tab.PlayerClickEvent -= OnTabClick;
        }
    }
}