<<<<<<< HEAD
﻿using BattleShip.GameEngine.Arsenal.Flot.Correctible;
=======
﻿using BattleShip.GameEngine.Arsenal.Flot.Corectible;
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Arsenal.Flot.RectangleShips
{
    public class OneStoreyRectangleShip : ShipBase
    {
        public OneStoreyRectangleShip(byte id, Position position)
            : base(new Ractangle(), id, position)
        { }
    }
}