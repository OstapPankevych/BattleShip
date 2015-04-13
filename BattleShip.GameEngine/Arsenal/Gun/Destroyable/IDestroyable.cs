using System;


using BattleShip.GameEngine.Location;
using BattleShip.GameEngine.GameObject;


namespace BattleShip.GameEngine.Arsenal.Gun.Destroyable
{

    interface IDestroyable
    {
        Position[] Destroy(Position point, byte size);
    }

}