#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure
{
    public interface ISkillRepository : IRepository<ISkillReadModel>
    {
    #region Public Methods

        ISkillReadModel FindSkillByOwner(string ownerId);

    #endregion
    }
}