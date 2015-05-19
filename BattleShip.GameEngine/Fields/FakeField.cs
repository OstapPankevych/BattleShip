using BattleShip.GameEngine.GameEventArgs;


namespace BattleShip.GameEngine.Fields
{
    public class FakeField : BaseField
    {
        public FakeField(Field field)
            : base(field.Size)
        {
            // підписати на зміну клітинки при її знищенні на оригінальному полі
            for (byte i = 0; i < Size; i++)
            {
                for (byte j = 0; j < Size; j++)
                {
                    field[i, j].DeadHandler += OnDeadHandler;
                }
            }

            _field = field;
        }


        private Field _field;

        private void OnDeadHandler(GameEvenArgs e)
        {
            _cells[e.Location.Line][e.Location.Column] = _field[e.Location.Line, e.Location.Column];
        }

        
    }
}