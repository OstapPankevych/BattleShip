using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using System;

namespace BattleShip.GameEngine.Arsenal.Protection
{
    public class PVOProtect : ProtectBase
    {
        public PVOProtect(byte id, Position position, byte size)
            : base(id, position)
        {
            protectionList.Add(typeof(PlaneDestroy));

            // встановлення координат позицій, які будуть захищені
            CurrentProtectedPositions = new Position[size];
            for (byte i = 0; i < size; i++)
                CurrentProtectedPositions[i] = new Position(position.Line, i);
        }

        public override Position[] GetProtectedPositions()
        {
            var pos = new Position[CurrentProtectedPositions.Length];
            for (byte i = 0; i < pos.Length; i++)
                pos[i] = new Position(CurrentProtectedPositions[i].Line, CurrentProtectedPositions[i].Column);
            return pos;
        }

        public override event Action<GameObject.GameObject, ProtectEventArgs> ProtectedHandler = delegate { };

        public override void OnProtectedHandler(GameObject.GameObject g, ProtectEventArgs e)
        {
            ProtectedHandler(g, e);
        }

        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEvenArgs e)
        {
            base.OnHitMeHandler(gameObject, e);
            OnProtectedHandler(gameObject, new ProtectEventArgs(GetType()));
        }
    }
}