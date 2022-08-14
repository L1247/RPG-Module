#region

using Zenject;

#endregion

namespace rStar.RPGModules.Item.Infrastructure.Adapter
{
    public class ItemProvider
    {
    #region Private Variables

        [Inject]
        private IItemRepository itemRepository;

    #endregion

    #region Public Methods

        public IItemReadModel GetItem(string id)
        {
            return itemRepository.FindById(id);
        }

    #endregion
    }
}