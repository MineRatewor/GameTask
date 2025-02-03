using System;
using Script.Battle.Equipment;
using Script.Inventory;
using Script.Inventory.SOInventory;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Script.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private GameController.GameController _gameController;
         private EquipmentController _equipmentController;
         private InventoryController _inventoryController;
        private HealthController _healthController;

        private WeaponSO _currentWeapon;
        private int _damageEnemy;
        private bool _attackHead = true;

        public int DamageEnemy = 15;
        public WeaponSO InitWeapon;

        [Inject]
        private void Construct(EquipmentController equipmentController, InventoryController inventoryController,
            HealthController healthController)
        {
            _equipmentController = equipmentController;
            _inventoryController = inventoryController;
            _healthController = healthController;
        }
        private void Start()
        {
            _currentWeapon = InitWeapon;
            _damageEnemy = DamageEnemy;
        }

        public void SwitchWeapon(WeaponSO weapon)
        {
            _currentWeapon = weapon;
        }

        public void Attack()
        {
            if (_inventoryController.HasAmountAmmo(_currentWeapon.AmmoType, _currentWeapon.AmmoPerShot))
            {
                _inventoryController.UseAmountAmmo(_currentWeapon.AmmoType, _currentWeapon.AmmoPerShot);
                _healthController.TakeDamage(_currentWeapon.Damage); 

                if (_healthController.IsEnemyDead())
                {
                    _healthController.ResetEnemy();
                    _inventoryController.TakeRandomItem();
                }
                else
                {
                    EnemyCounterAttack();
                }
            }
            else
            {
                Debug.Log("Недостаточно  патронов!");
            }
        }

        private void EnemyCounterAttack()
        {
            if (_attackHead)
            {
                _healthController.TakeDamage( _damageEnemy,_equipmentController.GetDefenseHead, true); 
            }
            else
            {
                _healthController.TakeDamage(_damageEnemy, _equipmentController.GetDefenseTorso, true);
            }

            _attackHead = !_attackHead;
            if (_healthController.IsPlayerDead())
            {
                _gameController.ShowGameOver();
            }
        }
        
    }
}