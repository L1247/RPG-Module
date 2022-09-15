#region

using rStar.RPGModules.Item.Installer;
using rStarUtility.Generic.Installer;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Example.Script
{
    public class ItemExampleInstaller : MonoInstaller<ItemExampleInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            ItemInstaller.Install(Container);

            Container.Bind<ItemEventHandlerExample>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemExamplePresenter>().AsSingle();
        }

    #endregion
    }
}