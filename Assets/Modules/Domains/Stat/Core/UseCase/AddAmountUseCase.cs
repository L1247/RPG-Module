#region

using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase.Extensions;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;

#endregion

namespace rStar.Modules.Stat.UseCase
{
    public class AddAmountUseCase : UseCase<AddAmountInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public AddAmountUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(AddAmountInput input , CqrsCommandPresenter output)
        {
            var statReadModel = repository.FindById(input.id);
            if (statReadModel != null)
            {
                statReadModel.TransformToDomain().AddBaseAmount(input.amount);
                domainEventBus.PostAll(statReadModel);
                output.SetExitCode(ExitCode.SUCCESS);
            }
            else
            {
                output.SetExitCode(ExitCode.FAILURE);
            }

            output.SetId(input.id);
        }

    #endregion
    }

    public class AddAmountInput : Input
    {
    #region Public Variables

        public int    amount;
        public string id;

    #endregion
    }
}