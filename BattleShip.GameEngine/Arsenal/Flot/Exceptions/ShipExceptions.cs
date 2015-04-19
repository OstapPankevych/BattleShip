using System;

namespace BattleShip.GameEngine.Arsenal.Flot.Exceptions
{
    public class ShipExceptions : ApplicationException
    {
        public ShipExceptions(string msg)
            : base(msg)
        { }
    }
}