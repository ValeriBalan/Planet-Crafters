using UnityEngine;

public static class HexGridMath
{
    public static Vector3 AxialToWorldXY(HexCoord c, Vector3 size, float edgeY, Vector2Int centerHex)
    {
        float x = size.x * c.x + (size.x/2 * (c.y % 2) - size.x* (c.y % 2)) - (centerHex.x * size.x) + ((centerHex.y % 2 )* size.x/2);
        float y = (-size.y + edgeY*2) * c.y + (centerHex.y * size.y);
        float z =  - size.z * c.y;
        return new Vector3(x, y, z);
    }
}
