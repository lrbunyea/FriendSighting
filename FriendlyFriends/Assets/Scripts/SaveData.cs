using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
    int levelsUnlocked = 0;
    float[] scores = new float[5];
    public SaveData(int unlocked)
    {
        levelsUnlocked = unlocked;
        for (int i = 0; i < scores.Length; i++)
        {
            scores[i] = 100000f;
        }
        
    }

    public SaveData(string data)
    {
        string[] vals = data.Split(' ');
        levelsUnlocked = int.Parse(vals[0]);
        scores[0] = float.Parse(vals[1]);

        for (int i = 0; i < 5; i++)
        {
            scores[i] = float.Parse(vals[i + 1]);
        }
    }

    public int GetUnlocked()
    {
        return levelsUnlocked;
    }
    public int SetUnlocked(int level)
    {
        levelsUnlocked = level;
        return levelsUnlocked;
    }

    public float GetScore(int i)
    {
        return scores[i];
    }
    public float SetScore(int i, float value)
    {
        scores[i] = value;
        return scores[i];
    }

    public override string ToString()
    {
        string result = levelsUnlocked + "";
        foreach (float f in scores)
        {
            result = result + " " + f;
        }
        return result;
    }

}
