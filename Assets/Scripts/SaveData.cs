using System;
using System.Collections.Generic;

// То, что лежит в save.json
[Serializable]
public class SaveData
{
    public int version = 1;
    public int bestScore = 0;
    public List<int> unlocked = new List<int>();   // номера открытых уровней
    public float musicVol = 0.8f;
}
