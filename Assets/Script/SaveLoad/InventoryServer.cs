using System.IO;
using UnityEngine;

namespace Script.Inventory.SOInventory
{
    public class InventorySaver : MonoBehaviour
    {
        private const string INVENTORY_FILE_NAME = "inventory_data.json";

        public static bool IsInventoryFileExists()
        {
            string path = Path.Combine(Application.persistentDataPath, INVENTORY_FILE_NAME);
            return File.Exists(path);
        }

        public static void SaveInventory(InventorySO inventory)
        {
            string json = JsonUtility.ToJson(inventory, true);
            string path = Path.Combine(Application.persistentDataPath, INVENTORY_FILE_NAME);
            File.WriteAllText(path, json);
            Debug.Log("Inventory saved to: " + path);
        }

        public static void LoadInventory(InventorySO inventory)
        {
            string path = Path.Combine(Application.persistentDataPath, INVENTORY_FILE_NAME);

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                JsonUtility.FromJsonOverwrite(json, inventory);
                Debug.Log("Inventory loaded from: " + path);
            }
            else
            {
                Debug.LogWarning("No inventory file found. Creating new inventory.");
                inventory.Init();  
            }
        }

        public static void DeleteInventoryFile()
        {
            string path = Path.Combine(Application.persistentDataPath, INVENTORY_FILE_NAME);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("Inventory file deleted.");
            }
            else
            {
                Debug.LogWarning("No inventory file to delete.");
            }
        }
    }
}