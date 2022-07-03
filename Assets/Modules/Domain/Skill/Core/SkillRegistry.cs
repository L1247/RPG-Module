#region

using System.Collections.Generic;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class SkillRegistry
    {
    #region Public Variables

        public IEnumerable<Skill> Skills => skills;

    #endregion

    #region Private Variables

        private readonly List<Skill> skills = new List<Skill>();

    #endregion

    #region Public Methods

        public void AddSkill(Skill skill)
        {
            skills.Add(skill);
        }

        public void RemoveSkill(Skill skill)
        {
            skills.Remove(skill);
        }

    #endregion
    }
}