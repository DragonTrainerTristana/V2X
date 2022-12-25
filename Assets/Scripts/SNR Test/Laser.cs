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

    //�ð� ����
    float timecount = 0;
    public float oneSecond = 0.25f;
    int realSecond = 0;

    //�ӽ� �浹 ������Ʈ ����
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

        // FieldManagement Script ���� �ҷ�����
        // FieldManagement fieldManagement = GameObject.Find("Field").GetComponent<FieldManagement>();
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
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));

                if (hit.collider.tag == "snrObj")
                {           
                    
                    //Debug.Log(hit.distance); // �Ÿ��� hit.distance�� �´°ɷ�
                    arbitaryObj = hit.collider.gameObject;
                    arbitrayDistance = hit.distance;
                    
                             
                    arbitaryObj.GetComponent<snrObj>().distance = arbitrayDistance;
                    //Debug.Log(arbitrayDistance);
                    //arbitaryObj = null;
                   
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
