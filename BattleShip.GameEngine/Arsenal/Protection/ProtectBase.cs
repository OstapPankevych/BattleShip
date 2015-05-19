using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;
using System.Collections;
using System.Collections.Generic;


namespace BattleShip.GameEngine.Arsenal.Protection
{
    public abstract class ProtectBase : IGameObject, IIdentificator, IEnumerable<Position>
    {
        protected ProtectBase(byte id, Position position)
        {
            _positions = new ObjectLocation(position);
            IsLife = true;
            ID = id;
        }


        #region Private

        // розташування самого обєкта
        private readonly ObjectLocation _positions;

        #endregion Private


        #region Protected

        // позиції, які захищає
        protected Position[] currentProtectedPositions;

        // ліст типів, від яких захищає
        protected List<Type> protectionList = new List<Type>();

        #endregion Protected


        #region Properties

        public Position[] Positions
        {
            get { return _positions.GetPositionsLifeParts(); }
        }

        public bool IsLife { get; private set; }

        public byte ID { get; private set; }

        #endregion Properties


        #region Public methods

        public Type[] GetProtectedType()
        {
            var types = new Type[protectionList.Count];

            for (var i = 0; i < types.Length; i++)
            {
                types[i] = protectionList[i];
            }

            return types;
        }

        #endregion Public methods


        #region AbstractMethods

        // позиції, які покриває даний захист
        public abstract Position[] GetProtectedPositions();

        public abstract void KillMe(GameEvenArgs e);

        #endregion AbstractMethods


        #region Events

        public abstract event Action<GameEvenArgs> DeadHandler;

        public abstract event Action<ProtectEventArgs> ProtectedHandler; 

        #endregion Events


        #region IEnumerable<Position>

        public IEnumerator<Position> GetEnumerator()
        {
            return _positions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion IEnumerable<Position>
    }
}