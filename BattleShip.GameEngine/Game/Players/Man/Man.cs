using BattleShip.GameEngine.Arsenal.Gun;
using BattleShip.GameEngine.Arsenal.Gun.Destroyable;
using BattleShip.GameEngine.Game.Players;
using BattleShip.GameEngine.Location;
using System;
using System.Collections.Generic;

public class Man : BasePlayer
{
    /*
     * Review GY: не рекомендую використовувати такий підхід при роботі з подіями.
     * Події створюють для того, щоб надати користувачу змогу підписатись на них ззовні.
     */
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
        StartSetShipsFromReferriHandler(this);
    }

    public override void BeginSetProtect()
    {
        StartSetProtectsFromReferriHandler(this);
    }

    public override Position GetPositionForAttack(Gun gun, IList<IDestroyable> gunList)
    {
        return GetPositionForAttackFromReferriHandler(gun, gunList);
    }
}