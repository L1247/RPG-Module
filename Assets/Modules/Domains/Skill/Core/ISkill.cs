#region

using rStar.RPGModules.Skill.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public interface ISkill : ISkillReadModel
    {
    #region Public Methods

        void Execute();
        void Init(string ownerId , string dataId , float cast , float cd);

        void Interrupt();
        void Tick(float time);
        void UseSkill();

    #endregion
    }
}