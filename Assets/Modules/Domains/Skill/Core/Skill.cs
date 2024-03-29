#region

using System;
using rStar.RPGModules.Skill.Infrastructure;
using rStar.RPGModules.Skill.Infrastructure.Events;
using rStarUtility.Generic.Infrastructure;
using Zenject;

#endregion

namespace rStar.RPGModules.Skill.Core
{
    public class Skill : IPoolable<IMemoryPool> , IDisposable , ISkill
    {
    #region Public Variables

        public bool IsCast { get; private set; }

        public bool IsCd { get; private set; }

        public float Cast { get; private set; }

        public float Cd { get; private set; }

        public float  DefaultCast { get; private set; }
        public float  DefaultCd   { get; private set; }
        public string DataId      { get; private set; }

        public string OwnerId { get; private set; }

    #endregion

    #region Private Variables

        private IMemoryPool pool;

        [Inject]
        private IDomainEventBus domainEventBus;

        private string id;

    #endregion

    #region Constructor

        public Skill()
        {
            id = Guid.NewGuid().ToString();
        }

    #endregion

    #region Public Methods

        public void Dispose()
        {
            pool.Despawn(this);
        }

        public void EnterCd()
        {
            IsCd = true;
            Cd   = DefaultCd;
            domainEventBus.Post(new CdEntered(GetId()));
        }

        public void Execute()
        {
            domainEventBus.Post(new Executed(GetId() , OwnerId , DataId));
        }

        public void ExitCd()
        {
            IsCd = false;
            domainEventBus.Post(new CdExit(GetId()));
        }

        public string GetId()
        {
            return id;
        }

        public void Init(string ownerId , string dataId , float cast , float cd)
        {
            DefaultCd   = cd;
            DefaultCast = cast;
            OwnerId     = ownerId;
            DataId      = dataId;
            Cd          = 0;
            Cast        = DefaultCast;
            IsCd        = false;
            IsCast      = false;
            domainEventBus.Post(new SkillCreated(GetId() , DataId));
        }

        public void Interrupt()
        {
            IsCast = false;
        }

        public void OnDespawned()
        {
            Dispose();
            pool = null;
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
        }

        public void Tick(float time)
        {
            Cd   -= time;
            Cast -= time;
            domainEventBus.Post(new Ticked(GetId() , Cast , Cd));
            if (IsCast)
                if (Cast <= 0)
                    ExitCast();
            if (IsCd)
                if (Cd <= 0)
                    ExitCd();
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
            domainEventBus.Post(new CastEntered(GetId() , OwnerId , DataId));
        }

        private void ExitCast()
        {
            IsCast = false;
            Execute();
        }

    #endregion

    #region Nested Types

        public class Pool : MemoryPool<ISkillReadModel> { }

    #endregion
    }
}