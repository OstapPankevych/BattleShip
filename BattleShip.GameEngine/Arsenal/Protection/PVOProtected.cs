using System;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Location.RulesOfSetPositions;

namespace BattleShip.GameEngine.Arsenal.Protection
{

    class PVOProtected : ProtectedBase
    {
        public PVOProtected(byte id, Position position)
            : base(id, position)
        {
            protectionList.Add(typeof(Gun.Destroyable.PlaneDestroy));
        }

        public override Position[] GetProtectedPositions(byte size)
        {
            Position[] position = _positions.GetPositionsLifeParts();
            IRuleSetPosition rule = new RectangleRule(new Position(position[0].Line, 0), new Position(position[0].Line, (byte)(size - 1)));
            return rule.GetRegionForCurrentRule();
        }
    }
}