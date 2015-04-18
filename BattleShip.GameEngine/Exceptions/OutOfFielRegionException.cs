using System;

namespace BattleShip.GameEngine.Exceptions
{
    public class OutOfFielRegionException : ApplicationException
    {
        private readonly string _msg = "out of field region";

        public OutOfFielRegionException(string sourceName)
        {
            Source = sourceName;
        }

        public override string Message
        {
            get { return _msg; }
        }
    }
}