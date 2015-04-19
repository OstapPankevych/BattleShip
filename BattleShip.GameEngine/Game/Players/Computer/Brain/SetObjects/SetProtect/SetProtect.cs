using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect
{
    internal class SetProtect : ISetibleProtect
    {
        public void SetProtects(GameMode.GameMode mode)
        {
            Random rnd = new Random();
            Position pos;

            do
            {
                byte Line = (byte)rnd.Next(mode.CurrentField.Size);
                byte Column = (byte)rnd.Next(mode.CurrentField.Size);

                pos = new Position(Line, Column);
            } while (!mode.AddProtect(new PVOProtect(0, pos, mode.CurrentField.Size)));
        }
    }
}