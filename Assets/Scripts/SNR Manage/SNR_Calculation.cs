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


    //�ð� ����
    float timecount = 0;
    public float oneSecond = 0.25f;
    int realSecond = 0;

    //�ӽ� �浹 ������Ʈ ����
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
        // FieldManagement Script ���� �ҷ�����
        //FieldManagement fieldManagement = GameObject.Find("Field").GetComponent<FieldManagement>();
        //GameObject[,] carObj = fieldManagement.carObj;
        //float[,] snrResult = fieldManagement.snrResult;
        

    }

    void Update()
    {

        
        // �ð� ��Ʈ���� oneSecond ���� �ϳ��� ��Ʈ��
        if (oneSecond < timecount)
        {
            // Beam Y�� Rotation�ϱ�
            transform.Rotate(new Vector3(0, 1f, 0f));
            timecount = 0;
            realSecond++;       
        }
        // Time.deltaTime�� �ƴ� oneSecond ������ ��Ʈ��
        timecount += 1f * Time.deltaTime;


        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = maxLength;

        //�ݻ� ���ϱ�, reflectoin�� inspector���� ���ϱ�
        for (int i = 0; i < reflections; i++)
        {

            /** �Ÿ� Ȯ���ϱ�
                �ؿ� Vector3.Distance(ray.origin, hit.point)�� carObj�� RayCast�� �Ÿ�           
            **/ 

            //remainingLength�� ó�� ������ �Ÿ��� ����ϸ� hit�� Obj���� �� �Ÿ��� üũ �� �� �ִ�.
            //SNR ��� �Ķ���� �� �ϳ�.
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


                //if (hit.collider.tag == "snrObj")Debug.Log("�浹���\n");
                //�浹 ���� Ȯ�� �� �� �ִ�.
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
