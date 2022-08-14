#region

using RPGCore.Item.Infrastructure;

#endregion

namespace RPGCore.Item.Entity
{
    public interface IItem : IItemReadModel
    {
    #region Public Methods

        void AddStack(int       amount);
        void ChangeOwner(string newOwner);

    #endregion
    }
}