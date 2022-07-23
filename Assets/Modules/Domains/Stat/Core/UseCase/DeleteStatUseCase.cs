#region

using Modules.Stat.Infrastructure;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Util;

#endregion

namespace Modules.Stat.UseCase
{
    public class DeleteStatUseCase : UseCase<DeleteStatInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public DeleteStatUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(DeleteStatInput input , CqrsCommandPresenter output)
        {
            var statId = input.id;
            Contract.RequireString(statId , "statId");
            if (repository.DeleteById(statId))
            {
                output.SetId(statId);
                output.SetExitCode(ExitCode.SUCCESS);
            }
        }

    #endregion
    }

    public class DeleteStatInput : Input
    {
    #region Public Variables

        public string id;

    #endregion
    }
}