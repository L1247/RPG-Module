#region

using Modules.Stat.Installer;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace Modules.Stat.Example.Beginner1
{
    public class StatExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<StatSampleMain>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatExamplePresenter>().AsSingle();
            StatInstaller.Install(Container);
            Container.Bind<StatEventHandlerExample>().AsSingle().NonLazy();
        }

    #endregion
    }
}