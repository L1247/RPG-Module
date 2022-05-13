#region

using System.Collections.Generic;
using RPGCore.Stat.Infrastructure;
using RPGCore.Stat.UseCase;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Utilities;
using Utilities.Contract;
using Zenject;

#endregion

namespace RPGCore.Stat.Core.UseCase.Controller
{
    public class StatController : IStatController
    {
    #region Private Variables

        [Inject]
        private IStatRepository repository;

        [Inject]
        private CreateStatUseCase createStatUseCase;

        private readonly CreateStatInput      createStatInput  = new CreateStatInput();
        private readonly CqrsCommandPresenter createStatOutput = CqrsCommandPresenter.NewInstance();

        [Inject]
        private ModifyAmountUseCase modifyAmountUseCase;

        private readonly ModifyAmountInput    modifyAmountInput  = new ModifyAmountInput();
        private readonly CqrsCommandPresenter modifyAmountOutput = CqrsCommandPresenter.NewInstance();

        [Inject]
        private DeleteStatUseCase deleteStatUseCase;

        [Inject]
        private AddAmountUseCase addAmountUseCase;

        private readonly DeleteStatInput      deleteStatInput = new DeleteStatInput();
        private readonly CqrsCommandPresenter deleteOutput    = CqrsCommandPresenter.NewInstance();
        private readonly AddAmountInput       addAmountInput  = new AddAmountInput();
        private readonly CqrsCommandPresenter addAmountOutput = CqrsCommandPresenter.NewInstance();

        [Inject]
        private AddModifiersUseCase addModifiersUseCase;

        [Inject]
        private RemoveModifiersUseCase removeModifiersUseCase;

        private readonly AddModifiersInput    addModifiersInput = new AddModifiersInput();
        private readonly CqrsCommandPresenter addModifierOutput = CqrsCommandPresenter.NewInstance();

        private readonly RemoveModifierInput removeModifierInput = new RemoveModifierInput();

        private readonly CqrsCommandPresenter removeModifierOutput = CqrsCommandPresenter.NewInstance();

    #endregion

    #region Public Methods

        public bool AddAmount(string statId , int amount)
        {
            addAmountInput.id     = statId;
            addAmountInput.amount = amount;
            addAmountUseCase.Execute(addAmountInput , addAmountOutput);
            return addAmountOutput.GetExitCode() == ExitCode.SUCCESS;
        }

        public void AddModifier(string statId , ModifierType modifierType , int amount)
        {
            addModifiersInput.id            = statId;
            addModifiersInput.modifierTypes = new List<ModifierType>() { modifierType };
            addModifiersInput.amounts       = new List<int>() { amount };
            addModifiersInput.modifierIds   = new List<string>() { GUID.NewGUID() };
            addModifiersUseCase.Execute(addModifiersInput , addModifierOutput);
        }

        public void CreateStat(string actorId , string dataId , int amount)
        {
            createStatInput.statDataId = dataId;
            createStatInput.amount     = amount;
            createStatInput.ownerId    = actorId;
            createStatUseCase.Execute(createStatInput , createStatOutput);
        }

        public void DeleteStat(string ownerId)
        {
            var statReadModels = (IEnumerable<IStatReadModel>)repository.FindStatsByOwnerId(ownerId);
            foreach (var stat in statReadModels)
            {
                deleteStatInput.id = stat.GetId();
                deleteStatUseCase.Execute(deleteStatInput , deleteOutput);
            }
        }

        public IModifier GetModifier(string statId , string modifierId)
        {
            var modifier = repository.FindModifer(statId , modifierId);
            return modifier;
        }

        public IStatReadModel GetStat(string statId)
        {
            var statReadModel = repository.FindStat(statId);
            Contract.RequireNotNull(statReadModel , "statReadModel");
            return statReadModel;
        }

        public IStatReadModel GetStat(string actorId , string dataId)
        {
            var statReadModel = repository.FindStat(actorId , dataId);
            // Contract.RequireNotNull(statReadModel , $"{dataId} , stat ");
            return statReadModel;
        }

        public void RemoveModifier(string statId , string modifierId)
        {
            removeModifierInput.id          = statId;
            removeModifierInput.modifierIds = new List<string>() { modifierId };
            removeModifiersUseCase.Execute(removeModifierInput , removeModifierOutput);
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