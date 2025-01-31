using System.IO;
using UnityEngine;
using Script.Battle;
using Script.Battle.Equipment;
using Script.Inventory.SOInventory;
using TMPro;


namespace Script.Inventory.SOInventory
{
   [System.Serializable]
    public class PlayerData
    {
        public int playerHP;
        public int enemyHP;
        public EquipmentItemSO headItem;
        public EquipmentItemSO torsoItem;
    }

    public class SaveSystem : MonoBehaviour
    {
        private const string SAVE_FILE_NAME = "game_save.json";

        public static bool IsSaveData()
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            return File.Exists(path);
        }
        public static void SaveGame(HealthController healthController, EquipmentController equipmentController)
        {
            PlayerData data = new PlayerData
            {
                playerHP = healthController.PlayerHP,
                enemyHP = healthController.EnemyHP,
                headItem = equipmentController.HeadItem,
                torsoItem = equipmentController.TorsoItem
            };

            string json = JsonUtility.ToJson(data);
            string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            File.WriteAllText(path, json);

            Debug.Log("Game saved to: " + path);
        }

       
        public static void LoadGame(HealthController healthController, EquipmentController equipmentController)
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);

               
                healthController.SetPlayerHealth(data.playerHP);
                healthController.SetEnemyHealth(data.enemyHP);
                healthController.UpdateHealthBars();
                
                if (data.headItem != null)
                {
                    equipmentController.Equip(data.headItem);
                }

                if (data.torsoItem != null)
                {
                    equipmentController.Equip(data.torsoItem);
                }

                Debug.Log("Game loaded from: " + path);
            }
            else
            {
                Debug.LogWarning("No save file found.");
            }
        }

       
        public static void DeleteSaveFile()
        {
            string path = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Save file deleted.");
            }
            else
            {
                Debug.LogWarning("No save file to delete.");
            }
        }
    }
}