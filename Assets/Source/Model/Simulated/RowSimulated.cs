using Zenject;

namespace ShootEmUp.Model
{
    public class RowSimulated : Simulation<Row>
    {
        private readonly RowMovement _rowMovement;

        [Inject]
        public RowSimulated(RowMovement rowMovement) 
        {
            _rowMovement = rowMovement;
        }

        public void Simulated(Row row)
        {
            Simulate(row);
        }

        protected override void OnUpdate(float delta)
        {
            foreach (var row in Entities)
                _rowMovement.Move(row,delta);
        }
    }
}
