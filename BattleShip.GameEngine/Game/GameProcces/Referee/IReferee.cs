using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Field;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.Game.GameProcces.ClassicGameReferee
{
    internal interface IReferee
    {
        // почати гру (або зробити один хід - в залежності від реалізації)
        void StartGame(Gun gun, Position position);

        // отримати повноцінне поле ігорока1
        Field.Field GetFieldOfPlayer1();

        // отримати фейкове поле ігрока2
        FakeField GetFakeFieldOfPlayer2();
    }
}