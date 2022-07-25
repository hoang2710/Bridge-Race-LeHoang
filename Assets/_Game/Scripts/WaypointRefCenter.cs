using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NOTE: temp solution fo buildBridgeState condition !!!!!!!!!!!!!!
[System.Serializable]
public class WaypointsData
{
    public Level_Stage LevelStage;
    public List<Transform> waypoints;
}
public class WaypointRefCenter : Singleton<WaypointRefCenter>
{
    [NonReorderable]
    public List<WaypointsData> WaypointsDatas;
    public Dictionary<Level_Stage, List<Transform>> WaypointsRef = new Dictionary<Level_Stage, List<Transform>>();
    private void Start()
    { 
        foreach (var item in WaypointsDatas)
        {
            Debug.Log(item.LevelStage + "  BBB  " + item.waypoints.Count);
            WaypointsRef.Add(item.LevelStage, item.waypoints); Debug.Log(WaypointsRef.Count + "  aaa");
        }
    }
}
