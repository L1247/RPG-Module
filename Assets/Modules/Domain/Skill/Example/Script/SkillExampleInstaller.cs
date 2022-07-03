#region

using rStar.Modules.Skill.Core;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            SkillInstaller.Install(Container);
            Container.BindInterfacesTo<Main>().AsSingle();
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
        }

    #endregion
    }
}