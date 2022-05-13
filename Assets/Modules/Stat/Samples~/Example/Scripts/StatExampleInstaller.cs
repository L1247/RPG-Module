#region

using RPGCore.Stat.Installer;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace RPGCore.Stat.Example.Scripts
{
    public class StatExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            var hasBindingDomainEventBus = Container.HasBinding<IDomainEventBus>();
            if (hasBindingDomainEventBus == false) DDDInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<StatSampleFlow>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatExamplePresenter>().AsSingle();
            StatInstaller.Install(Container);
            Container.Rebind<StatEventHandler>().To<StatEventHandlerExample>().AsSingle().NonLazy();
        }

    #endregion
    }
}