using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect
{
    public class SetProtect : ISetibleProtect
    {
        public void SetProtects(Func<ProtectBase, bool> SetPtotectFunc, byte fieldSize)
        {
            Random rnd = new Random();
            Position pos;

            do
            {
                byte line = (byte)rnd.Next(fieldSize);
                byte column = (byte)rnd.Next(fieldSize);

                pos = new Position(line, column);
            } while (!SetPtotectFunc(new PVOProtect(0, pos, fieldSize)));
        }
    }
}