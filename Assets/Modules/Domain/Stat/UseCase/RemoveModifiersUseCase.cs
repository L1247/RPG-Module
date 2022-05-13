#region

using System.Collections.Generic;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Implement.Core;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase;
using rStarUtility.DDD.Usecase.CQRS;
using Stat.Infrastructure;
using Stat.UseCase.Extensions;
using Utilities.Contract;

#endregion

namespace Stat.UseCase
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