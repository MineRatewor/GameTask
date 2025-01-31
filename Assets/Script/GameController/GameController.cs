using System;
using Script.Inventory.SOInventory;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.GameController
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOver;
        

        public void ShowGameOver()
        {
            _gameOver.gameObject.SetActive(true);
        }
        
        public void ReloadScene()
        {
            InventorySaver.DeleteInventoryFile();
            SaveSystem.DeleteSaveFile();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}