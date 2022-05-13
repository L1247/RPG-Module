#region

using AutoBot.Utilities.Extensions;
using RPGCore.Stat.Infrastructure;
using TMPro;
using Zenject;

#endregion

namespace RPGCore.Stat.Example.Scripts
{
    public class StatExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private StatReference statReference;

        [Inject]
        private IStatController statController;

        private string cachedStatId1;
        private string cachedStatId2;

        [Inject]
        private StatSampleFlow statSampleFlow;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            statReference.statModifyAmountButtonActor1.BindClick(() => statController.AddAmount(cachedStatId1 , 5));
            statReference.statModifyAmountButtonActor2.BindClick(() => statController.AddAmount(cachedStatId2 , 1));
            statReference.addModifierButton.BindClick(() => statSampleFlow.AddModifier());
            statReference.removeModifierButton.BindClick(() => statSampleFlow.RemoveModifier());
        }

        public void UpdateStatView(string statId)
        {
            var stat = statController.GetStat(statId);

            if (statId.Equals(cachedStatId1)) statReference.statAmountTextActor1.text = $"Current BaseAmount: {stat.BaseAmount}";
            if (statId.Equals(cachedStatId2)) statReference.statAmountTextActor2.text = $"Current BaseAmount: {stat.BaseAmount}";
        }

        public void UpdateStatView(string statId , string ownerId)
        {
            TMP_Text textComponent = null;
            if (ownerId.Equals("actor1"))
            {
                textComponent = statReference.statAmountTextActor1;
                cachedStatId1 = statId;
            }
            else if (ownerId.Equals("actor2"))
            {
                textComponent = statReference.statAmountTextActor2;
                cachedStatId2 = statId;
            }

            var stat = statController.GetStat(statId);
            textComponent.text = $"{ownerId} , {stat.DataId} , {statId} {stat.CalculatedAmount}";
        }

    #endregion
    }
}