#region

using rStar.Modules.Stat.Installer;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace rStar.Modules.Stat.Example.Scripts
{
    public class StatExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<StatSampleFlow>().AsSingle();
            Container.BindInterfacesAndSelfTo<StatExamplePresenter>().AsSingle();
            StatInstaller.Install(Container);
            Container.Bind<StatEventHandlerExample>().AsSingle().NonLazy();
        }

    #endregion
    }
}