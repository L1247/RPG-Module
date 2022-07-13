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
            Contract.RequireString(id , "Id");
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