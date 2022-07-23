#region

using Modules.Stat.Infrastructure;
using Modules.Stat.UseCase.Extensions;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Util;

#endregion

namespace Modules.Stat.UseCase
{
    public class ModifyAmountUseCase : UseCase<ModifyAmountInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public ModifyAmountUseCase(IDomainEventBus domainEventBus , IStatRepository repository) :
            base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(ModifyAmountInput input , CqrsCommandPresenter output)
        {
            var id = input.id;
            Contract.RequireString(id , "Id");
            var statReadModel = repository.FindById(id);

            if (statReadModel != null)
            {
                statReadModel.TransformToDomain().SetBaseAmount(input.amount);
                domainEventBus.PostAll(statReadModel);
                output.SetExitCode(ExitCode.SUCCESS);
            }
            else
            {
                output.SetExitCode(ExitCode.FAILURE);
            }

            output.SetId(id);
        }

    #endregion
    }

    public class ModifyAmountInput : Input
    {
    #region Public Variables

        public int    amount;
        public string id;

    #endregion
    }
}