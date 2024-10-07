namespace ShootEmUp.Model
{
    public class RowSimulated : Simulation<Row>,IService
    {
        public void Simulated(Row row)
        {
            Simulate(row);
        }

        protected override void OnUpdate(float delta)
        {
            foreach (var row in Entities)
                row.Move(delta);
        }
    }
}
