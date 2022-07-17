#region

using rStar.Modules.Stat.Infrastructure;
using Zenject;

#endregion

namespace rStar.Modules.Stat.Example.Scripts
{
    public class StatSampleFlow : IInitializable
    {
    #region Private Variables

        [Inject]
        private IStatController statController;

        private readonly string actorId1 = "actor1";
        private readonly string actorId2 = "actor2";
        private readonly string dataId   = "123";

        [Inject]
        private StatExamplePresenter statExamplePresenter;

        private string statId;
        private string modifierId;

    #endregion

    #region Public Methods

        public void AddModifier()
        {
            statController.AddModifier(statId , ModifierType.Flat , 5);
        }

        public void Initialize()
        {
            statController.CreateStat(actorId1 , dataId , 100);
            statController.CreateStat(actorId2 , dataId , 120);
        }

        public void RemoveModifier()
        {
            statController.RemoveModifier(statId , modifierId);
        }

        public void UpdateModifier(string statId , string modifierId)
        {
            this.modifierId = modifierId;
            statController.GetModifier(modifierId , statId);
        }

        public void UpdateStatView(string statId , string ownerId)
        {
            this.statId = statId;
            statExamplePresenter.UpdateStatView(statId , ownerId);
        }

    #endregion
    }
}