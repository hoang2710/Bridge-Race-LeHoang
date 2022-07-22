using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: temp solution fo buildBridgeState condition !!!!!!!!!!!!!!
public class WaypointRefCenter : Singleton<WaypointRefCenter>
{
    public Transform plane1_2;
    public List<Vector3> waypoints;
    private void Start()
    {
        Vector3 way1 = plane1_2.position + Vector3.right * 1.2f + Vector3.back * 2;
        Vector3 way2 = plane1_2.position + Vector3.right * 3 + Vector3.back * 2;
        Vector3 way3 = plane1_2.position + Vector3.left * 1.2f + Vector3.back * 2;
        Vector3 way4 = plane1_2.position + Vector3.left * 3 + Vector3.back * 2;

        waypoints.Add(way1);
        waypoints.Add(way2);
        waypoints.Add(way3);
        waypoints.Add(way4);

        Debug.DrawRay(way1, Vector3.up, Color.cyan, 5f);
        Debug.DrawRay(way2, Vector3.up, Color.cyan, 5f);
        Debug.DrawRay(way3, Vector3.up, Color.cyan, 5f);
        Debug.DrawRay(way4, Vector3.up, Color.cyan, 5f);
    }
}
