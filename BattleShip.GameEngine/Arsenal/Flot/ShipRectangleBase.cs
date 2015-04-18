using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot
{
    public abstract class ShipRectangleBase : GameObject.GameObject, IEnumerable<Position>
    {
        protected ObjectLocation _positions;
        protected byte _storeyCount;
        protected bool _wasDead;

        public ShipRectangleBase(byte id, params Position[] positions)
            : base(id)
        {
            _positions = new ObjectLocation(positions);
        }

        public byte StoreyCount
        {
            get { return _storeyCount; }
        }

        public override bool IsLife
        {
            get { return _positions.IsLife; }
        }

        // повертає позиції кораблика
        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // івент влучання в об'єкт
        public override event Action<GameObject.GameObject, GameEvenArgs> HitMeHandler = delegate { };
        // івент вмирання об'єкта
        public override event Action<GameObject.GameObject, GameEvenArgs> DeadHandler = delegate { };

        public override void OnHitMeHandler(GameObject.GameObject sender, GameEvenArgs e)
        {
            // вбити клітинку, в яку потрапили
            _positions.ChangeLifeToDead(e.Location);

            // сказати тому, хто підписаний на цей корабель, що в нього попали
            HitMeHandler(this, e);

            // якщо його вбили цілком - сказати підписникам, що він вбитий
            if (!_wasDead)
                if (!IsLife)
                {
                    OnDeadHandler();
                    DeadHandler(this, e);
                }
        }

        private void OnDeadHandler()
        {
            _wasDead = true;
        }
    }
}