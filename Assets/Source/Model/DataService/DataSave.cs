using UnityEngine;

namespace ShootEmUp.Model
{
    public class DataSave : IDataService
    {
        public void Save(GameData data)
        {
            string json = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(Application.persistentDataPath + Constant.PathBeforceData, json);
        }

        public GameData Load()
        {
            string path = Application.persistentDataPath + Constant.PathBeforceData;
            if (System.IO.File.Exists(path))
            {
                string json = System.IO.File.ReadAllText(path);
                return JsonUtility.FromJson<GameData>(json);
            }

            return new GameData();
        }
    }
}