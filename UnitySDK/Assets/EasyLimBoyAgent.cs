using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

public class EasyLimBoyAgent : Agent
{
    public Transform gears;
    public Transform Reward;
    public Component Actionlogic;
    private Rigidbody rb;
    private Vector3 startpoint;
    private float predistance;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        startpoint = gameObject.transform.position;
        predistance = Vector3.Distance(this.transform.position, Reward.position);
    }
    //this is for when you finished or when you die
    public override void AgentReset()
    {
        this.rb.angularVelocity = Vector3.zero;
        this.rb.velocity = Vector3.zero;
        this.transform.position = startpoint;
        predistance = Vector3.Distance(this.transform.position, Reward.position);
    }
    //这个是，移动的时候需要获取来帮助判断的参数
    public override void CollectObservations()
    {
        AddVectorObs(Reward.position.y);
        AddVectorObs(Reward.position.x);
        AddVectorObs(gears.position.x);
        AddVectorObs(gears.position.y);

        AddVectorObs(this.transform.position.x);
        AddVectorObs(this.transform.position.y);
        AddVectorObs(rb.velocity);
    

    }

    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Action,size =2
        base.AgentAction(vectorAction, textAction);
        Vector3 ControlSignal = Vector3.zero;
        //ControlSignal.x = vectorAction[0];
        //ControlSignal.z = vectorAction[1];
        //use the vectorAction[0] for moving
        //using the vectorAction[1] for jumping
        this.GetComponent<Train_PlayerControl>().move(vectorAction[0] - 0.5f);
        if(vectorAction[1]>0.5f)
         this.GetComponent<Train_PlayerControl>().jump();

        //Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Reward.transform.position);
        //Reached Target
        if (distanceToTarget < predistance)
        {
            SetReward(0.1f);
        }
        else
        {
            SetReward(-0.2f);
        }
        predistance = distanceToTarget;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle")
        {
            SetReward(-10);
            Done();
        }
        else if (collision.gameObject.tag == "reward")
        {
            SetReward(10);
            Done();
        }
    }
}