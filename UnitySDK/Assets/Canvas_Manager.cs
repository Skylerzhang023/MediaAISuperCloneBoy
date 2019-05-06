using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas_Manager : MonoBehaviour {
    float timer_f;
    int timer_i,time;
    public Text TimerText,PausedText,FinalText,instruction;
    public GameObject player;
    bool ifpaused;
	// Use this for initialization
	void Start () {
        ifpaused = false;
        time = 0;
	}

    // Update is called once per frame
    void Update() {
        timer_f = Time.time;
        timer_i = Mathf.FloorToInt(timer_f);
        TimerText.GetComponent<Text>().text = "Time Spend:" + time/60;
        //TimerText.GetComponent < Text >().text = "Time Spend:"+timer_i.ToString();
        FinalText.GetComponent<Text>().text = "Time Spend:"+ time / 60;
        if (Input.GetKeyDown(KeyCode.Escape))
            ifpaused = !ifpaused;
        if (ifpaused)
        {   
            PausedText.GetComponent<Text>().enabled = true;
            instruction.GetComponent<Text>().enabled = true;
            player.GetComponent<PlayerController>().enabled = false;
        }
        else
        {
            time++;
            PausedText.GetComponent<Text>().enabled = false;
            instruction.GetComponent<Text>().enabled = false;
            player.GetComponent<PlayerController>().enabled = true ;
        }
    }
}
