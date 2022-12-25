using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snrObj : MonoBehaviour
{

    Vector3 pos;
    bool active = false;
    bool activeTime = false;
    public float distance = 0f;
    public float TestObj = 0f;

    //Timer 
    float timer;
    int waitingTime = 1;

    void Start()
    {

        timer = 0.0f;
        waitingTime = 3;

        //SNR_Calculation snr_Calculation = GameObject.Find("Beam").GetComponent<SNR_Calculation>();
        //FieldManagement fieldManagement = GameObject.Find("Field").GetComponent<FieldManagement>();

       
        //자기 자신인지 확인해야 함 
        //Debug.Log(this.gameObject.transform.position.x); 작동 됨 확인

    }

    void Update()
    {
        if (distance > 0 && active == false)
        {
            Debug.Log(distance);

            //Debug.Log(this.gameObject.transform.position.x);
            active = true;
        }

        timer += Time.deltaTime;
        if (timer > waitingTime) {
            activeTime = false;
        }

        if (timer > waitingTime && activeTime == false)
        {
            Debug.Log(distance);
            activeTime = true;
        }
    }

}
