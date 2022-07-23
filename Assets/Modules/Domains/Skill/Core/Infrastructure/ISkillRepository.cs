#region

using rStarUtility.DDD.Event.Usecase;

#endregion

namespace Modules.Skill.Infrastructure
{
    public interface ISkillRepository : IRepository<ISkillReadModel>
    {
    #region Public Methods

        ISkillReadModel FindSkillByOwner(string ownerId);

    #endregion
    }
}