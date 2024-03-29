namespace rStar.RPGModules.Skill.Infrastructure
{
    public interface ISkillController
    {
    #region Public Methods

        void CreateSkill(string         ownerId , string dataId , float cast , float cd);
        void ExecuteSkill(string        id);
        void Interrupt(string           id);
        void RemoveSkill(string         Id);
        void RemoveSkillsByOwner(string ownerId);
        void TickSkill(string           id , float time);
        void UseSkill(string            id);

    #endregion
    }
}