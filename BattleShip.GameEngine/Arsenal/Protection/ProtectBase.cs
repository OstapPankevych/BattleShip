using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Arsenal.Protection
{
    public abstract class ProtectBase : GameObject, IEnumerable<Position>
    {
        // розташування самого обєкта
        private ObjectLocation _positions;

        #region Protected

        // позиції, які захищає
        protected Position[] CurrentProtectedPositions;

        // ліст типів, від яких захищає
        protected List<Type> protectionList = new List<Type>();

        #endregion Protected

        #region Public

        public ProtectBase(byte id, Position position)
            : base(id)
        {
            this._positions = new ObjectLocation(position);
        }

        public Type[] GetProtectedType()
        {
            var types = new Type[protectionList.Count];

            for (var i = 0; i < types.Length; i++)
            {
                types[i] = protectionList[i];
            }

            return types;
        }

        #region Properties

        public Position[] Positions
        {
            get { return this._positions.GetPositionsLifeParts(); }
        }

        public override bool IsLife
        {
            get { return this._positions.IsLife; }
        }

        #endregion Properties

        #region IEnumerable<Position>

        public IEnumerator<Position> GetEnumerator()
        {
            return this._positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerable<Position>

        #region AbstractMethods

        // позиції, які покриває даний захист
        public abstract Position[] GetProtectedPositions();

        // івент зняття захисту
        public abstract event Action<GameObject, ProtectEventArgs> ProtectedHandler;

        public abstract void OnProtectedHandler(GameObject g, ProtectEventArgs e);

        #endregion AbstractMethods

        #region Override

        public override event Action<GameObject, GameEvenArgs> DeadHandler = delegate { };

        public override event Action<GameObject, GameEvenArgs> HitMeHandler = delegate { };

        public override void OnHitMeHandler(GameObject gameObject, GameEvenArgs e)
        {
            if (this._positions.IsLife)
            {
                // вбити цілком захист
                var positions = this._positions.GetPositionsLifeParts();
                foreach (var x in positions)
                {
                    this._positions.ChangeLifeToDead(x);
                }

                // запустити івент
                DeadHandler(this, e);
            }
        }

        #endregion Override

        #endregion Public
    }
}