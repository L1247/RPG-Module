#region

#endregion

#region

using rStarUtility.Generic.Model;

#endregion

namespace Modules.Skill.Infrastructure
{
    public interface ISkillReadModel : IEntity<string>
    {
    #region Public Variables

        bool   IsCast      { get; }
        bool   IsCd        { get; }
        float  Cast        { get; }
        float  Cd          { get; }
        float  DefaultCast { get; }
        float  DefaultCd   { get; }
        string DataId      { get; }
        string OwnerId     { get; }

    #endregion
    }
}