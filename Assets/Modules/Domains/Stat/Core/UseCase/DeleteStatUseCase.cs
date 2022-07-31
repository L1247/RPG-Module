#region

using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Stat.UseCase
{
    public class DeleteStatUseCase : UseCase<DeleteStatInput , Result , IStatRepository>
    {
    #region Constructor

        public DeleteStatUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(DeleteStatInput input , Result output)
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