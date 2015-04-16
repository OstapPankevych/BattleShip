using System;
using System.Collections.Generic;

using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.Location.RulesOfSetPositions;

namespace BattleShip.GameEngine.Arsenal.Protection
{

    public class PVOProtected : ProtectedBase, IEnumerable<Type>
    {
        public PVOProtected(byte id, Position position, byte size)
            : base(id, position)
        {
            protectionList.Add(typeof(Gun.Destroyable.PlaneDestroy));

            // встановлення координат позицій, які будуть захищені
            currentProtectedPositions = new Position[size];
            for (byte i = 0; i < size; i++)
                currentProtectedPositions[i] = new Position(position.Line, i);
        }

        public override Position[] GetProtectedPositions()
        {
            Position[] pos = new Position[currentProtectedPositions.Length];
            for (byte i = 0; i < pos.Length; i++)
                pos[i] = new Position(currentProtectedPositions[i].Line, currentProtectedPositions[i].Column);
            return pos;
        }

        public override event Action<GameObject.GameObject, GameEventArgs.ProtectEventArgs> ProtectedHandler = delegate { };


        public override void OnProtectedHandler(GameObject.GameObject g, GameEventArgs.ProtectEventArgs e)
        {
            ProtectedHandler(g, e);
        }


        public override void OnHitMeHandler(GameObject.GameObject gameObject, GameEventArgs.GameEvenArgs e)
        {
            OnProtectedHandler(gameObject, new GameEventArgs.ProtectEventArgs(this.GetType()));
            base.OnHitMeHandler(gameObject, e);
        }

        public new IEnumerator<Type> GetEnumerator()
        {
            return protectionList.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return (IEnumerator<Type>)GetEnumerator();
        }
    }
}