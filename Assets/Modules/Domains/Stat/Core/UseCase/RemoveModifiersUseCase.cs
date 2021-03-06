#region

using System.Collections.Generic;
using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase.Extensions;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Stat.UseCase
{
    public class RemoveModifiersUseCase : UseCase<RemoveModifierInput , Result , IStatRepository>
    {
    #region Constructor

        public RemoveModifiersUseCase(IDomainEventBus domainEventBus , IStatRepository repository) :
            base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(RemoveModifierInput input , Result output)
        {
            var id = input.id;
            Contract.RequireString(id , "Id");
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
        ///     stat's Id
        /// </summary>
        public string id;

    #endregion
    }
}