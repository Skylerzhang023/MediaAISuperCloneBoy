using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterfacer : MonoBehaviour
{

    public GameObject player;
    private int direction;
    private int LastDirection;

    public float speed = 50.0f;
    Quaternion left, right, centre;

    // Use this for initialization
    void Start()
    {
        float x = this.transform.localEulerAngles.x;
        float z = this.transform.localEulerAngles.z;

        left = Quaternion.Euler(x, -5.0f, z);
        right = Quaternion.Euler(x, 5.0f, z);
        centre = Quaternion.Euler(x, 0.0f, z);
        LastDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.GetComponent<PlayerController>().GetWallDirection();

        Debug.Log(direction);

    }
    void FixedUpdate()
    {   
        if(direction != LastDirection){
            StopAllCoroutines();
            CameraBehavior(direction);
            LastDirection = direction;
        } 
    }

    void CameraBehavior(int status)
    {   
        Quaternion currentRotation = this.transform.localRotation;
        //1 wall right  -1 wall left
        if (status == 1)
        {   
            Vector3 rightRotation = new Vector3(this.transform.localEulerAngles.x, 15.0f, this.transform.localEulerAngles.z);

            float a = Quaternion.Angle(currentRotation, right); //degrees we must travel
            StartCoroutine(RotateOverTime(currentRotation, right, a / speed)); //a/speed = time it'd take to travel a at such speed
        }
        else if (status == -1)
        {
            Vector3 leftRotation = new Vector3(this.transform.localEulerAngles.x, -15.0f, this.transform.localEulerAngles.z);

            float a = Quaternion.Angle(currentRotation, left); //degrees we must travel
            StartCoroutine(RotateOverTime(currentRotation, left, a / speed)); //a/speed = time it'd take to travel a at such speed
        }
        else
        {
            float a = Quaternion.Angle(currentRotation, centre); //degrees we must travel
            StartCoroutine(RotateOverTime(currentRotation, centre, a / speed)); //a/speed = time it'd take to travel a at such speed
        }

        //this.transform.localEulerAngles = new Vector3(this.transform.localEulerAngles.x, tempAngle, this.transform.localEulerAngles.z);
    }
    
     IEnumerator RotateOverTime(Quaternion start, Quaternion end, float dur)
    {
        //yield return new WaitForSeconds(.5f);
        float t = 0f;
        while(t < dur)
        {
            this.transform.rotation = Quaternion.Slerp(start, end, t / dur);
            yield return null;
            t += Time.deltaTime;
        }
        this.transform.rotation = end;
    }
}
