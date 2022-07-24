#region

using Modules.Stat.Installer;
using rStarUtility.Generic.Implement.Core;
using Zenject;

#endregion

namespace Modules.Stat.Example.Beginner1
{
    public class StatExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<StatSampleMain>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatExamplePresenter>().AsSingle();
            StatInstaller.Install(Container);
            Container.Bind<StatEventHandlerExample>().AsSingle().NonLazy();
        }

    #endregion
    }
}