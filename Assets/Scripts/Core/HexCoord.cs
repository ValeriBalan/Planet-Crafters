using System;
using UnityEngine;

[Serializable]
public struct HexCoord : IEquatable<HexCoord>
{
    public int x;
    public int y;

    public HexCoord(int x, int y) { this.x = x; this.y = y; }

    public bool Equals(HexCoord other) => x == other.x && y == other.y;
    public override bool Equals(object obj) => obj is HexCoord o && Equals(o);
    public override int GetHashCode() => (x * 397) ^ y;

    public override string ToString() => $"({x},{y})";
}
