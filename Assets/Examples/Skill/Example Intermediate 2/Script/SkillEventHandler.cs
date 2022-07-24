#region

using Modules.Skill.Infrastructure.Events;
using rStarUtility.Generic.Implement.Core;
using rStarUtility.Generic.Interfaces;
using Zenject;

#endregion

namespace Modules.Skill.Example.Intermediate2
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
            Register<CdEntered>(OnCdEntered);
            Register<Ticked>(OnTicked);
        }

    #endregion

    #region Private Methods

        private void OnCastEntered(CastEntered entered)
        {
            presenter.PlayCastEnter();
        }

        private void OnCdEntered(CdEntered obj)
        {
            presenter.ShowMask();
        }

        private void OnExecuted(Executed executed)
        {
            presenter.PlayAfterCast();
            presenter.SpawnProjectile();
        }

        private void OnTicked(Ticked ticked)
        {
            presenter.UpdateInfo();
        }

    #endregion
    }
}