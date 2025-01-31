using Script.Inventory.SOInventory;
using UnityEngine;

namespace Script.Battle
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon")]
    public class WeaponSO : ScriptableObject
    {
        [field: SerializeField]
        public int Damage { get; set; }
        [field: SerializeField]
        public AmmoType AmmoType { get; set; }
        [field: SerializeField]
        public int AmmoPerShot { get; set; }
}
}