namespace ShootEmUp.Model
{
    public interface IDataService : ISingelService
    {
        void Save(GameData data);
        GameData Load();
    }
}
