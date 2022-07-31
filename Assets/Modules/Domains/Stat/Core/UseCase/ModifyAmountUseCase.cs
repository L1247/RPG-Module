#region

using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase.Extensions;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Stat.UseCase
{
    public class ModifyAmountUseCase : UseCase<ModifyAmountInput , Result , IStatRepository>
    {
    #region Constructor

        public ModifyAmountUseCase(IDomainEventBus domainEventBus , IStatRepository repository) :
            base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(ModifyAmountInput input , Result output)
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