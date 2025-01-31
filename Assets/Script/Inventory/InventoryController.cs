using System;
using System.Collections.Generic;
using Script.Battle;
using Script.Battle.Equipment;
using Script.Inventory.InvetoryUI;
using Script.Inventory.SOInventory;
using UnityEngine;

namespace Script.Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private RandomLoot _randomLoot;
        [SerializeField] private EquipmentController _equipmentController;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private InventorySO _inventoryData;
        [SerializeField] private UIInventoryPage _inventoryPage;

        private int _currentActiveIndexItem = -1;
        public List<InventoryItem> InitItems = new List<InventoryItem>();
        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            
            UpdateInventoryPage();
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Init();
            _inventoryData.OnInventoryUpdated += UpdateInventoryPage;
            
            if (InventorySaver.IsInventoryFileExists())
            {
                InventorySaver.LoadInventory(_inventoryData);
                SaveSystem.LoadGame(_healthController, _equipmentController);
                UpdateInventoryPage();
            }
            else
            {
                foreach (InventoryItem item in InitItems)
                {
                    if (item.IsEmpty)
                        continue;
                    _inventoryData.AddItem(item);
                }
            }
        }
        private void PrepareUI()
        {
            _inventoryPage.InitInventoryUI(_inventoryData.Size);
            
            _inventoryPage.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryPage.OnSwapItems += HandleSwapItems;
            _inventoryPage.OnStartDragging += HandleDragging;
            _inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryPage.CreateDraggedItem(inventoryItem.Item.ItemIcon,inventoryItem.Quantity);
        }

        private void HandleSwapItems(int itemIndex1, int itemIndex2)
        {
            _inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetSelection();
                return;
            }

            _currentActiveIndexItem = itemIndex;
            Debug.Log("index current item " + _currentActiveIndexItem);
            
            ItemSO item = inventoryItem.Item;
            _inventoryPage.UpdateDescription(itemIndex, item);
        }

        public void UpdateInventoryPage()
        {
            _inventoryPage.ResetAllItems();
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryPage.UpdateData(item.Key, 
                    item.Value.Item.ItemIcon, 
                     item.Value.Quantity);
            }
        }

        public void RemoveItem()
        {
            _inventoryData.RemoveItem(_currentActiveIndexItem);
            _inventoryPage.ResetSelection();
        }

        public void EquipItem()
        {
            InventoryItem item = _inventoryData.GetItemAt(_currentActiveIndexItem);
            if (item.Item is EquipmentItemSO)
            {
                EquipmentItemSO equipmentItem = (EquipmentItemSO)item.Item;
            
                _equipmentController.Equip(equipmentItem);
                _inventoryData.RemoveItem(_currentActiveIndexItem, item.Quantity);
                _inventoryPage.ResetSelection();
            }
            
        }

        public void ConsumeItem()
        {
            InventoryItem item = _inventoryData.GetItemAt(_currentActiveIndexItem);
            if (item.Item is ConsumableItemSO)
            {
                ConsumableItemSO consumableItem = (ConsumableItemSO)item.Item;
                
                _healthController.RecoverPlayerHP(consumableItem.EffectValue);
                Debug.Log("efefct" + consumableItem.EffectValue);
                _inventoryData.RemoveItem(_currentActiveIndexItem);
                _inventoryPage.ResetSelection();
            }
        }
        public void BuyQuantityItem()
        {
            _inventoryData.BuyQuantityItem(_currentActiveIndexItem);
            _inventoryPage.ResetSelection();
        }

        public bool HasAmountAmmo(AmmoType type , int ammoPerShot)
        {
            int countAmmo = _inventoryData.GetAmmoCount(type);
            return countAmmo >= ammoPerShot;
        }

        public void UseAmountAmmo(AmmoType type, int ammoPerShot)
        {
            _inventoryData.RemoveAmmo(type, ammoPerShot);
        }

        public void AddItem(ItemSO item)
        {
            _inventoryData.AddItem(item);
        }

        public void TakeRandomItem()
        {
            ItemSO item = _randomLoot.GetRandomLot();

            if (item == null)
                return;
            
            _inventoryData.AddItem(item, item.MaxStackSize);
        }

        private void OnApplicationQuit()
        {
            InventorySaver.SaveInventory(_inventoryData);
            SaveSystem.SaveGame(_healthController, _equipmentController);
        }
    }
}