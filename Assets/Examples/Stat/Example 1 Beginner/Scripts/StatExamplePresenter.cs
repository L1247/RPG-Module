#region

using rStar.RPGModules.Stat.Infrastructure;
using rStarUtility.Util.Extensions;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Example.Beginner1
{
    public class StatExamplePresenter : IInitializable
    {
    #region Private Variables

        [Inject]
        private StatReference statReference;

        [Inject]
        private StatSampleMain main;

        [Inject]
        private IStatRepository statRepository;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            statReference.statModifyAmountButtonActor1.BindClick(() => main.AddAmount());
            statReference.addModifierButton.BindClick(() => main.AddModifier());
            statReference.removeModifierButton.BindClick(() => main.RemoveModifier());
        }

        public void UpdateStatView(string statId , string ownerId)
        {
            var textComponent = statReference.statAmountTextActor1;
            var stat          = statRepository.FindStat(statId);
            textComponent.text = $"{ownerId}'s {stat.DataId}\n" +
                                 $"Modifier's Count: {stat.Modifiers.Count}\n" +
                                 $"BaseAmount is {stat.BaseAmount}\n" +
                                 $"CalculatedAmount is {stat.CalculatedAmount}.";
        }

    #endregion
    }
}