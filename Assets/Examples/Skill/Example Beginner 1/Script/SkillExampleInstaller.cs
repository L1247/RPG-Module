#region

using rStar.RPGModules.Skill.Installer;
using rStarUtility.Generic.Implement.Core;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Example.Beginner1
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            Container.BindInstance(false).WhenInjectedInto<SkillInstaller>();
            SkillInstaller.Install(Container);
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesTo<SkillExamplePresenter>().AsSingle();
        }

    #endregion
    }
}