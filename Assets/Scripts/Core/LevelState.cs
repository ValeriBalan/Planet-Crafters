using System;

public enum LevelStateType { Locked, Unlocked, InProgress, Completed }

[Serializable]
public class LevelState
{
    public LevelStateType state;
    public int percent;  // 0-100 for InProgress
    public int stars;    // 0-3 for Completed
}
