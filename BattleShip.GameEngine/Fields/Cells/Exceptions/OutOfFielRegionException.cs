using System;
//простір імен не відповідає розміщенню файлу
namespace BattleShip.GameEngine.Fields.Exceptions
{
    //класс можна зробити не наслідуваним
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