using Script.Inventory;
using Script.Inventory.SOInventory;
using UnityEngine;

namespace Script.Battle.Equipment
{
    public class EquipmentController : MonoBehaviour
    {
        [SerializeField] private EquipmentUI _equipmentUI;
        [SerializeField] private InventoryController _inventoryController;
        private EquipmentItemSO _headItem;
        private EquipmentItemSO _torsoItem;

        public EquipmentItemSO HeadItem => _headItem;
        public EquipmentItemSO TorsoItem => _torsoItem;
        public void Equip(EquipmentItemSO item)
        {
            if (item.EquipmentType == EquipmentType.Head)
            {
                if (_headItem != null)
                    Unequip(_headItem);
                
                    
                
                _headItem = item;
                
                _equipmentUI.SetHeadData(_headItem.ItemIcon, _headItem.Defense);
            }
            else
            {
                if (_torsoItem != null)
                    Unequip(_torsoItem);
                
                
                _torsoItem = item;
                Debug.Log("equip " + item.Defense);
                _equipmentUI.SetTorsoData(_torsoItem.ItemIcon, _torsoItem.Defense);
            }
        }

        public void Unequip(EquipmentItemSO item)
        {
            _inventoryController.AddItem(item);
        }

        public int GetDefenseHead => _headItem != null ? _headItem.Defense : 0;

        public int GetDefenseTorso => _torsoItem != null ? _torsoItem.Defense : 0;

    }
}