#region

using Stat.Core.UseCase.Controller;
using Stat.Infrastructure;
using Stat.UseCase;
using Stat.UseCase.Repository;
using Zenject;

#endregion

namespace Stat.Installer
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