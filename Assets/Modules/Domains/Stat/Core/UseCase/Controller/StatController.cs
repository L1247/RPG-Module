#region

using System;
using System.Collections.Generic;
using Modules.Stat.Infrastructure;
using Modules.Stat.UseCase;
using rStarUtility.DDD.Implement.CQRS;
using rStarUtility.DDD.Usecase.CQRS;
using rStarUtility.Util;
using Zenject;

#endregion

namespace Modules.Stat.Core.UseCase.Controller
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

        public void CreateStat(string ownerId , string dataId , int amount)
        {
            createStatInput.statDataId = dataId;
            createStatInput.amount     = amount;
            createStatInput.ownerId    = ownerId;
            createStatUseCase.Execute(createStatInput , createStatOutput);
        }

        public void RemoveAllModifier(string statId)
        {
            throw new NotImplementedException();
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