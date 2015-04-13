using System;

using BattleShip.GameEngine.GameObject;
using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Field.Cell.StatusCell
{
    // клас клітинки навколо кораблика
    class AroundShip : StatusCell
    {
        public AroundShip(Position position)
            : base(position)
        { }
    }
}