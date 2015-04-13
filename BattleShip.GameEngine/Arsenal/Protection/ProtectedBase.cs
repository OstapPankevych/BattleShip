using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;

namespace BattleShip.GameEngine.Arsenal.Protection
{
    abstract class ProtectedBase : GameObject.GameObject
    {
        protected ObjectLocation _positions;

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
        public abstract Position[] GetProtectedPositions(byte size);

        protected List<Type> protectionList = new List<Type>();

        // івент вмирання об'єкта
        public event Action<byte> DeadHandler;

        public override void OnHitMeHandler(Position position)
        {
            if (_positions.IsLife)
            {
                // знищити
                OnDeadHandler();

                // запустити івент
                if (DeadHandler != null)
                    DeadHandler(ID);
            }
        }

        public override void OnDeadHandler()
        {
            // вбити цілком захист
            Position[] positions = _positions.GetPositionsLifeParts();
            foreach (var x in positions)
                _positions.ChangeLifeToDead(x);
        }
    }
}