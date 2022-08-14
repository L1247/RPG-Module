#region

using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Event
{
    public class StackChanged : DomainEvent
    {
    #region Public Variables

        public int    Count  { get; }
        public string DataId { get; }

    #endregion

    #region Constructor

        public StackChanged(string dataId , int count)
        {
            Count  = count;
            DataId = dataId;
        }

    #endregion
    }
}