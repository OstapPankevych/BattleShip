using BattleShip.GameEngine.Arsenal.Flot;
using System;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetRectangleBase
{
    public interface ISetibleShip
    {
        // передаю делегат на функцію, яка розставляє кораблики на полі (зроблено з міркувань безпеки)
        void SetShips(Func<ShipBase, bool> SetShipsFunc, byte fieldSize);
    }
}