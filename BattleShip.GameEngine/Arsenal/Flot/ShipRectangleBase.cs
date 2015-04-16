using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location.RulesOfSetPositions;
using BattleShip.GameEngine.GameEventArgs;



namespace BattleShip.GameEngine.Arsenal.Flot
{
    public abstract class ShipRectangleBase : GameObject.GameObject, IEnumerable<Position>
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

        void OnDeadHandler()
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