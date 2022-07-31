#region

using rStar.RPGModules.Skill.Infrastructure.Events;
using rStarUtility.Generic.Infrastructure;
using UnityEngine;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Example.Intermediate1
{
    public class SkillEventHandler : DomainEventHandler
    {
    #region Private Variables

        [Inject]
        private SkillExamplePresenter presenter;

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