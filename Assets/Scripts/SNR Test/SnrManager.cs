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

    //시간 설정
    float timecount = 0;
    public float oneSecond = 0.25f;
    int realSecond = 0;

    //임시 충돌 오브젝트 변수
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
            // Beam Y로 Rotation하기
            LaserBeam.transform.Rotate(new Vector3(0, 1f, 0f));
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

                if (hit.collider.tag == "snrObj")
                {

                    //Debug.Log(hit.distance); // 거리는 hit.distance가 맞는걸로
                    arbitaryObj = hit.collider.gameObject;
                    arbitrayDistance = hit.distance;
                    Debug.Log(arbitrayDistance);
                    //Debug.Log(arbitaryObj.transform.position.x);                   
                    //arbitaryObj.GetComponent<snrObj>().distance = arbitrayDistance;

                    arbitaryObj = null;

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
