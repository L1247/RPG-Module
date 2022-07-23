#region

using System.Linq;
using Zenject;

#endregion

namespace Modules.Skill.Core
{
    public class SkillManager
    {
    #region Private Variables

        [Inject]
        private SkillRegistry registry;

    #endregion

    #region Public Methods

        public Skill GetSkillByOwner(string ownerId)
        {
            return registry.GetAll().FirstOrDefault(skill => skill.OwnerId.Equals(ownerId));
        }

    #endregion
    }
}