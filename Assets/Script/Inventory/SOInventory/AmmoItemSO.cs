using UnityEngine;

namespace Script.Inventory.SOInventory
{
    [CreateAssetMenu(fileName = "NewAmmo", menuName = "Inventory/Ammo")]
    public class AmmoItemSO : ItemSO
    {
        [field: SerializeField]
        public AmmoType AmmoType { get; set; }
    }
}