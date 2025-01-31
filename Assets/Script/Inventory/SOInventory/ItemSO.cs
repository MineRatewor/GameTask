using UnityEngine;

namespace Script.Inventory.SOInventory
{
    [CreateAssetMenu]
    public abstract class  ItemSO : ScriptableObject
    {
        [field:SerializeField]
        public bool IsStackable { get; set; }
        
        public int ID => GetInstanceID();

        [field:SerializeField]
        public int MaxStackSize { get; set; } = 1;
        
        [field:SerializeField]
        public string Name;
        
        [field:SerializeField]
        public Sprite ItemIcon { get; set; }

        [field: SerializeField] 
        public float Weight { get; set; }
    }

    public interface IConsumable
    {
        void Use();
    }

    public interface IEquipment
    {
        public int Defense { get; set; }
        public  EquipmentType EquipmentType { get; set; }
    }

    public interface IWeapon
    {
        public int Damage { get; set; }
        public AmmoType AmmoType { get; set; }
        
        public int AmmoPerShot { get; set; }
        public void Fire();
    }

    public enum AmmoType
    {
        Pistol9x18,
        Rifle5x45
    }

    public enum EquipmentType
    {
        Head,
        Torso
    }
}