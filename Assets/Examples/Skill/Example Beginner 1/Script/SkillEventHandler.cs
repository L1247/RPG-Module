#region

using rStar.RPGModules.Skill.Infrastructure.Events;
using rStarUtility.Generic.Implement.Core;
using rStarUtility.Generic.Interfaces;
using UnityEngine;

#endregion

namespace rStar.RPGModules.Skill.Example.Beginner1
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
            Debug.Log($"{executed.OwnerId} executed");
        }

    #endregion
    }
}