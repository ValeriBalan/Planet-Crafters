using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SpaceGame/Planet Level Map Config", fileName = "PlanetLevelMapConfig")]
public class PlanetLevelMapConfig : ScriptableObject
{
    public int planetId = 1;
    public Vector3 hexSize;
    public float edgeY;
    public Vector2Int centerHex;
    public List<LevelNodeConfig> nodes = new List<LevelNodeConfig>();
}

[Serializable]
public class LevelNodeConfig
{
    public int levelId;
    public int q;
    public int r;
}
