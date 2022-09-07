#region

using System.Collections.Generic;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure
{
    public interface ISkillRepository : IRepository<ISkillReadModel>
    {
    #region Public Methods

        ISkillReadModel              FindSkillByOwner(string  ownerId);
        IEnumerable<ISkillReadModel> FindSkillsByOwner(string ownerId);

    #endregion
    }
}