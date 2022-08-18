#region

using rStar.RPGModules.Item.Infrastructure;
using rStar.RPGModules.Item.UseCase.Extension;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Item.UseCase
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
            var exitCode        = ExitCode.SUCCESS;
            var item            = itemReadModel.TransformToDomain();
            var dataId          = item.DataId;
            var sameDataIdItems = repository.GetAllItemByDataId(dataId);
            // todo: Test - stackable == false
            if (sameDataIdItems.Count > 1 && item.Stackable)
            {
                repository.DeleteById(id);
                item = sameDataIdItems[0].TransformToDomain();
                item.AddStack(1);
            }
            else
            {
                // SameOwnerId
                if (ownerId.Equals(item.OwnerId))
                    exitCode = ExitCode.FAILURE;
                else item.ChangeOwner(ownerId);
            }

            output.SetExitCode(exitCode);
            output.SetId(id);
            if (exitCode == ExitCode.SUCCESS)
                domainEventBus.PostAll(item);
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