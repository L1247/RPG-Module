#region

using rStar.RPGModules.Stat.Installer;
using rStarUtility.Generic.Installer;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Example.Beginner1
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