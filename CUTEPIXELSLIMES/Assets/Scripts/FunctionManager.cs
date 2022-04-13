using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class FunctionManager
{
    public static float CheckDistanceNotSquared(Vector3 a, Vector3 b)
    {
        float abX = a.x - b.x;
        float abY = a.y - b.y;
        float abZ = a.z - b.z;
        return (abX * abX + abY * abY + abZ * abZ);
    }
    public static Vector3 GetRandomVector3(Vector3 a, Vector3 b)
    {
        float x = Random.Range(a.x, b.x);
        float y = Random.Range(a.y, b.y);
        float z = Random.Range(a.z, b.z);
        return new Vector3(x, y, z);
    }
}
