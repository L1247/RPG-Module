#region

using System.Linq;
using Modules.Skill.Infrastructure;
using rStarUtility.Generic.Implement.Abstract;

#endregion

namespace Modules.Skill.Core
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