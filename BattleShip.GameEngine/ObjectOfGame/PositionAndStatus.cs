using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.ObjectOfGame
{
    public struct PositionAndStatus
    {
        private readonly Position _position;
        private bool _life;

        public PositionAndStatus(Position position)
        {
            this._life = true;
            this._position = position;
        }

        public bool IsLife
        {
            get { return this._life; }
        }

        public Position Location
        {
            get { return _position; }
        }

        public void ChangeLifeToDead()
        {
            this._life = false;
        }
    }
}