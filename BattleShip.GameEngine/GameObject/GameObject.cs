using System;
using System.Collections.Generic;


using BattleShip.GameEngine.Location;



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

        // метод опрацювання влучання 
        public abstract void OnHitMeHandler(Position position);

        // метод опрацювання знищення об'єкта
        public abstract void OnDeadHandler();
    }
}