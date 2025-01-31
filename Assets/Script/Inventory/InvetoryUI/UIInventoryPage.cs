using System;
using System.Collections.Generic;
using Script.Inventory.InvetoryUI.Dragging;
using Script.Inventory.SOInventory;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Inventory.InvetoryUI
{
    public class UIInventoryPage : MonoBehaviour
    {
        public event Action<int> OnDescriptionRequested,
            OnItemActionRequested,
            OnStartDragging;

        public event Action<int, int> OnSwapItems;
        
        
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private UIInventoryDescription _inventoryDescription;
        [SerializeField] private MouseFollower _mouseFollower;
        
        private List<UIInventoryItem> _listOfUIItems = new List<UIInventoryItem>();
        private int _currentDraggedItemIndex = -1;
        private bool _isDragging = false;
        
        
        private void Awake()
        {
            _inventoryDescription.ResetDescription();
        }
        

        public void InitInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                UIInventoryItem item = 
                    Instantiate(_itemPrefab, Vector3.zero, Quaternion.identity, _contentPanel);
                _listOfUIItems.Add(item);

                item.OnItemClicked += HandleItemSelection;
                item.OnItemBeginDrag += HandleBeginDrag;
                item.OnItemDroppedOn += HandleSwap;
                item.OnItemEndDrag += HandleEndDrag;
                item.OnRightMouseClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemIcon, int itemQuantity)
        {
            if (_listOfUIItems.Count > itemIndex)
            {
                _listOfUIItems[itemIndex].SetData(itemIcon, itemQuantity);
            }
        }
        private void HandleShowItemActions(UIInventoryItem obj)
        {
            
        }

        private void HandleEndDrag(UIInventoryItem obj)
        {
            _isDragging = false;
            ResetDraggtedItem();
        }

        private void HandleSwap(UIInventoryItem obj)
        {
            int index = _listOfUIItems.IndexOf(obj);
            if(index == -1)
            {
                
                return;
            }
            OnSwapItems?.Invoke(_currentDraggedItemIndex, index);
            HandleItemSelection(obj);
        }

        private void ResetDraggtedItem()
        {
            _mouseFollower.Toogle(false);
            _currentDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem obj)
        {
            int index = _listOfUIItems.IndexOf(obj);
            if(index == -1) return;
            _currentDraggedItemIndex = index;
            _isDragging = true;
            //HandleItemSelection(obj);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            _mouseFollower.Toogle(true);
            _mouseFollower.SetData(sprite, quantity);
        }
        private void HandleItemSelection(UIInventoryItem obj)
        {
            if (_isDragging) return;
            
            int index = _listOfUIItems.IndexOf(obj);
            if(index == -1) return;
            
            OnDescriptionRequested?.Invoke(index);
        }

        public void ResetSelection()
        {
            _inventoryDescription.ResetDescription();
            DeselectAllItems();
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in _listOfUIItems)
            {
                item.Deselect();
            }
        }

        public void UpdateDescription(int itemIndex, ItemSO item)
        {
            if (item is IConsumable)
            {
                ConsumableItemSO ConsumeItem = (ConsumableItemSO)item;
                _inventoryDescription.SetDescriptionConsumable(ConsumeItem .ItemIcon, ConsumeItem .Name,
                    ConsumeItem .EffectValue ,ConsumeItem .Weight);
            }else if (item is IEquipment)
            {
                EquipmentItemSO equipmentItem = (EquipmentItemSO)item;
                _inventoryDescription.SetDescriptionEquipment(equipmentItem.ItemIcon,
                    equipmentItem.Name, equipmentItem.Defense, equipmentItem.Weight);
            }else if (item is AmmoItemSO)
            {
                AmmoItemSO ammoItem = (AmmoItemSO)item;
                _inventoryDescription.SetDescriptionAmmo(ammoItem.ItemIcon,ammoItem.Name,
                    ammoItem.Weight);
            }
            DeselectAllItems();
            _listOfUIItems[itemIndex].Select();
        }

        public void ResetAllItems()
        {
            foreach (var item in _listOfUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }
    }
}