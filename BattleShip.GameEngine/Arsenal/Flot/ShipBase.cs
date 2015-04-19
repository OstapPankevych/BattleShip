using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.Exceptions;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BattleShip.GameEngine.Arsenal.Flot
{
    public abstract class ShipBase : GameObject.GameObject, IEnumerable<Position>
    {
        protected ObjectLocation _positions;
        protected byte _storeyCount;
        protected bool _wasDead;

        protected ICorrectible Correctible = null;

        public ShipBase(ICorrectible correctible, byte id, params Position[] positions)
            : base(id)
        {
            if (correctible == null)
                throw new ShipExceptions("the rule for ship type can't be NULL");

            Correctible = correctible;

            _storeyCount = (byte)positions.Length;

            if (Correctible.IsTrueShipRegion(StoreyCount, positions))
            {
                _positions = new ObjectLocation(positions);
            }
            else
            {
                throw new ShipExceptions("Incorrect region for current type of ship");
            }
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