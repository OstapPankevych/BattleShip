using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Protection
{
    public abstract class ProtectedBase : GameObject.GameObject, IEnumerable<Position>
    {
        // розташування самого обєкта
        protected ObjectLocation _positions;
        // позиції, які захищає
        protected Position[] CurrentProtectedPositions;
        // ліст типів, від яких захищає
        protected List<Type> protectionList = new List<Type>();

        public ProtectedBase(byte id, Position position)
            : base(id)
        {
            _positions = new ObjectLocation(position);
        }

        public Position[] Positions
        {
            get { return _positions.GetPositionsLifeParts(); }
        }

        public override bool IsLife
        {
            get { return _positions.IsLife; }
        }

        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // позиції, які покриває даний захист
        public abstract Position[] GetProtectedPositions();

        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEvenArgs e)
        {
            if (_positions.IsLife)
            {
                // вбити цілком захист
                var positions = _positions.GetPositionsLifeParts();
                foreach (var x in positions)
                    _positions.ChangeLifeToDead(x);

                // запустити івент
                DeadHandler(this, e);
            }
        }

        public override event Action<GameObject.GameObject, GameEvenArgs> DeadHandler = delegate { };
        public override event Action<GameObject.GameObject, GameEvenArgs> HitMeHandler = delegate { };
        // івент зняття захисту
        public abstract event Action<GameObject.GameObject, ProtectEventArgs> ProtectedHandler;
        public abstract void OnProtectedHandler(GameObject.GameObject g, ProtectEventArgs e);

        public virtual Type[] GetProtectedType()
        {
            var types = new Type[protectionList.Count];

            for (var i = 0; i < types.Length; i++)
                types[i] = protectionList[i];

            return types;
        }
    }
}