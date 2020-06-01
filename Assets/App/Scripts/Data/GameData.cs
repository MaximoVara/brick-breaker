using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

[System.Serializable]
[DataContract]
public class GameData : System.Object
{
    [DataMember]
    public Dictionary<string, Score> scores = new Dictionary<string, Score>();


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
