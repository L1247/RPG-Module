#region

using RPGCore.Item.Infrastructure;
using RPGCore.Item.UseCase.Extension;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace RPGCore.Item.UseCase
{
    public class ChangeOwnerUseCase : UseCase<ChangeOwnerInput , Result , IItemRepository>
    {
    #region Constructor

        public ChangeOwnerUseCase(IDomainEventBus domainEventBus , IItemRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(ChangeOwnerInput input , Result output)
        {
            var id = input.id;
            Contract.RequireString(id , "id");
            var ownerId = input.ownerId;
            Contract.RequireString(ownerId , "ownerId");
            var itemReadModel = repository.FindById(id);
            Contract.RequireNotNull(itemReadModel , "itemReadModel");
            var item            = itemReadModel.TransformToDomain();
            var dataId          = item.DataId;
            var sameDataIdItems = repository.GetAllItemByDataId(dataId);
            if (sameDataIdItems.Count > 1)
            {
                repository.DeleteById(id);
                item = sameDataIdItems[0].TransformToDomain();
                item.AddStack(1);
            }
            else
            {
                item.ChangeOwner(ownerId);
            }

            domainEventBus.PostAll(item);

            output.SetId(id);
            output.SetExitCode(ExitCode.SUCCESS);
        }

    #endregion
    }

    public class ChangeOwnerInput : Input
    {
    #region Public Variables

        public string id;
        public string ownerId;

    #endregion
    }
}