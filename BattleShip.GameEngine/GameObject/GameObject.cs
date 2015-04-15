using System;
using System.Collections.Generic;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameEventArgs;


namespace BattleShip.GameEngine.GameObject
{
    abstract class GameObject 
    {
        // чи живий
        public abstract bool IsLife { get; }

        private byte _id;
        public byte ID
        {
            get
            {
                return _id;
            }
        }

        public GameObject(byte id)
        {
            this._id = id;
        }

        // івент вмирання об'єкта
        public abstract event Action<GameObject, GameEvenArgs> DeadHandler;
        // івент влучання в об'єкт
        public abstract event Action<GameObject, GameEvenArgs> HitMeHandler;

        public virtual void OnHitMeHandler(GameObject g, GameEvenArgs e) { }

        void OnDeadHandler() { }
    }
}