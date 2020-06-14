using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "App/Objects/Create Level Database")]
public class LevelDatabase : SOSingleton<LevelDatabase>
{
    [SerializeField]
    private LevelData[] levels = new LevelData[1];

    public IEnumerable<LevelData> Levels
    {
        get
        {
            return this.levels;
        }
    }
    public LevelData GetLevelData(string levelName)
    {
        for(int i = 0; i < this.levels.Length; ++i)
        {
            var level = this.levels[i];
            if(string.Compare(level.Name, levelName) == 0 )
            {
                return level;
            }
        }
        return null;
    }
    [System.Serializable]
    public class LevelData : System.Object
    {
        [SerializeField]
        private string name;
        [SerializeField]
        private Sprite icon;
        [SerializeField]
        private string nextLevel;

        public string Name
        {
            get
            {
                return this.name;
            }
        }
        public Sprite Icon
        {
            get
            {
                return this.icon;
            }
        }
        public string NextLevel
        {
            get
            {
                return this.nextLevel;
            }
        }
    }
}