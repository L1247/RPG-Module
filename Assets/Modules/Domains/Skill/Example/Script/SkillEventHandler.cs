#region

using rStar.Modules.Skill.Core.Event;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using UnityEngine;

#endregion

namespace rStar.Modules.Skill.Example
{
    public class SkillEventHandler : DomainEventHandler
    {
    #region Constructor

        public SkillEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<Executed>(OnExecuted);
        }

    #endregion

    #region Private Methods

        private void OnExecuted(Executed executed)
        {
            Debug.Log($"OnExecuted: {executed.OwnerId}");
        }

    #endregion
    }
}