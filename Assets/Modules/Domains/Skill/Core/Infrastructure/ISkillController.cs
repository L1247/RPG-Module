namespace Modules.Domains.Skill.Core.Infrastructure
{
    public interface ISkillController
    {
    #region Public Methods

        void ExecuteSkill(string id);
        void TickSkill(string    id , int time);
        void UseSkill(string     id);

    #endregion
    }
}