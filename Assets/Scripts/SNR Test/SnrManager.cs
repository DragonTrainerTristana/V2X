using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SnrManager : MonoBehaviour
{
    Vector3 pos;
    GameObject[] snrObj;
    public GameObject Prefab_carObj;
    public GameObject criteria;
    public GameObject LaserBeam;

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
   
    void Awake()
    {

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.black;
    }


    void Start()
    {
        snrObj = new GameObject[5];

        pos = criteria.transform.position;
        Debug.Log(pos.x);
        for (int i = 0; i < 5; i++){
            snrObj[i] = Instantiate(Prefab_carObj, new Vector3(pos.x + (i*3), pos.y, pos.z + 10), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (oneSecond < timecount)
        {
            // Beam Y�� Rotation�ϱ�
            LaserBeam.transform.Rotate(new Vector3(0, 1f, 0f));
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

                if (hit.collider.tag == "snrObj")
                {

                    //Debug.Log(hit.distance); // �Ÿ��� hit.distance�� �´°ɷ�
                    arbitaryObj = hit.collider.gameObject;
                    arbitrayDistance = hit.distance;
                    Debug.Log(arbitrayDistance);
                    //Debug.Log(arbitaryObj.transform.position.x);                   
                    //arbitaryObj.GetComponent<snrObj>().distance = arbitrayDistance;

                    arbitaryObj = null;

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
