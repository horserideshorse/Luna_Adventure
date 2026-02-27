using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mission
{
    public string name { get; private set; }
    public string content { get; private set; }
    public int items { get; private set; }
    public bool isComplete { get; private set; }

    public Mission(string name, string content)
    {
        this.name = name;
        this.content = content;
        isComplete = false;
    }
    public Mission(string name, string content, int items)
    {
        this.name = name;
        this.content = content;
        this.items = items;
        isComplete = false;
    }

    public void IfMissionComplete()
    {
        isComplete = true;
    }
}
public class MissionManager : ManagerBase<MissionManager>
{
    protected List<Mission> MissionsList;//ÈÎÎñ

    public event Action<int> OnMissionsListChange;

    public Mission AddMissions { 
        set {
            MissionsList.Add(value);
            Debug.Log(OnMissionsListChange);
            OnMissionsListChange?.Invoke(MissionsList.Count);
            Debug.Log(OnMissionsListChange);
        }
    }
    public List<Mission> ReadMissions { get => MissionsList; }

    protected void Start()
    {
        MissionsList = new List<Mission> { };
    }
}
