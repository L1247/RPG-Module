#region

using rStar.RPGModules.Combat.Core;
using rStar.RPGModules.Combat.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Installer
{
    public class CombatInstaller : Installer<CombatInstaller>
    {
    #region Private Variables

        private readonly bool useOwnConfig;

    #endregion

    #region Constructor

        public CombatInstaller(bool useOwnConfig = false)
        {
            this.useOwnConfig = useOwnConfig;
        }

    #endregion

    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<ICombatService>().To<CombatService>().AsSingle();
            if (useOwnConfig == false) Container.Bind<IStatConfig>().To<StatConfig>().AsSingle();
        }

    #endregion
    }
}