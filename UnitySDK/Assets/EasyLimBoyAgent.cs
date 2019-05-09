using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

public class EasyLimBoyAgent : Agent
{
    public GameObject gears;
    public GameObject walls;
    public GameObject floors;
    public GameObject Actionlogic;
    // Start is called before the first frame update
    void Start()
    {

    }
    //this is for when you finished or when you die
    public override void AgentReset()
    {

    }
    //这个是，移动的时候需要获取来帮助判断的参数
    public override void CollectObservations()
    {

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
    }
}