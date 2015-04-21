using BattleShip.GameEngine.Arsenal.Protection;
using BattleShip.GameEngine.Location;
using System;
using BattleShip.GameEngine.Arsenal.Flot;

namespace BattleShip.GameEngine.Game.Players.Computer.Brain.SetObjects.SetProtect
{
    public class SetProtect : ISetibleProtect
    {
        public void SetProtects(GameMode.GameMode myMode)
        {
            Random rnd = new Random();
            Position pos;

            do
            {
                byte Line = (byte)rnd.Next(myMode.CurrentField.Size);
                byte Column = (byte)rnd.Next(myMode.CurrentField.Size);

                pos = new Position(Line, Column);
            } while (!myMode.AddProtect(new PVOProtect(0, pos, myMode.CurrentField.Size)));
        }
        
    }
}