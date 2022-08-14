#region

using rStar.RPGModules.Item.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.Entity
{
    public interface IItem : IItemReadModel

#region Public Methods

    {
    #region Public Methods

        void AddStack(int       amount);
        void ChangeOwner(string newOwner);

    #endregion
    }
}