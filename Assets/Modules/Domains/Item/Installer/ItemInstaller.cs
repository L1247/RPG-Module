#region

using rStar.RPGModules.Item.Infrastructure;
using rStar.RPGModules.Item.Infrastructure.Adapter;
using rStar.RPGModules.Item.UseCase;
using Zenject;

#endregion

namespace rStar.RPGModules.Item.Installer
{
    public class ItemInstaller : Installer<ItemInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<CreateItemUseCase>().AsSingle();
            Container.Bind<ChangeOwnerUseCase>().AsSingle();

            Container.Bind<IItemController>().To<ItemController>().AsSingle();
            Container.Bind<IItemRepository>().To<ItemRepository>().AsSingle();
            Container.Bind<ItemProvider>().AsSingle();
            Container.Bind<ItemService>().AsSingle();
        }

    #endregion
    }
}