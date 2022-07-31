#region

using rStar.RPGModules.Skill.Infrastructure.Events;
using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Example.Intermediate2
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
            Register<CdExit>(OnCdExit);
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

        private void OnCdExit(CdExit obj)
        {
            presenter.HideMask();
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