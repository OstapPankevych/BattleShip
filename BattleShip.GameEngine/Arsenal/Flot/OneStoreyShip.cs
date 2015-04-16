using System;


using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Arsenal.Flot
{
    public class OneStoreyShip : ShipRectangleBase
    {
        public OneStoreyShip(byte id, Position position)
            : base(id, position)
        {
            _storeyCount = 1;
        }
    }
}