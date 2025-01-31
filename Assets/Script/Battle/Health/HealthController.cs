using Script.Inventory.SOInventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Battle
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private Scrollbar _playerHPBar;
        [SerializeField] private Scrollbar _enemyHPBar;
        [SerializeField] private TMP_Text _playerHealth;
        [SerializeField] private TMP_Text _enemyHealth;

        private int _playerHP;
        private int _enemyHP;
        private int _maxHP = 100;

        public int PlayerHP => _playerHP;
        public int EnemyHP => _enemyHP;

        private void Start()
        {
            if (!SaveSystem.IsSaveData())
            {
                _playerHP = _maxHP;
                _enemyHP = _maxHP;
            }
            
            UpdateHealthBars();
        }
        
        public void TakeDamage(int damage,int defense = 0, bool isPlayer = false)
        {
            if (isPlayer)
                _playerHP = Mathf.Max(0, _playerHP - damage + defense);
            else
                _enemyHP = Mathf.Max(0, _enemyHP - damage + defense);

            UpdateHealthBars();
        }

        public bool IsEnemyDead() => _enemyHP <= 0;
        public bool IsPlayerDead() => _playerHP <= 0;

        public void ResetEnemy()
        {
            _enemyHP = _maxHP;
            UpdateHealthBars();
        }

        public void RecoverPlayerHP(int value)
        {
            
            _playerHP = Mathf.Min(_maxHP, _playerHP + value);
            UpdateHealthBars();
        }
        public void UpdateHealthBars()
        {
            _playerHPBar.size = (float)_playerHP / _maxHP;
            _enemyHPBar.size = (float)_enemyHP / _maxHP;

            _playerHealth.text = _playerHP.ToString();
            _enemyHealth.text = _enemyHP.ToString();
        }
        
        public void SetPlayerHealth(int health) => _playerHP = health;
        public void SetEnemyHealth(int health) => _enemyHP = health;
    }
}