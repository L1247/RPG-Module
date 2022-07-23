#region

using Modules.Skill.Infrastructure;
using rStarUtility.Util;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillController : ISkillController
    {
    #region Private Variables

        [Inject]
        private ISkillRepository skillRepository;

        [Inject]
        private Skill.Factory factory;

    #endregion

    #region Public Methods

        public void CreateSkill(string ownerId , string dataId , float cast , float cd)
        {
            Contract.RequireString(ownerId , $"ownerId:{ownerId}");
            Contract.RequireString(dataId ,  $"dataId:{dataId}");
            Contract.Require(cast >= 0 , "cast need greater than or equal zero");
            Contract.Require(cd >= 0 ,   "cast need greater than or equal zero");
            var skill = factory.Create();
            skill.Init(ownerId , dataId , cast , cd);
        }

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