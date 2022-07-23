#region

using System.Linq;
using rStarUtility.DDD.Implement.Abstract;

#endregion

namespace Modules.Skill.Core
{
    public class SkillRepository : GenericRepository<ISkill>
    {
    #region Public Methods

        public ISkill GetSkillByOwner(string ownerId)
        {
            return GetAll().FirstOrDefault(skill => skill.OwnerId.Equals(ownerId));
        }

    #endregion
    }
}