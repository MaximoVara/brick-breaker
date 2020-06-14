using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections.Generic;
using System.Runtime.Serialization;

[System.Serializable]
[DataContract]
public class GameData : System.Object
{
    static readonly string[] LEVELS_UNLOCKED = new string[] { "Level 1" };

    [DataMember]
    public Dictionary<string, Score> scores = new Dictionary<string, Score>();
    [DataMember]
    private List<string> levelsUnlocked = new List<string>(); // hold the levesl taht are unlocked by the player...


    // 
    public void Validate()
    {
        this.ValidateLevels(); // initialize the first level to be unlocked.... or else... how do they play? 
    }
    private void ValidateLevels()
    {
        for (int i = 0; i < GameData.LEVELS_UNLOCKED.Length; ++i){
            var level = GameData.LEVELS_UNLOCKED[i];
            if (this.levelsUnlocked.Contains(level) == false)
            {
                this.levelsUnlocked.Add(level);
            }
        }
    }
    public IReadOnlyList<string> LevelsUnlocked
    {
        get {
            if(this.levelsUnlocked.Contains("Level 1") == false)
            {
                this.levelsUnlocked.Add("Level 1");
            }
            return this.levelsUnlocked;
        }
    }
    public void UnlockLevel(string levelName)
    {
        this.levelsUnlocked.Add(levelName);
    }

    public bool SetScore(string levelName, Score score)
    {
        if(this.scores.ContainsKey(levelName) == false)
        {
            this.scores.Add(levelName, score);
            return true;
        }
        var lastScore = this.scores[levelName];
        if(score.Value > lastScore.Value)
        {
            this.scores[levelName] = score;
            return true;
        }
        return false;
    }

    public Score GetScore(string levelName)
    {
        if(this.scores.ContainsKey(levelName) == false) return null;
        return this.scores[levelName];
    }
}

[System.Serializable]
public class Score : System.Object
{
    private int value;
    private System.DateTime date;
    public Score(int value, System.DateTime date)
    {
        this.value = value;
        this.date = date;
    }
    public int Value
    {
        get
        {
            return this.value;
        } set
        {
            this.value = value;
        }
    }
    public System.DateTime Date
    {
        get
        {
            return this.date;
        } set
        {
            this.date = value;
        }
    }
}
