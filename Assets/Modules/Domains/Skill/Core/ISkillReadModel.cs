#region

using rStarUtility.DDD.Model;

#endregion

namespace Modules.Skill.Core
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