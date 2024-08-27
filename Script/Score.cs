using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

// Application.persistentDataPath: C:\Users\username\AppData\LocalLow\CompanyName\ProductName

[Serializable]
public class ScoreElement
{
    public string name;
    public int score;
}

[CreateAssetMenu]
public class Score : ScriptableObject
{
    public List<ScoreElement> scores = new ();
    public string diff;

    public void AddScore(string name, int score)
    {
        ScoreElement newScore = new()
        {
            name = name,
            score = score
        };

        scores.Add(newScore);
        SortScores();
    }

    void SortScores()
    {
        scores.Sort((a, b) => b.score.CompareTo(a.score));
    }

    public void RemoveScore(string name)
    {
        scores.RemoveAll(score => score.name == name);
    }

    public int GetScore(string name)
    {
        ScoreElement score = scores.Find(s => s.name == name);
        return score.score;
    }

    public void SetScore(string name, int score)
    {
        ScoreElement scoreElement = scores.Find(s => s.name == name);
        if(scoreElement == null)
        {
            AddScore(name, score);
            return;
        }
        scoreElement.score = score;
        SortScores();
    }

    public void ClearScores()
    {
        scores.Clear();
    }

    public void DeleteData()
    {
        string path =  Application.persistentDataPath + "/scores" + diff + ".json";

        if (File.Exists(path))
        {
            File.Delete(path);
        }
        ClearScores();
    }

    public void SaveScores()
    {
        string path =  Application.persistentDataPath + "/scores" + diff + ".json";

        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, json);
    }

    void LoadScores()
    {
        ClearScores();

        string path =  Application.persistentDataPath + "/scores" + diff + ".json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    public void PrintScores()
    {
        foreach (ScoreElement score in scores)
        {
            Debug.Log(score.name + ": " + score.score);
        }
    }

    void OnEnable()
    {
        LoadScores();
    }

    void OnDisable()
    {
        SaveScores();
    }

    void OnDestroy()
    {
        SaveScores();
    }
}