using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.ObjectOfGame;
using System;

namespace BattleShip.GameEngine.Arsenal.Protection
{
    public class PVOProtect : ProtectBase
    {
        public PVOProtect(byte id, Position position, byte size)
            : base(id, position)
        {
            base.protectionList.Add(typeof(PlaneDestroy));

            // встановлення координат позицій, які будуть захищені
            base.CurrentProtectedPositions = new Position[size];
            for (byte i = 0; i < size; i++)
            {
                base.CurrentProtectedPositions[i] = new Position(position.Line, i);
            }
        }

        #region Override

        public override Position[] GetProtectedPositions()
        {
            var pos = new Position[base.CurrentProtectedPositions.Length];
            for (byte i = 0; i < pos.Length; i++)
            {
                pos[i] = new Position(base.CurrentProtectedPositions[i].Line, base.CurrentProtectedPositions[i].Column);
            }

            return pos;
        }

        public override event Action<GameObject, ProtectEventArgs> ProtectedHandler = delegate { };

        public override void OnProtectedHandler(GameObject g, ProtectEventArgs e)
        {
<<<<<<< HEAD
            //if (ProtectedHandler != null) ProtectedHandler(g, e); перевірка null не завадить 
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
            ProtectedHandler(g, e);
        }

        public override void OnHitMeHandler(GameObject gameObject, GameEvenArgs e)
        {
            base.OnHitMeHandler(gameObject, e);
            OnProtectedHandler(gameObject, new ProtectEventArgs(GetType()));
        }

        #endregion Override
    }
}