using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location.RulesOfSetPositions;


namespace BattleShip.GameEngine.Arsenal.Flot
{
    abstract class ShipRectangleBase : GameObject.GameObject, IEnumerable<Position>
    {
        protected byte _storeyCount;
        public byte StoreyCount
        {
            get
            {
                return _storeyCount;
            }
        }

        protected bool _wasDead = false;
        public override bool IsLife
        {
            get
            {
                return _positions.IsLife;
            }
        }

        protected ObjectLocation _positions;

        public ShipRectangleBase(byte id, params Position[] positions)
            : base(id)
        {
            _positions = new ObjectLocation(positions);
        }

        // івент влучання в об'єкт
        public event Action<Position, byte> HitMeHandler;

        // івент вмирання об'єкта
        public event Action<byte> DeadHandler;

        public override void OnHitMeHandler(Position position)
        {
            // вбити клітинку, в яку потрапили
            _positions.ChangeLifeToDead(position);

            // сказати тому, хто підписаний на цей корабель, що в нього попали
            if (HitMeHandler != null)
                HitMeHandler(position, ID);

            // якщо його вбили цілком - сказати підписникам, що він вбитий
            if (!_wasDead)
                if (!IsLife)
                {
                    OnDeadHandler();
                    if (DeadHandler != null)
                        DeadHandler(ID);
                }
        }

        public override void OnDeadHandler()
        {
            _wasDead = true;
        }
    
        // повертає позиції кораблика
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