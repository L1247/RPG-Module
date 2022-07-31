#region

using rStar.RPGModules.Skill.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Combat.Example.Beginner1
{
    public class CombatMain : IInitializable , ITickable
    {
    #region Private Variables

        [Inject]
        private ISkillController skillController;

        [Inject]
        private ISkillRepository skillRepository;

    #endregion

    #region Public Methods

        public void Initialize()
        {
            skillController.CreateSkill("Player1" , "Skill1" , 0.9f , 0);
        }

        public void Tick()
        {
            foreach (var skill in skillRepository.GetAll()) skillController.UseSkill(skill.GetId());
        }

    #endregion
    }
}