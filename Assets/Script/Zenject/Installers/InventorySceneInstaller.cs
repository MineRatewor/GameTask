using Script.Inventory;
using UnityEngine;
using Zenject;

namespace Script.Zenject.Installers
{
    public class InventorySceneInstaller : MonoInstaller
    {
        [SerializeField] private InventoryController _inventoryController;
        public override void InstallBindings()
        {
            Container.Bind<InventoryController>().FromInstance(_inventoryController);
        }
    }
}