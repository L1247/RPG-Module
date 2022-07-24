#region

#endregion

#region

using rStarUtility.Generic.Usecase;

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