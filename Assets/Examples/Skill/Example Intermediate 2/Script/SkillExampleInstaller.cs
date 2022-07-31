#region

using rStar.RPGModules.Skill.Installer;
using rStarUtility.Generic.Installer;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Example.Intermediate2
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            EventBusInstaller.Install(Container);
            SkillInstaller.Install(Container);
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SkillExamplePresenter>().AsSingle();
        }

    #endregion
    }
}