using System;

namespace BattleShip.GameEngine.Fields.Exceptions
{
    //клас можна зробити sealed
    public class OutOfFieldRegionException : ApplicationException
    {
        //_msg доцільно зробити константою
        private readonly string _msg = "out of field region";

        public OutOfFieldRegionException(string sourceName)
        {
            //віртульний член викликається в конструкторі
            Source = sourceName;
        }

        public override string Message
        {
            get { return _msg; }
        }
    }
}