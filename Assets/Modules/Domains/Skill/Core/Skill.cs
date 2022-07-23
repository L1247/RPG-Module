#region

using System;
using Modules.Skill.Infrastructure;
using Modules.Skill.Infrastructure.Events;
using rStarUtility.DDD.Event;
using UnityEngine;
using Zenject;

#endregion

namespace Modules.Skill.Core
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
        private ISkillRepository skillRepository;

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

        public void EnterCast()
        {
            Cast   = DefaultCast;
            IsCast = true;
            domainEventBus.Post(new CastEntered(GetId() , OwnerId));
        }

        public void EnterCd()
        {
            IsCd = true;
            Cd   = DefaultCd;
        }

        public void Execute()
        {
            domainEventBus.Post(new Executed(GetId() , OwnerId , DataId));
        }

        public void ExitCast()
        {
            IsCast = false;
            Execute();
        }

        public void ExitCd()
        {
            IsCd = false;
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
        }

        public void OnDespawned()
        {
            Dispose();
            pool = null;
            skillRepository.DeleteById(GetId());
        }

        public void OnSpawned(IMemoryPool pool)
        {
            this.pool = pool;
            skillRepository.Save(GetId() , this);
        }

        public void Tick(float time)
        {
            Debug.Log($"tick : {time}");
            Cd   -= time;
            Cast -= time;
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

    #region Nested Types

        public class Factory : PlaceholderFactory<Skill> { }

    #endregion
    }
}