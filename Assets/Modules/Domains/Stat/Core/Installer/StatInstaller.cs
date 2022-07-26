#region

using rStar.RPGModules.Stat.Core.UseCase.Controller;
using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase;
using rStar.RPGModules.Stat.UseCase.Repository;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Installer
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

            Container.Bind<IStatController>().To<StatController>().AsSingle();
            Container.Bind<IStatRepository>().To<StatRepository>().AsSingle();
        }

    #endregion
    }
}