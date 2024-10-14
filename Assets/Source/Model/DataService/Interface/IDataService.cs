namespace ShootEmUp.Model
{
    public interface IDataService
    {
        void Save(GameData data);
        GameData Load();
    }
}
