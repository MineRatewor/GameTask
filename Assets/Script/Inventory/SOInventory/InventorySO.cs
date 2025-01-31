using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Script.Inventory.SOInventory
{
    [CreateAssetMenu]
    public class InventorySO : ScriptableObject
    {
        public event Action OnInventoryUpdated; 
        [SerializeField] private List<InventoryItem> _inventoryItems;
        
        [field: SerializeField] 
        public int Size { get; private set; } = 10;

        public void Init()
        {
            _inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public void BuyQuantityItem(int index)
        {
            if (index >= 0 && index < _inventoryItems.Count())
            {
                if(_inventoryItems[index].IsEmpty)
                    return;

                _inventoryItems[index] =
                    _inventoryItems[index].ChangeQuantity(_inventoryItems[index].Item.MaxStackSize);
                InformAboutChange();
            }
        }
        public void RemoveItem(int index, int quantity = 1)
        {
            
            if (index >= 0 && index < _inventoryItems.Count())
            {
                if (_inventoryItems[index].IsEmpty)
                    return;
                
                int newQuantity = _inventoryItems[index].Quantity - 1;
                if (newQuantity > 0)
                {
                    _inventoryItems[index] = _inventoryItems[index].ChangeQuantity(newQuantity);
                }
                else
                {
                    _inventoryItems[index] = InventoryItem.GetEmptyItem();
                }
                
                InformAboutChange();
            }
        }
        
        //int
        public void AddItem(ItemSO item, int quantity = 1)
        {
            if (!item.IsStackable)
            {
                for (int i = 0; i < _inventoryItems.Count; i++)
                {
                    while (quantity > 0 && !IsInventoryFull())
                    {
                        quantity -= AddItemToFirstFreeSlot(item);
                    }
                    InformAboutChange();
                    //return quantity;
                }
            }

            quantity = AddStackableItem(item, quantity);
            InformAboutChange();
            //return quantity;
        }

        private int AddItemToFirstFreeSlot(ItemSO item, int quantity = 1)
        {
            
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    InventoryItem newItem = new InventoryItem
                    {
                        Item = item,
                        Quantity = quantity
                    };
                    
                    _inventoryItems[i] = newItem;
                    return quantity;
                }
            }

            return 0;
        }

        private bool IsInventoryFull()
            => !_inventoryItems.Where(item => item.IsEmpty).Any();

        private int AddStackableItem(ItemSO item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;
                if (_inventoryItems[i].Item.ID == item.ID)
                {
                    int amountPossibleToTake =
                        _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].Quantity;

                    if (quantity > amountPossibleToTake)
                    {
                        _inventoryItems[i] = _inventoryItems[i].
                            ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                        quantity -= amountPossibleToTake;
                    }
                    else
                    {
                        _inventoryItems[i] = _inventoryItems[i].
                            ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                        InformAboutChange();
                        return 0;
                    }
                }
            }
            while (quantity > 0 && !IsInventoryFull())
            {
                int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
                quantity -= newQuantity;
                AddItemToFirstFreeSlot(item, newQuantity);
            }

            return quantity;
        }

        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();

            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if(_inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = _inventoryItems[i];
            }

            return returnValue;
        }

        public int GetAmmoCount(AmmoType ammoType)
        {
            int totalAmmo = 0;
            for (int i = 0; i < _inventoryItems.Count(); i++)
            {
                if (!_inventoryItems[i].IsEmpty && _inventoryItems[i].Item is AmmoItemSO ammoItem &&
                    ammoItem.AmmoType == ammoType )
                {
                    totalAmmo += _inventoryItems[i].Quantity;
                }
            }

            return totalAmmo;
        }

        public void RemoveAmmo(AmmoType ammoType, int quantity)
        {
            int ammoQuantity = quantity;
            
            for (int i = 0; i < _inventoryItems.Count(); i++)
            {
                if (!_inventoryItems[i].IsEmpty && _inventoryItems[i].Item is AmmoItemSO ammoItem &&
                    ammoItem.AmmoType == ammoType )
                {
                    if (_inventoryItems[i].Quantity > ammoQuantity)
                    {
                        _inventoryItems[i] =
                            _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Quantity - ammoQuantity);
                        
                        if (_inventoryItems[i].Quantity == 0 || _inventoryItems[i].Quantity < 0)
                            _inventoryItems[i] = InventoryItem.GetEmptyItem();
                        InformAboutChange();
                        return;
                    }
                    
                    
                    ammoQuantity -= _inventoryItems[i].Quantity;
                    _inventoryItems[i] = InventoryItem.GetEmptyItem();
                    InformAboutChange();
                }
            }
        }
        public InventoryItem GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Quantity);
        }
        
        public void SwapItems(int itemIndex1, int itemIndex2)
        {
            if (itemIndex1 >= 0 && itemIndex2 >= 0 && itemIndex1 < _inventoryItems.Count() &&
                itemIndex2 < _inventoryItems.Count())
            {
                InventoryItem item1 = _inventoryItems[itemIndex1];
                _inventoryItems[itemIndex1] = _inventoryItems[itemIndex2];
                _inventoryItems[itemIndex2] = item1;
                InformAboutChange();
            }
            
        }

        private void InformAboutChange()
        {
            OnInventoryUpdated?.Invoke();
        }
        
    }

    [Serializable]
    public struct InventoryItem
    {
        public int Quantity;
        public ItemSO Item;

        public bool IsEmpty => Item == null;

        public InventoryItem ChangeQuantity(int newQuantity)
            => new InventoryItem{
            Item = Item,
            Quantity = newQuantity,
        };
        

        public static InventoryItem GetEmptyItem() 
            => new InventoryItem
        {
            Item = null,
            Quantity = 0,
        };
    }
}