#region

using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class SkillInstaller : Installer<SkillInstaller>
    {
    #region Public Methods

        public override void InstallBindings()
        {
            Container.Bind<SkillRegistry>().AsSingle();
            Container.Bind<SkillSpawner>().AsSingle();
        }

    #endregion
    }
}