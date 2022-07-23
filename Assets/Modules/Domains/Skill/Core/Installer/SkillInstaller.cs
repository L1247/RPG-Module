#region

using Modules.Skill.Infrastructure;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillInstaller : Installer<SkillInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<ISkillRepository>().To<SkillRepository>().AsSingle();
            Container.Bind<ISkillController>().To<SkillController>().AsSingle();
            Container.Bind<ISkillTicker>().To<SkillTicker>().AsSingle();

            Container.BindFactory<Skill , Skill.Factory>().FromPoolableMemoryPool();
        }

    #endregion
    }
}