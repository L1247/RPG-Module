#region

using System;
using rStar.RPGModules.Stat.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Stat.Example.Beginner1
{
    public class StatSampleMain : IInitializable
    {
    #region Private Variables

        [Inject]
        private IStatController statController;

        private readonly string ownerId = "Character";
        private readonly string dataId  = "ATK";

        [Inject]
        private IStatRepository repository;

        [Inject]
        private IStatController controller;

    #endregion

    #region Public Methods

        public void AddAmount()
        {
            var statId = GetStatId();
            controller.AddAmount(statId , 5);
        }

        public void AddModifier()
        {
            var statId  = GetStatId();
            var ownerId = Guid.NewGuid().ToString();
            statController.AddModifier(ownerId , statId , ModifierType.Flat , 5);
        }

        public void Initialize()
        {
            statController.CreateStat(ownerId , dataId , 100);
        }

        public void RemoveModifier()
        {
            var stat               = GetStat();
            var statId             = stat.GetId();
            var modifiersCount     = stat.Modifiers.Count;
            var isNoModifierOnStat = modifiersCount == 0;
            if (isNoModifierOnStat) return;
            var modifier   = stat.Modifiers[modifiersCount - 1];
            var modifierId = modifier.GetId();
            statController.RemoveModifier(statId , modifierId);
        }

    #endregion

    #region Private Methods

        private IStatReadModel GetStat()
        {
            var stat = repository.FindStat(ownerId , "ATK");
            return stat;
        }

        private string GetStatId()
        {
            var stat   = GetStat();
            var statId = stat.GetId();
            return statId;
        }

    #endregion
    }
}