#region

using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.Implement.Core;
using rStarUtility.Generic.Implement.CQRS;
using rStarUtility.Generic.Interfaces;
using rStarUtility.Generic.Usecase;
using rStarUtility.Generic.Usecase.CQRS;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Stat.UseCase
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
            Contract.RequireString(ownerId , "OwnerId");
            var statDataId = input.statDataId;
            Contract.RequireString(statDataId , "statDataId");
            var amount = input.amount;
            Contract.Require(amount >= 0 , "amount must greater than or equal 0.");

            var stat = new Entity.Stat(id , ownerId , statDataId , amount);
            repository.Save(id , stat);

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