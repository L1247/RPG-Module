#region

using Modules.Skill.Core;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace Modules.Skill.Example2
{
    public class SkillExample2Installer : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            SkillInstaller.Install(Container);
            Container.UnbindInterfacesTo<SkillTicker>();
            Container.BindInterfacesAndSelfTo<SkillExample2Presenter>().AsSingle();
            Container.Bind<SkillEventHandler>().AsSingle().NonLazy();
        }

    #endregion
    }
}