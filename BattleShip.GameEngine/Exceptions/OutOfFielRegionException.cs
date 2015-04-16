using System;


namespace BattleShip.GameEngine.Exceptions
{
    public class OutOfFielRegionException : ApplicationException
    {
        string _msg = "out of field region";
        public override string Message
        {
            get
            {
                return _msg;
            }
        }

        public OutOfFielRegionException(string sourceName)
        {
            Source = sourceName;
        }
    }
}
