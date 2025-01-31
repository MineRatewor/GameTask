using UnityEngine;

namespace Script.Inventory.SOInventory
{
    [CreateAssetMenu(fileName = "NewEquipment", menuName = "Inventory/Equipment")]
    public class EquipmentItemSO : ItemSO, IEquipment
    {
        [field: SerializeField]
        public int Defense { get; set; }
        [field: SerializeField]
        public EquipmentType EquipmentType { get; set; }
    }
}