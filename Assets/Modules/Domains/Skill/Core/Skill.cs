#region

using System;
using rStar.Modules.Skill.Core.Event;
using rStarUtility.DDD.Event;
using rStarUtility.DDD.Model;
using Zenject;

#endregion

namespace rStar.Modules.Skill.Core
{
    public class Skill : IPoolable<IMemoryPool> , IDisposable , IEntity<string>
    {
    #region Public Variables

        public bool IsCast { get; private set; }

        public bool IsCd { get; private set; }

        public float Cast { get; private set; }

        public float Cd { get; private set; }

        public float DefaultCast { get; private set; }
        public float DefaultCd   { get; private set; }

        public string OwnerId { get; private set; }

    #endregion

    #region Private Variables

        private IMemoryPool pool;

        [Inject]
        private SkillRegistry skillRegistry;

        [Inject]
        private IDomainEventBus domainEventBus;

        private string id;

    #endregion

    #region Public Methods

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public void Execute()
        {
            domainEventBus.Post(new Executed(GetId() , OwnerId));
        }

        public string GetId()
        {
            return id;
        }

        public void Init(string ownerId , float cast , float cd)
        {
            id          = Guid.NewGuid().ToString();
            DefaultCd   = cd;
            DefaultCast = cast;
            OwnerId     = ownerId;
            Cd          = 0;
            Cast        = DefaultCast;
            IsCd        = false;
            IsCast      = false;
        }

        public void OnDespawned()
        {
            pool = null;
            skillRegistry.RemoveSkill(this);
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
            skillRegistry.AddSkill(this);
        }

        public void Tick(float time)
        {
            Cd   -= time;
            Cast -= time;
            if (IsCast)
                if (Cast <= 0)
                    ExitCast();
        }

        public void UseSkill()
        {
            if (IsCd) return;
            if (DefaultCast <= 0) Execute();
            else if (IsCast == false) EnterCast();
            if (DefaultCd > 0) EnterCd();
        }

    #endregion

    #region Private Methods

        private void EnterCast()
        {
            Cast   = DefaultCast;
            IsCast = true;
            domainEventBus.Post(new CastEntered(GetId()));
        }

        private void EnterCd()
        {
            IsCd = true;
            Cd   = DefaultCd;
        }

        private void ExitCast()
        {
            IsCast = false;
            Execute();
        }

    #endregion

    #region Nested Types

        public class Factory : PlaceholderFactory<Skill> { }

    #endregion
    }
}