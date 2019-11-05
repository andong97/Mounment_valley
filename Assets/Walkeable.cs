using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walkeable : MonoBehaviour
{
    public List<WalkPath> possiblePaths = new List<WalkPath>();

    public bool isStair = false;
    public float walkPointOffset = 0.5f;
    public float stairOffset = 0.4f;
    public Transform previousCube;

    public Vector3 GetWalkPoint() {
        float stair = isStair ? stairOffset : 0;
        return transform.position + transform.up * walkPointOffset - transform.up * stair;
    }
}

[System.Serializable]
public class WalkPath
{
    public Transform target;
    public bool active = true;
}
