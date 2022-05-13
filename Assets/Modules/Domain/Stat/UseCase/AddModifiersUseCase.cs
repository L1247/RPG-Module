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
    /// <summary>
    ///     節省計算，可一次傳新增多個Modifier
    /// </summary>
    public class AddModifiersUseCase : UseCase<AddModifiersInput , CqrsCommandPresenter , IStatRepository>
    {
    #region Constructor

        public AddModifiersUseCase(IDomainEventBus domainEventBus , IStatRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(AddModifiersInput input , CqrsCommandPresenter output)
        {
            var id = input.id;
            Contract.RequireString(id , "id");
            var statReadModel = repository.FindById(id);
            if (statReadModel != null)
            {
                var modifierIds = input.modifierIds;
                Contract.RequireNotNull(modifierIds , "modifierIds");
                var modifierTypes = input.modifierTypes;
                Contract.RequireNotNull(modifierTypes , "modifierTypes");
                var amounts = input.amounts;
                Contract.RequireNotNull(amounts , "amounts");
                statReadModel.TransformToDomain().AddModifiers(modifierIds , modifierTypes , amounts);
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

    public class AddModifiersInput : Input
    {
    #region Public Variables

        public List<int>          amounts;
        public List<ModifierType> modifierTypes;
        public List<string>       modifierIds;
        public string             id;

    #endregion
    }
}