using Script.Battle;
using Script.Battle.Equipment;
using UnityEngine;
using Zenject;

namespace Script.Zenject.Installers
{
    public class BattleSceneInstaller : MonoInstaller
    {
        [SerializeField] private EquipmentController _equipmentController;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private RandomLoot _randomLoot;
        public override void InstallBindings()
        {
            Container.Bind<EquipmentController>().FromInstance(_equipmentController);
            Container.Bind<HealthController>().FromInstance(_healthController);
            Container.Bind<RandomLoot>().FromInstance(_randomLoot);
        }
    }
}