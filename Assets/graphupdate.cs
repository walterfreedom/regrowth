using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class graphupdate : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<GraphUpdateScene>().Apply();
    }
}
