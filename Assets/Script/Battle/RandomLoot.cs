using System.Collections.Generic;
using ModestTree;
using Script.Inventory.SOInventory;
using UnityEngine;

namespace Script.Battle
{
    public class RandomLoot : MonoBehaviour
    {
        [SerializeField] private List<ItemSO> _randomLoot = new List<ItemSO>();

        public ItemSO GetRandomLot()
        {
            if (_randomLoot.IsEmpty())
            {
                return null;
            }
            
            int index = Random.Range(0, _randomLoot.Count);
            return _randomLoot[index];
        }
    }
}