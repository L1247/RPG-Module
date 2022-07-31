#region

using System.Linq;
using rStar.RPGModules.Skill.Infrastructure;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public class SkillRepository : GenericRepository<ISkillReadModel> , ISkillRepository
    {
    #region Public Methods

        public ISkillReadModel FindSkillByOwner(string ownerId)
        {
            return GetAll().FirstOrDefault(skill => skill.OwnerId.Equals(ownerId));
        }

    #endregion
    }
}