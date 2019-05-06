using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.CommunicatorObjects;

public class CubeAgent : Agent
{
    Rigidbody rBody;
    public float speed;
    private float predistance;
    private Vector3 StartPosition;
    public Transform obstacle1,obstacle2;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        StartPosition = this.transform.position;

    }
    public Transform Target;

    //every time when the player hit the target, call the reset function or the 
    public override void AgentReset() {
        this.rBody.angularVelocity = Vector3.zero;
        this.rBody.velocity = Vector3.zero;
        this.transform.position = StartPosition;
        predistance = Vector3.Distance(this.transform.position, Target.position);
    }

    //这个是，移动的时候需要获取来帮助判断的参数
    public override void CollectObservations() {
        //Target and Agent position
        AddVectorObs(Target.transform.position.z);
        AddVectorObs(Target.transform.position.x);
        AddVectorObs(this.transform.position.x);
        AddVectorObs(this.transform.position.z);
        AddVectorObs(obstacle1.transform.position.x);
        AddVectorObs(obstacle1.transform.position.z);
        AddVectorObs(obstacle2.transform.position.x);
        AddVectorObs(obstacle2.transform.position.z);
        //AddVectorObs(reward.transform.position.x);
        //AddVectorObs(reward.transform.position.z);
    }
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        //Action,size =2
        base.AgentAction(vectorAction, textAction);
        Vector3 ControlSignal = Vector3.zero;
        ControlSignal.x = vectorAction[0];
        ControlSignal.z = vectorAction[1];


        gameObject.transform.Translate(ControlSignal.x * speed, 0, ControlSignal.z * speed);

        //Rewards
        float distanceToTarget = Vector3.Distance(this.transform.position, Target.position);
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

        if (gameObject.transform.position.z > 3.8 || gameObject.transform.position.z < -3.35 || gameObject.transform.position.x > 4.66 || gameObject.transform.position.x < -4.66) {
            SetReward(-10);
            Done();
         }

    }
    private void OnCollisionEnter(Collision collision)
    {
        //print("reach");
        if (collision.gameObject.tag == "Target") {
            SetReward(10);
            Done();
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            SetReward(-20);
            Done();
            //print("obstacle");
        }

    }
}
