using System;
using BattleShip.GameEngine.Arsenal.Flot.Corectible;
using BattleShip.GameEngine.Arsenal.Flot.Exceptions;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.GameEvent;


namespace BattleShip.GameEngine.Arsenal.Flot
{
    public abstract class ShipBase : IFullGameObject, IEnumerable<Position>
    {
        #region Constructors

        protected ShipBase(ICorrectible correctible, byte id, params Position[] positions)
        {
            
            if (correctible == null)
            {
                throw new ShipExceptions("the rule for ship type can't be NULL");
            }

            _correctible = correctible;

            _storeyCount = (byte)positions.Length;

            if (_correctible.IsTrueShipRegion(StoreyCount, positions))
            {
                _positions = new ObjectLocation(positions);
            }
            else
            {
                throw new ShipExceptions("Incorrect region for current type of ship");
            }
        }

        #endregion Constructors


        #region Private

        private readonly ObjectLocation _positions;

        private readonly byte _storeyCount;

        private readonly ICorrectible _correctible;

        private bool _wasDead = false;

        #endregion Private


        #region Events

        public event Action<GameEvenArgs> DeadHandler;

        public event Action<GameEvenArgs> HitHandler;

        #endregion Events


        #region Properties

        public byte StoreyCount
        {
            get { return _storeyCount; }
        }

        public bool IsLife
        {
            get { return _positions.IsLife; }
        }

        #endregion Properties


        #region IEnumerable<Position>

        // повертає позиції кораблика
        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable<Position>


        public void KillMe(GameEvenArgs e)
        {
            if (!_wasDead)
            {
                if (!IsLife)
                {
                    if (DeadHandler != null)
                    {
                        DeadHandler(e);
                    }
                }
            }
        }

        public void DestroyMe(GameEvenArgs e)
        {
            _positions.ChangeLifeToDead(e.Location);

            if (HitHandler != null)
            {
                HitHandler(e);
            }

            KillMe(e);
        }
     
    }


}