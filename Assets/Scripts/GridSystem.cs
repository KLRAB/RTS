using UnityEngine;
public static class GridSystem
{
    public static float cellSize = 1f;
    public static Vector3 Snap(Vector3 world)
    {
        return new Vector3(
            Mathf.Round(world.x / cellSize) * cellSize,
            world.y,
            Mathf.Round(world.z / cellSize) * cellSize
        );
    }
}
