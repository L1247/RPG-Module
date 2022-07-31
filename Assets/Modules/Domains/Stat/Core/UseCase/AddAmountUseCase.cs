#region

using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase.Extensions;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Stat.UseCase
{
    public class AddAmountUseCase : UseCase<AddAmountInput , Result , IStatRepository>
    {
    #region Constructor

        public AddAmountUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(AddAmountInput input , Result output)
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