#region

using Modules.Skill.Core;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace Modules.Skill.Example.Beginner1
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            SkillInstaller.Install(Container);
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SkillExamplePresenter>().AsSingle();
            Container.UnbindInterfacesTo<SkillTicker>();
        }

    #endregion
    }
}