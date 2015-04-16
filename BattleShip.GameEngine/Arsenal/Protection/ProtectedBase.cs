using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.GameEventArgs;


namespace BattleShip.GameEngine.Arsenal.Protection
{
    public abstract class ProtectedBase : GameObject.GameObject, IEnumerable<Position>
    {
        // розташування самого обєкта
        protected ObjectLocation _positions;

        public Position[] Positions
        {
            get
            {
                return _positions.GetPositionsLifeParts();
            }
        }

        // позиції, які захищає
        protected Position[] currentProtectedPositions;

        public override bool IsLife
        {
            get
            {
                return _positions.IsLife;
            }
        }

        public ProtectedBase(byte id, Position position)
            : base(id)
        {
            _positions = new ObjectLocation(position);
        }

        // позиції, які покриває даний захист
        public abstract Position[] GetProtectedPositions();

        // ліст типів, від яких захищає
        protected List<Type> protectionList = new List<Type>();

        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEvenArgs e)
        {
            if (_positions.IsLife)
            {
                // вбити цілком захист
                Position[] positions = _positions.GetPositionsLifeParts();
                foreach (var x in positions)
                    _positions.ChangeLifeToDead(x);

                // запустити івент
                DeadHandler(this, e);
            }
        }

        public override event Action<GameObject.GameObject, GameEvenArgs> DeadHandler = delegate { };

        public override event Action<GameObject.GameObject, GameEvenArgs> HitMeHandler = delegate { };

        // івент зняття захисту
        public abstract event Action<GameObject.GameObject, GameEventArgs.ProtectEventArgs> ProtectedHandler;

        public abstract void OnProtectedHandler(GameObject.GameObject g, GameEventArgs.ProtectEventArgs e);

        // повертає позиції захисту
        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Position>)GetEnumerator();
        }
    }
}