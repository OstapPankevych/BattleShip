using System;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Location.RulesOfSetPositions;

namespace BattleShip.GameEngine.Arsenal.Protection
{

    public class PVOProtected : ProtectedBase
    {
        public PVOProtected(byte id, Position position, byte size)
            : base(id, position)
        {
            protectionList.Add(typeof(Gun.Destroyable.PlaneDestroy));

            // встановлення координат позицій, які будуть захищені
            currentProtectedPositions = new Position[size];
            for (byte i = 0; i < size; i++)
                currentProtectedPositions[i] = new Position(position.Line, i);
        }

        public override Position[] GetProtectedPositions()
        {
            Position[] pos = new Position[currentProtectedPositions.Length];
            for (byte i = 0; i < pos.Length; i++)
                pos[i] = new Position(currentProtectedPositions[i].Line, currentProtectedPositions[i].Column);
            return pos;
        }
    }
}