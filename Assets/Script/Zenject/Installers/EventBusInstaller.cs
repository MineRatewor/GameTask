using Script.CustomEventBus;
using Zenject;

namespace Script.Zenject.Installers
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle().NonLazy();
        }
    }
}