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
        private SkillRepository skillRepository;

    #endregion

    #region Public Methods

        public void ExecuteSkill(string id)
        {
            GetSkill(id).Execute();
        }

        public void TickSkill(string id , float time)
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

        private ISkill GetSkill(string id)
        {
            var skillReadModel = skillRepository.FindById(id);
            var skill          = skillReadModel.TransformToDomain();
            return skill;
        }

    #endregion
    }
}