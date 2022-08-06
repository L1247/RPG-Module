#region

using rStar.RPGModules.Skill.Infrastructure;
using rStarUtility.Util;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public class SkillController : ISkillController
    {
    #region Private Variables

        [Inject]
        private ISkillRepository repository;

        [Inject]
        private Skill.Pool pool;

    #endregion

    #region Public Methods

        public void CreateSkill(string ownerId , string dataId , float cast , float cd)
        {
            Contract.RequireString(ownerId , $"ownerId:{ownerId}");
            Contract.RequireString(dataId ,  $"dataId:{dataId}");
            Contract.Require(cast >= 0 , "cast need greater than or equal zero");
            Contract.Require(cd >= 0 ,   "cast need greater than or equal zero");
            var skill = pool.Spawn().TransformToDomain();
            skill.Init(ownerId , dataId , cast , cd);
            repository.Save(skill.GetId() , skill);
        }

        public void ExecuteSkill(string id)
        {
            GetSkill(id).Execute();
        }

        public void RemoveSkill(string id)
        {
            Contract.RequireString(id , $"id , {id}");
            var skill = GetSkill(id) as Skill;
            pool.Despawn(skill);
            repository.DeleteById(id);
        }

        public void TickSkill(string id , float time)
        {
            GetSkill(id).Tick(time);
        }

        public void UseSkill(string id)
        {
            var skill = GetSkill(id);
            skill.UseSkill();
        }

    #endregion

    #region Private Methods

        private ISkill GetSkill(string id)
        {
            var skillReadModel = repository.FindById(id);
            var skill          = skillReadModel.TransformToDomain();
            return skill;
        }

    #endregion
    }
}