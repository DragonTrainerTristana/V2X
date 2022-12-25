using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    //시간 설정
    float timecount = 0;
    public float oneSecond = 0.25f;
    int realSecond = 0;

    //임시 충돌 오브젝트 변수
    GameObject arbitaryObj;
    float arbitrayDistance = 0f;
    int hitCount = 0;
    Vector3 pos;


    void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
    }

    void Start()
    {

        // FieldManagement Script 변수 불러오기
        // FieldManagement fieldManagement = GameObject.Find("Field").GetComponent<FieldManagement>();
        //GameObject[,] carObj = fieldManagement.carObj;
        //float[,] snrResult = fieldManagement.snrResult;


    }

    void Update()
    {


        // 시간 컨트롤은 oneSecond 변수 하나만 컨트롤
        if (oneSecond < timecount)
        {
            // Beam Y로 Rotation하기
            transform.Rotate(new Vector3(0, 1f, 0f));
            timecount = 0;
            realSecond++;
        }
        // Time.deltaTime이 아닌 oneSecond 변수만 컨트롤
        timecount += 1f * Time.deltaTime;


        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        //반사 정하기, reflectoin은 inspector에서 정하기
        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag == "snrObj")
                {           
                    
                    //Debug.Log(hit.distance); // 거리는 hit.distance가 맞는걸로
                    arbitaryObj = hit.collider.gameObject;
                    arbitrayDistance = hit.distance;
                    
                             
                    arbitaryObj.GetComponent<snrObj>().distance = arbitrayDistance;
                    //Debug.Log(arbitrayDistance);
                    //arbitaryObj = null;
                   
                }


                //if (hit.collider.tag == "snrObj")Debug.Log("충돌됬네\n");
                //충돌 됨을 확인 할 수 있다.

                
                if (hit.collider.tag != "Mirror")
                {
                    break;
                }
                
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}
