#region

using rStar.Modules.Skill.Core;
using rStarUtility.DDD.Implement.Core;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example2
{
    public class SkillExample2Installer : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            DDDInstaller.Install(Container);
            SkillInstaller.Install(Container);
        }

    #endregion
    }
}