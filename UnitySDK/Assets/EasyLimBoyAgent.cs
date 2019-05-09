using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

public class EasyLimBoyAgent : Agent
{
    public GameObject gears;
    public GameObject Reward;
    public GameObject Actionlogic;
    private Rigidbody rb;
    private Vector3 startpoint;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startpoint = gameObject.transform.position;
    }
    //this is for when you finished or when you die
    public override void AgentReset()
    {
        this.rb.angularVelocity = Vector3.zero;
        this.rb.velocity = Vector3.zero;
        this.transform.position = startpoint;
    }
    //这个是，移动的时候需要获取来帮助判断的参数
    public override void CollectObservations()
    {
        AddVectorObs(Reward.transform.position.y);
        AddVectorObs(Reward.transform.position.x);
        AddVectorObs(gears.transform.position.x);
        AddVectorObs(gears.transform.position.y);

        AddVectorObs(this.transform.position.x);
        AddVectorObs(this.transform.position.y);
        AddVectorObs(rb.velocity);
    

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
    }
}