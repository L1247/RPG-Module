#region

using RPGCore.Stat.Infrastructure;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Utilities;
using Utilities.Contract;

#endregion

namespace RPGCore.Stat.UseCase
{
    public class CreateStatUseCase : UseCase<CreateStatInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public CreateStatUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(CreateStatInput input , CqrsCommandPresenter output)
        {
            var id = GUID.NewGUID();

            var ownerId = input.ownerId;
            Contract.RequireString(ownerId , "ownerId");
            var statDataId = input.statDataId;
            Contract.RequireString(statDataId , "statDataId");
            var amount = input.amount;
            Contract.Require(amount >= 0 , "amount must greater than or equal 0.");

            var stat = new Entity.Stat(id , ownerId , statDataId , amount);
            repository.Save(stat);

            domainEventBus.PostAll(stat);

            output.SetId(id);
            output.SetExitCode(ExitCode.SUCCESS);
        }

    #endregion
    }

    public class CreateStatInput : Input
    {
    #region Public Variables

        public int    amount;
        public string ownerId;
        public string statDataId;

    #endregion
    }
}