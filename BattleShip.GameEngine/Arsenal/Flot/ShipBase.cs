using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.Exceptions;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;
using System.Collections;
using System.Collections.Generic;

<<<<<<< HEAD
//класи, які наслідуються від ShipBase можна було о'бєднати в один *.cs файл
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
namespace BattleShip.GameEngine.Arsenal.Flot
{
    public abstract class ShipBase : GameObject, IEnumerable<Position>
    {
        #region Private

        private readonly ObjectLocation _positions;

        private readonly byte _storeyCount;

        private bool _wasDead;

        private readonly ICorrectible _correctible;

        private void OnDeadHandler()
        {
            this._wasDead = true;
        }

        #endregion Private

        #region Public

        #region Constructors

        public ShipBase(ICorrectible correctible, byte id, params Position[] positions)
            : base(id)
        {
            if (correctible == null)
            {
                throw new ShipExceptions("the rule for ship type can't be NULL");
            }

            this._correctible = correctible;

            this._storeyCount = (byte)positions.Length;

            if (this._correctible.IsTrueShipRegion(StoreyCount, positions))
            {
                this._positions = new ObjectLocation(positions);
            }
            else
            {
                throw new ShipExceptions("Incorrect region for current type of ship");
            }
        }

        #endregion Constructors

        #region Properties

        public byte StoreyCount
        {
            get { return this._storeyCount; }
        }

        public override bool IsLife
        {
            get { return this._positions.IsLife; }
        }

        #endregion Properties

        #region IEnumerable<Position>

        // повертає позиції кораблика
        public IEnumerator<Position> GetEnumerator()
        {
            return this._positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable<Position>

        #region GameObject realization

        // івент влучання в об'єкт
        public override event Action<GameObject, GameEvenArgs> HitMeHandler = delegate { };

        // івент вмирання об'єкта
        public override event Action<GameObject, GameEvenArgs> DeadHandler = delegate { };

        public override void OnHitMeHandler(GameObject sender, GameEvenArgs e)
        {
            // вбити клітинку, в яку потрапили
            this._positions.ChangeLifeToDead(e.Location);

            // сказати тому, хто підписаний на цей корабель, що в нього попали
<<<<<<< HEAD
            //було б непогано добавити перевірку на null if (HitMeHandler != null) HitMeHandler(this, e);
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
            HitMeHandler(this, e);

            // якщо його вбили цілком - сказати підписникам, що він вбитий
            if (!this._wasDead)
            {
                if (!IsLife)
                {
                    OnDeadHandler();
<<<<<<< HEAD
                    //було б непогано добавити перевірку на null if (DeadHandler != null) DeadHandler(this, e);
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
                    DeadHandler(this, e);
                }
            }
        }

        #endregion GameObject realization

        #endregion Public
    }
}