#region

using rStar.Modules.Skill.Core;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Example
{
    public class SkillExampleInstaller : MonoInstaller
    {
    #region Public Methods

        public override void InstallBindings()
        {
            SkillInstaller.Install(Container);
        }

    #endregion
    }
}