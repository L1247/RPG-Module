#region

using Modules.Domains.Skill.Core.Infrastructure;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillController : ISkillController
    {
    #region Private Variables

        [Inject]
        private SkillManager skillManager;

    #endregion

    #region Public Methods

        public void ExecuteSkill(string id)
        {
            GetSkill(id).Execute();
        }

        public void TickSkill(string id , int time)
        {
            GetSkill(id).Tick(time);
        }

        public void UseSkill(string id)
        {
            var skill1 = GetSkill(id);
            skill1.UseSkill();
        }

    #endregion

    #region Private Methods

        private Skill GetSkill(string id)
        {
            var skill  = skillManager.FindById(id);
            var skill1 = (Skill)skill;
            return skill1;
        }

    #endregion
    }
}