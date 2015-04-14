using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.GameObject
{
    struct ObjectLocation : IEnumerable<Position>
    {
        PositionAndStatus[] _positionAndStatus;

        public ObjectLocation(params Position[] positions)
        {
            if (positions == null)
                throw new NullReferenceException();

            _positionAndStatus = new PositionAndStatus[positions.Length];

            for (int i = 0; i < _positionAndStatus.Length; i++)
                _positionAndStatus[i] = new PositionAndStatus(positions[i]);
        }

        public bool GetLifeStatus(Position position)
        {
            foreach (var x in _positionAndStatus)
                if (x.Location == position)
                    return x.IsLife;
            throw new ArgumentOutOfRangeException();
        }

        public byte GetCountLifeParts()
        {
            byte count = 0;
            foreach (var x in _positionAndStatus)
                if (x.IsLife)
                    count++;
            return count;
        }

        public int GetCountParts()
        {
            return _positionAndStatus.Length;
        }

        public Position[] GetPositionsLifeParts()
        {
            Position[] arrLife = new Position[GetCountLifeParts()];
            int pos = 0;
            for (int i = 0; i < _positionAndStatus.Length; i++)
                if (_positionAndStatus[i].IsLife)
                    arrLife[pos++] = _positionAndStatus[i].Location;

            return arrLife;
        }

        public Position[] GetPositionDeadParts()
        {
            Position[] arrLife = new Position[_positionAndStatus.Length - GetCountLifeParts()];
            int pos = 0;
            for (int i = 0; i < _positionAndStatus.Length; i++)
                if (!_positionAndStatus[i].IsLife)
                    arrLife[pos++] = _positionAndStatus[i].Location;

            return arrLife;
        }

        public void ChangeLifeToDead(Position position)
        {
            foreach (var x in _positionAndStatus)
            {
                if (x.Location == position)
                {
                    x.ChangeLifeToDead();
                    return;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        public bool IsLife
        {
            get
            {
                foreach (var x in _positionAndStatus)
                    if (x.IsLife)
                        return true;
                return false;
            }
        }

        public IEnumerator<Position> GetEnumerator()
        {
            foreach (var x in _positionAndStatus)
                yield return x.Location;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Position>)GetEnumerator();
        }
    }
}