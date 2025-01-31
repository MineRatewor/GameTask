using UnityEngine;

namespace Script.Inventory.SOInventory
{
    [CreateAssetMenu(fileName = "NewConsumable", menuName = "Inventory/Consumable")]
    public class ConsumableItemSO : ItemSO, IConsumable
    {
        [field: SerializeField]
        public int EffectValue { get; set; }
        
        public void Use()
        {
            
        }
    }
}