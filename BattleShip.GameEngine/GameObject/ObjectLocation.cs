using System;
using System.Collections;
using System.Collections.Generic;
using BattleShip.GameEngine.Location;

namespace BattleShip.GameEngine.GameObject
{
    public struct ObjectLocation : IEnumerable<Position>
    {
        private readonly PositionAndStatus[] _positionAndStatus;

        public ObjectLocation(params Position[] positions)
        {
            if (positions == null)
                throw new NullReferenceException();

            _positionAndStatus = new PositionAndStatus[positions.Length];

            for (var i = 0; i < _positionAndStatus.Length; i++)
                _positionAndStatus[i] = new PositionAndStatus(positions[i]);
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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            for (var i = 0; i < _positionAndStatus.Length; i++)
                if (_positionAndStatus[i].IsLife)
                    count++;
            return count;
        }

        public int GetCountParts()
        {
            return _positionAndStatus.Length;
        }

        public Position[] GetPositionsLifeParts()
        {
            var arrLife = new Position[GetCountLifeParts()];
            var pos = 0;
            for (var i = 0; i < _positionAndStatus.Length; i++)
                if (_positionAndStatus[i].IsLife)
                    arrLife[pos++] = _positionAndStatus[i].Location;

            return arrLife;
        }

        public Position[] GetPositionDeadParts()
        {
            var arrLife = new Position[_positionAndStatus.Length - GetCountLifeParts()];
            var pos = 0;
            for (var i = 0; i < _positionAndStatus.Length; i++)
                if (!_positionAndStatus[i].IsLife)
                    arrLife[pos++] = _positionAndStatus[i].Location;

            return arrLife;
        }

        public bool ChangeLifeToDead(Position position)
        {
            for (var i = 0; i < _positionAndStatus.Length; i++)
            {
                if ((_positionAndStatus[i]).Location == position)
                {
                    _positionAndStatus[i].ChangeLifeToDead();
                    return true;
                }
            }
            return true;
        }
    }
}