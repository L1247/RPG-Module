#region

using System.Collections.Generic;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.UseCase.Extensions;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Util;

#endregion

namespace rStar.Modules.Stat.UseCase
{
    public class RemoveModifiersUseCase : UseCase<RemoveModifierInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public RemoveModifiersUseCase(IDomainEventBus domainEventBus , IStatRepository repository) :
            base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(RemoveModifierInput input , CqrsCommandPresenter output)
        {
            var id = input.id;
            Contract.RequireString(id , "id");
            var statReadModel = repository.FindById(id);
            if (statReadModel != null)
            {
                var modifierIds = input.modifierIds;
                Contract.RequireNotNull(modifierIds , "modifierIds");
                statReadModel.TransformToDomain().RemoveModifiers(modifierIds);
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

    public class RemoveModifierInput : Input
    {
    #region Public Variables

        public List<string> modifierIds;

        /// <summary>
        ///     stat's id
        /// </summary>
        public string id;

    #endregion
    }
}