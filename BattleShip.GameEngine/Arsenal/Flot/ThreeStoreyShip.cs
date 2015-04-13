using System;


using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Arsenal.Flot
{
    class ThreeStoreyShip : ShipRectangleBase
    {
        public ThreeStoreyShip(byte id, Position begin, Position average, Position end)
            : base(id, begin, average, end)
        {
            _storeyCount = 3;
        }
    }
}