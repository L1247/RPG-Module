#region

using Modules.Skill.Infrastructure.Events;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using UnityEngine;
using Zenject;

#endregion

namespace Modules.Skill.Example2
{
    public class SkillEventHandler : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private SkillExample2Presenter presenter;

    #endregion

    #region Constructor

        public SkillEventHandler(IDomainEventBus domainEventBus) : base(domainEventBus)
        {
            Register<CastEntered>(OnCastEntered);
            Register<Executed>(OnExecuted);
        }

    #endregion

    #region Private Methods

        private void OnCastEntered(CastEntered entered)
        {
            Debug.Log($"OnCastEntered: {entered.ID}");
            presenter.PlayCastEnter();
        }

        private void OnExecuted(Executed executed)
        {
            Debug.Log($"OnExecuted : {executed.ID}");
            presenter.PlayAfterCast();
            presenter.SpawnProjectile();
        }

    #endregion
    }
}