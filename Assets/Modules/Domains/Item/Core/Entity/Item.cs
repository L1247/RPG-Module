#region

using rStar.RPGModules.Item.Infrastructure.Event;
using rStarUtility.Generic.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Entity
{
    public class Item : AggregateRoot , IItem
    {
    #region Public Variables

        public bool   Stackable  { get; }
        public int    StackCount { get; private set; }
        public string DataId     { get; }
        public string OwnerId    { get; private set; }

    #endregion

    #region Constructor

        public Item(string id , string ownerId , string dataId , bool stackable) : base(id)
        {
            OwnerId    = ownerId;
            DataId     = dataId;
            Stackable  = stackable;
            StackCount = 1;
            AddDomainEvent(new ItemCreated(GetId() , dataId , ownerId));
        }

    #endregion

    #region Public Methods

        public void AddStack(int amount)
        {
            if (Stackable == false) return;
            StackCount += amount;
            AddDomainEvent(new StackChanged(DataId , StackCount));
        }

        public void ChangeOwner(string newOwner)
        {
            OwnerId = newOwner;
            AddDomainEvent(new OwnerChanged(GetId() , OwnerId));
        }

    #endregion
    }
}