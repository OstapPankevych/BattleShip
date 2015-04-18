using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.GameObject
{
    public struct PositionAndStatus
    {
        private readonly Position _position;
        private bool _life;

        public PositionAndStatus(Position position)
        {
            _life = true;
            _position = position;
        }

        public bool IsLife
        {
            get { return _life; }
        }

        public Position Location
        {
            get { return _position; }
        }

        public void ChangeLifeToDead()
        {
            _life = false;
        }
    }
}