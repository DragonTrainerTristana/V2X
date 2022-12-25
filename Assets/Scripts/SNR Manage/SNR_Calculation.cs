using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(LineRenderer))]
public class SNR_Calculation : MonoBehaviour
{
    public int reflections;
    public float maxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private Vector3 pos;
    private float [] final_distance;
    private int countNum;
    private bool status = false;


    //시간 설정
    float timecount = 0;
    public float oneSecond = 0.25f;
    int realSecond = 0;

    //임시 충돌 오브젝트 변수
    public GameObject arbitaryObj;
    public GameObject Prefab_Final;
    public float arbitrayDistance = 0f;
    public int hitCount = 0;


    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
        lineRenderer.SetWidth(5,5);
    }

    void Start()
    {
        //FileStream test = new FileStream("C:/Users/admin/Downloads/test.csv",FileMode.Create);
        //StreamWriter testStreamWriter = new StreamWriter(test);
        //countNum = 0;
        //final_distance = new float[1000];
        //for (int i = 0; i < 1000; i++)final_distance[i] = 0f;
        // FieldManagement Script 변수 불러오기
        //FieldManagement fieldManagement = GameObject.Find("Field").GetComponent<FieldManagement>();
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

            /** 거리 확인하기
                밑에 Vector3.Distance(ray.origin, hit.point)가 carObj와 RayCast의 거리           
            **/ 

            //remainingLength로 처음 설정한 거리와 계산하면 hit된 Obj와의 빔 거리를 체크 할 수 있다.
            //SNR 계산 파라미터 중 하나.
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag == "snrObj") {

                    arbitaryObj = hit.collider.gameObject;
                  
                    arbitrayDistance = hit.distance;
                    if (arbitaryObj.GetComponent<ObjDestroy>().distance != 0)
                    {
                        if (arbitaryObj.GetComponent<ObjDestroy>().distance > arbitrayDistance) {

                            arbitaryObj.GetComponent<ObjDestroy>().distance = arbitrayDistance;
                            arbitaryObj.GetComponent<ObjDestroy>().active = true;
                        }
                    }
                    else {
                        arbitaryObj.GetComponent<ObjDestroy>().distance = arbitrayDistance;
                        arbitaryObj.GetComponent<ObjDestroy>().active = true;
                    }             
                    //arbitaryObj.GetComponent<ObjDestroy>().distance = arbitrayDistance;

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
