#region

using System.Collections.Generic;
using System.Linq;
using rStar.RPGModules.Stat.Infrastructure;
using rStar.RPGModules.Stat.UseCase;
using rStar.RPGModules.Stat.UseCase.Extensions;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Core.UseCase.Controller
{
    public class StatController : IStatController
    {
    #region Private Variables

        [Inject]
        private IStatRepository repository;

        [Inject]
        private CreateStatUseCase createStatUseCase;

        private readonly CreateStatInput createStatInput  = new CreateStatInput();
        private readonly Result          createStatOutput = new Result();

        [Inject]
        private ModifyAmountUseCase modifyAmountUseCase;

        private readonly ModifyAmountInput modifyAmountInput  = new ModifyAmountInput();
        private readonly Result            modifyAmountOutput = new Result();

        [Inject]
        private DeleteStatUseCase deleteStatUseCase;

        [Inject]
        private AddAmountUseCase addAmountUseCase;

        private readonly DeleteStatInput deleteStatInput = new DeleteStatInput();
        private readonly Result          deleteOutput    = new Result();
        private readonly AddAmountInput  addAmountInput  = new AddAmountInput();
        private readonly Result          addAmountOutput = new Result();

        [Inject]
        private AddModifiersUseCase addModifiersUseCase;

        [Inject]
        private RemoveModifiersUseCase removeModifiersUseCase;

        private readonly AddModifiersInput addModifiersInput = new AddModifiersInput();
        private readonly Result            addModifierOutput = new Result();

        private readonly RemoveModifierInput removeModifierInput = new RemoveModifierInput();

        private readonly Result removeModifierOutput = new Result();

        [Inject]
        private IDomainEventBus domainEventBus;

    #endregion

    #region Public Methods

        public bool AddAmount(string statId , int amount)
        {
            addAmountInput.id     = statId;
            addAmountInput.amount = amount;
            addAmountUseCase.Execute(addAmountInput , addAmountOutput);
            return addAmountOutput.GetExitCode() == ExitCode.SUCCESS;
        }

        public void AddModifier(string statId , string ownerId , ModifierType modifierType , int amount)
        {
            addModifiersInput.id            = statId;
            addModifiersInput.ownerId       = ownerId;
            addModifiersInput.modifierTypes = new List<ModifierType>() { modifierType };
            addModifiersInput.amounts       = new List<int>() { amount };
            addModifiersInput.modifierIds   = new List<string>() { GUID.NewGUID() };
            addModifiersUseCase.Execute(addModifiersInput , addModifierOutput);
        }

        public void CreateStat(string ownerId , string dataId , int amount)
        {
            createStatInput.statDataId = dataId;
            createStatInput.amount     = amount;
            createStatInput.ownerId    = ownerId;
            createStatUseCase.Execute(createStatInput , createStatOutput);
        }

        public void RemoveModifier(string statId , string modifierId)
        {
            removeModifierInput.id          = statId;
            removeModifierInput.modifierIds = new List<string>() { modifierId };
            removeModifiersUseCase.Execute(removeModifierInput , removeModifierOutput);
        }

        public void RemoveModifierByOwnerId(string statId , string ownerId)
        {
            var stat = repository.FindStat(statId);
            var modifiers = stat.Modifiers.Where(modifier => modifier.OwnerId.Equals(ownerId))
                                .Select(modifier => modifier.GetId())
                                .ToList();
            stat.TransformToDomain().RemoveModifiers(modifiers);
            domainEventBus.PostAll(stat);
        }

        public bool RemoveStat(string id)
        {
            deleteStatInput.id = id;
            deleteStatUseCase.Execute(deleteStatInput , deleteOutput);
            return deleteOutput.GetExitCode() == ExitCode.SUCCESS;
        }

        public bool RemoveStatsByOwner(string ownerId)
        {
            Contract.RequireString(ownerId , $"ownerId: {ownerId}");
            var stats = repository.FindStatsByOwnerId(ownerId).ToList();
            foreach (var stat in stats) RemoveStat(stat.GetId());
            return repository.FindStatsByOwnerId(ownerId).Any() == false;
        }


        public void SetAmount(string statId , int amount)
        {
            modifyAmountInput.id     = statId;
            modifyAmountInput.amount = amount;
            modifyAmountUseCase.Execute(modifyAmountInput , modifyAmountOutput);
        }

    #endregion
    }
}