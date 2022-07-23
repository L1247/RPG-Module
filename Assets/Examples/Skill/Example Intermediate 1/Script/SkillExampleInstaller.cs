#region

using Modules.Skill.Core;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace Modules.Skill.Example.Intermediate1
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            SkillInstaller.Install(Container);
            Container.UnbindInterfacesTo<SkillTicker>();
            Container.BindInterfacesAndSelfTo<SkillExamplePresenter>().AsSingle();
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
        }

    #endregion
    }
}