#region

using rStar.RPGModules.Skill.Core;
using rStar.RPGModules.Skill.Infrastructure;
using rStarUtility.Generic.Installer;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Installer
{
    public class SkillInstaller : Installer<SkillInstaller>
    {
    #region Private Variables

        private readonly bool useTicker;

    #endregion

    #region Constructor

        public SkillInstaller(bool useTicker = true)
        {
            this.useTicker = useTicker;
        }

    #endregion

    #region Public Methods

        public override void InstallBindings()
        {
            GenericInstaller.Install(Container);
            Container.Bind<ISkillRepository>().To<SkillRepository>().AsSingle();
            Container.Bind<ISkillController>().To<SkillController>().AsSingle();
            if (useTicker) Container.BindInterfacesAndSelfTo<SkillTicker>().AsSingle();
            Container.BindMemoryPool<Core.Skill , Core.Skill.Pool>().AsSingle();
        }

    #endregion
    }
}