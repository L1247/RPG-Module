#region

using rStar.Modules.Stat.Core.UseCase.Controller;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase;
using rStar.Modules.Stat.UseCase.Repository;
using Zenject;

#endregion

namespace rStar.Modules.Stat.Installer
{
    public class StatInstaller : Installer<StatInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<CreateStatUseCase>().AsSingle();
            Container.Bind<ModifyAmountUseCase>().AsSingle();
            Container.Bind<AddAmountUseCase>().AsSingle();
            Container.Bind<AddModifiersUseCase>().AsSingle();
            Container.Bind<RemoveModifiersUseCase>().AsSingle();
            Container.Bind<DeleteStatUseCase>().AsSingle();

            Container.Bind<StatEventHandler>().AsSingle().NonLazy();
            Container.Bind<IStatController>().To<StatController>().AsSingle();
            Container.Bind<IStatRepository>().To<StatRepository>().AsSingle();
        }

    #endregion
    }
}