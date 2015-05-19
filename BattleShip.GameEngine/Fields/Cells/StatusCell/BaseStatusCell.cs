using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.ObjectOfGame;
using BattleShip.GameEngine.Location;


namespace BattleShip.GameEngine.Fields.Cells.StatusCell
{
    public abstract class BaseStatusCell : IGameObject
    {
        protected BaseStatusCell(Position position)
        {
            _position = position;
        }

        private readonly Position _position;

        public new bool IsLife { get; private set; }

        public void KillMe(GameEvenArgs e)
        {
            if (e.Location == _position)
            {
                IsLife = false;
            }
        }

    }
}
