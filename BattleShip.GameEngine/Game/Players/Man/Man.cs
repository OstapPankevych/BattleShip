using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Game.Players.Man
{
    public class Man : BasePlayer
    {
        private Position _currentPositionForAttack;

        public Position CurrentPositionForAttack
        {
            get
            {
                return _currentPositionForAttack;
            }

            set
            {
                if (Field.Field.IsFielRegion(value.Line, value.Column, gameMode.CurrentField.Size))
                    _currentPositionForAttack = value;
            }
        }

        public Man(String name, GameMode.GameMode gameMode)
            : base(name, gameMode)
        { }
    }
}