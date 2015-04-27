using BattleShip.GameEngine.GameEventArgs;
using BattleShip.GameEngine.ObjectOfGame;

namespace BattleShip.GameEngine.Fields
{
    public class FakeField : BaseField
    {
        public FakeField(Fields.Field field)
            : base(field.Size)
        {
            // підписати на зміну клітинки при її знищенні на оригінальному полі
            for (int i = 0; i < field.Size * field.Size; i++)
            {
                field[i].DeadHandler += OnShowRealStatus;
            }
        }

        private void OnShowRealStatus(GameObject g, GameEvenArgs e)
        {
            this[e.Location].AddGameObject(g, false);

            // "вбити" - WasAttack зробити true
            this[e.Location].OnDeadHandler();
        }
    }
}