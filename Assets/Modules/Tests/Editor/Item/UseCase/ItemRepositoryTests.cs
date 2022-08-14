#region

using NSubstitute;
using NUnit.Framework;
using rStar.RPGModules.Item.Infrastructure;

#endregion

namespace rStar.RPGModules.Item.UseCase.Tests
{
    public class ItemRepositoryTests
    {
    #region Test Methods

        [Test]
        public void GetAllItemByDataId()
        {
            var itemRepository = new ItemRepository();
            var dataId         = "dataId";
            var item1          = GivenAItem("id1" , dataId);
            var item2          = GivenAItem("id2" , dataId);
            itemRepository.Save("id1" , item1);
            itemRepository.Save("id2" , item2);
            var allItemByDataId = itemRepository.GetAllItemByDataId(dataId);
            Assert.AreEqual(2 , allItemByDataId.Count , "count is not equal");
        }

    #endregion

    #region Private Methods

        private static IItemReadModel GivenAItem(string id , string dataId)
        {
            var item = Substitute.For<IItemReadModel>();
            item.GetId().Returns(id);
            item.DataId.Returns(dataId);
            return item;
        }

    #endregion
    }
}