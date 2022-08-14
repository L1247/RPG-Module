#region

using rStar.RPGModules.Item.Infrastructure;
using rStarUtility.Generic.Infrastructure;
using rStarUtility.Util;

#endregion

namespace rStar.RPGModules.Item.UseCase
{
    public class CreateItemUseCase : UseCase<CreateItemInput , Result , IItemRepository>
    {
    #region Constructor

        public CreateItemUseCase(IDomainEventBus domainEventBus , IItemRepository repository) : base(domainEventBus , repository) { }

    #endregion

    #region Public Methods

        public override void Execute(CreateItemInput input , Result output)
        {
            var id = GUID.NewGUID();

            var ownerId = input.ownerId;
            Contract.RequireString(ownerId , "ownerId");
            var dataId = input.dataId;
            Contract.RequireString(dataId , "dataId");

            var item = new Entity.Item(id , ownerId , dataId , input.stackable);
            repository.Save(id , item);

            domainEventBus.PostAll(item);
            output.SetId(id);
            output.SetExitCode(ExitCode.SUCCESS);
        }

    #endregion
    }

    public class CreateItemInput : Input
    {
    #region Public Variables

        public bool   stackable;
        public string dataId;
        public string ownerId;

    #endregion
    }
}