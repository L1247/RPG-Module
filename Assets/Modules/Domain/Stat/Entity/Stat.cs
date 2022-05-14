#region

using System;
using System.Collections.Generic;
using System.Linq;
using rStar.Modules.Stat.Infrastructure;
using rStar.Modules.Stat.Infrastructure.Events;
using rStarUtility.DDD.Implement.Core;
using Utilities.Contract;

#endregion

namespace rStar.Modules.Stat.Entity
{
    public class Stat : AggregateRoot , IStat
    {
    #region Public Variables

        public int             BaseAmount       { get; private set; }
        public int             CalculatedAmount { get; private set; }
        public List<IModifier> Modifiers        { get; }
        public string          DataId           { get; }
        public string          OwnerId          { get; }

    #endregion

    #region Constructor

        public Stat(string id , string ownerId , string statDataId , int baseAmount) : base(id)
        {
            DataId           = statDataId;
            BaseAmount       = baseAmount;
            CalculatedAmount = baseAmount;
            OwnerId          = ownerId;
            Modifiers        = new List<IModifier>();
            AddDomainEvent(new StatCreated(id , statDataId , ownerId));
        }

    #endregion

    #region Public Methods

        public void AddBaseAmount(int amount)
        {
            SetBaseAmount(BaseAmount + amount);
        }

        public void AddModifiers(List<string> modifierIds , List<ModifierType> modifierTypes , List<int> amounts)
        {
            var wantToAddCount = modifierIds.Count;
            Contract.Require(0 != wantToAddCount ,                   "modifierId's count is zero");
            Contract.Require(0 != modifierTypes.Count ,              "modifierType's count is zero");
            Contract.Require(0 != amounts.Count ,                    "amount's count is zero");
            Contract.Require(wantToAddCount == modifierTypes.Count , "id with type count is not equal.");
            Contract.Require(wantToAddCount == amounts.Count ,       "id with amount count is not equal.");

            var modifiersCount = Modifiers.Count;
            for (var i = 0 ; i < wantToAddCount ; i++)
            {
                var modifierId   = modifierIds[i];
                var modifierType = modifierTypes[i];
                var amount       = amounts[i];
                AddModifier(modifierId , modifierType , amount);
            }

            Contract.Ensure(Modifiers.Count - modifiersCount == wantToAddCount , "modifier count is not equal.");
            Calculate();
        }

        public IModifier GetModifier(string modifierId)
        {
            var modifier = Modifiers.Find(modifier => modifier.GetId().Equals(modifierId));
            return modifier;
        }

        public void RemoveModifiers(List<string> modifierIds)
        {
            var modifierCountShouldEqualIds = Modifiers.Count >= modifierIds.Count;
            Contract.Require(modifierCountShouldEqualIds , "modifierCount small than ids count");
            var expectedCount = Modifiers.Count - modifierIds.Count;

            foreach (var modifierId in modifierIds)
            {
                Contract.RequireString(modifierId , "modifierId");
                RemoveModifier(modifierId);
            }

            Contract.Ensure(Modifiers.Count == expectedCount , "modifier count is not equal.");

            Calculate();
        }

        public void SetBaseAmount(int amount)
        {
            BaseAmount = amount;
            if (BaseAmount < 0) BaseAmount = 0;
            AddDomainEvent(new BaseAmountModified(GetId() , OwnerId , DataId));
            Calculate();
        }

    #endregion

    #region Private Methods

        private void AddModifier(string modifierId , ModifierType modifierType , int amount)
        {
            var modifier = new Modifier(modifierId , modifierType , amount);
            Modifiers.Add(modifier);
            AddDomainEvent(new ModifierAdded(GetId() , modifierId));
        }

        private void Calculate()
        {
            var flatModifiers         = Modifiers.FindAll(modifier => modifier.Type.Equals(ModifierType.Flat));
            var percentAddModifiers   = Modifiers.FindAll(modifier => modifier.Type.Equals(ModifierType.PercentAdd));
            var percentMultiModifiers = Modifiers.FindAll(modifier => modifier.Type.Equals(ModifierType.PercentMulti));
            // calculate Flat
            var sumFlat         = flatModifiers.Sum(modifier => modifier.Amount);
            var sumPercentAdd   = percentAddModifiers.Sum(modifier => modifier.Amount);
            var calculateResult = BaseAmount + sumFlat;
            // calculate Percent add
            if (percentAddModifiers.Count > 0)
            {
                var multiplyAdd    = 1f + sumPercentAdd / 100f;
                var multiplyResult = Math.Round(calculateResult * multiplyAdd , MidpointRounding.AwayFromZero);
                calculateResult = (int)multiplyResult;
            }

            // calculate Percent multi
            if (percentMultiModifiers.Count > 0)
            {
                var aggregatePercentMulti = percentMultiModifiers.Aggregate(1f , (x , y) =>
                {
                    var result = x * (1f + y.Amount / 100f);
                    return result;
                });
                if (aggregatePercentMulti != 0)
                    calculateResult = (int)Math.Round(calculateResult * aggregatePercentMulti);
            }

            if (calculateResult < 0) calculateResult = 0;
            SetCalculatedAmount(calculateResult);
        }

        private void RemoveModifier(string modifierId)
        {
            var modifier      = GetModifier(modifierId);
            var removeSucceed = Modifiers.Remove(modifier);
            if (removeSucceed) AddDomainEvent(new ModifierRemoved(GetId() , modifierId));
        }

        private void SetCalculatedAmount(int amount)
        {
            CalculatedAmount = amount;
            AddDomainEvent(new CalculatedAmountModified(GetId() , OwnerId));
        }

    #endregion
    }
}