using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjDestroy : MonoBehaviour
{
    Renderer objColor;
    
    bool active_delete = false;

    public bool active = false;
    public float receiverPower = 0f;
    public float snr_Rate = 0f;
    public float distance = 0f;

    float noise = 2f;

    //Timer 
    float timer;
    int waitingTime;
    int deleteTime;

    void Start()
    {
        objColor = gameObject.GetComponent<Renderer>();
        timer = 0.0f;
        waitingTime = 3;
        deleteTime = 20;

    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime) { //5초정도 삭제하는 시간 줘야함
            if (active_delete == false) {
                Destroy(this.gameObject);
            }
        }

        if (timer > deleteTime) {
            if (distance == 0) {
                Destroy(this.gameObject);
            }
        }


        if (active == true) {
            receiverPower = (285000000000)/Mathf.Pow(4*Mathf.PI*distance ,2); // Receiver Power
            // active true일 경우에만 계산 및, 28.5GH 에서, 100 W 로 송신했을 때, Pr 구하기

            snr_Rate = 10 * Mathf.Log10(receiverPower/noise);
            active = false;

            if (snr_Rate > 60)
            {
                objColor.material.color = Color.red;
            }
            else if (snr_Rate < 60 && snr_Rate > 50)
            {
                objColor.material.color = new Color(255/255f, 162/255f, 0/255f, 255/255f);
            }
            else if (snr_Rate < 50 && snr_Rate > 40)
            {
                objColor.material.color = Color.yellow;
            }
            else if (snr_Rate < 40 && snr_Rate > 30) {
                objColor.material.color = Color.green;
            }
            else if (snr_Rate < 30 && snr_Rate > 20)
            {
                objColor.material.color = Color.blue;
            }
            else if (snr_Rate < 20 && snr_Rate > 10)
            {

            }
            else if (snr_Rate < 10 && snr_Rate > 0)
            {

            }
        }

        if (distance > 0) {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }

    }

    void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "Street")
        {
            active_delete = true;
            
            //pos = this.gameObject.transform.position;
            //Debug.Log(pos.x);          
            //Debug.Log("삭제");
        }
    }
}
