#region

using Modules.Domains.Skill.Core.Infrastructure;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillInstaller : Installer<SkillInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<SkillManager>().AsSingle();
            Container.Bind<ISkillController>().To<SkillController>().AsSingle();
            Container.Bind<SkillSpawner>().AsSingle();
            Container.BindInterfacesTo<SkillTicker>().AsSingle();

            Container.BindFactory<Skill , Skill.Factory>().FromPoolableMemoryPool();
        }

    #endregion
    }
}