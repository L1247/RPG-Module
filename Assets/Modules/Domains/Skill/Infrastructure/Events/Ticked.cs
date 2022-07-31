#region

#endregion

#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Skill.Infrastructure.Events
{
    public class Ticked : DomainEvent
    {
    #region Public Variables

        public float Cast { get; }
        public float Cd   { get; }

        public string Id { get; }

    #endregion

    #region Constructor

        public Ticked(string id , float cast , float cd)
        {
            Id   = id;
            Cast = cast;
            Cd   = cd;
        }

    #endregion
    }
}