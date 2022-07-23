#region

using Modules.Skill.Infrastructure;

#endregion

namespace Modules.Skill.Core
{
    public interface ISkill : ISkillReadModel
    {
    #region Public Methods

        void EnterCast();
        void EnterCd();
        void Execute();
        void ExitCast();
        void ExitCd();
        void Init(string ownerId , string dataId , float cast , float cd);
        void Tick(float  time);
        void UseSkill();

    #endregion
    }
}