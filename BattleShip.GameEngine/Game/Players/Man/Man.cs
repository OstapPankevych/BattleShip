using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Game.Players;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

public class Man : BasePlayer
{
    public Man(string name,
        byte fieldSize,
        Action<Man> StartSetShipsFromReferriOnHandler,
        Action<Man> StartSetProtectsFromReferriOnHandler,
        Func<Gun, IList<IDestroyable>, Position> GetPositionForAttackFromReferriOnHandler)
        : base(name, null, null, fieldSize)
    {
        this.StartSetProtectsFromReferriHandler += StartSetProtectsFromReferriOnHandler;
        this.StartSetShipsFromReferriHandler += StartSetShipsFromReferriOnHandler;
        this.GetPositionForAttackFromReferriHandler += GetPositionForAttackFromReferriOnHandler;
    }

    public event Action<Man> StartSetShipsFromReferriHandler;

    public event Action<Man> StartSetProtectsFromReferriHandler;

    public event Func<Gun, IList<IDestroyable>, Position> GetPositionForAttackFromReferriHandler;

    public override void BeginSetShips()
    {
<<<<<<< HEAD
        //потрбна перевірка на null
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
        StartSetShipsFromReferriHandler(this);
    }

    public override void BeginSetProtect()
    {
<<<<<<< HEAD
        //потрбна перевірка на null
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
        StartSetProtectsFromReferriHandler(this);
    }

    public override Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList)
    {
<<<<<<< HEAD
        //потрбна перевірка на null
=======
>>>>>>> adcb4d49f57b1a9c51a12f9f9099df7db01d1a0d
        return GetPositionForAttackFromReferriHandler(gun, gunList);
    }
}