#region

using Modules.Stat.Core.UseCase.Controller;
using Modules.Stat.Infrastructure;
using Modules.Stat.UseCase;
using Modules.Stat.UseCase.Repository;
using Zenject;

#endregion

namespace Modules.Stat.Installer
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