using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManagement : MonoBehaviour
{
    float[,] array; 
    Vector3 pos;
    Vector3 newpos;
    public GameObject[,] carObj; // 오브젝트 관리 함수  
    public GameObject Prefab_carObj;
    public int arraySize_Object = 100; // 초기 값 100 by 100
    public float[,] snrResult; // snr 관리 함수
    //List<GameObject> carObjList = new List<GameObject>(); -> List 관리 부분 필요 X

    float timer;
    int waitingTime;
    int countNumber;
   
    // Start is called before the first frame update
    void Start()
    {
        countNumber = 0;
        timer = 0f;
        waitingTime = 100;
        //***** Start 부분 변수 활성화 

        pos = this.gameObject.transform.position; // 오브젝트 생성 중심값 계산
        //Debug.Log(pos.x); // 중심값 x 좌표
        //Debug.Log(pos.z); // 중심값 y 좌표

        carObj = new GameObject[arraySize_Object, arraySize_Object]; // 배열 값 할당 -> Public 변수로 Inspector 창에서 변환 가능
        snrResult = new float[arraySize_Object, arraySize_Object];

        //***** Start 부분 변수 활성화 

        //Laser로 일단 쏘는 척 하고 거리 계산할까?


        //***** Obj 생성 방안 

        //3,4차분면

        //간단하게 Bubble_Sort로 했는데 QuickSort로 변경예정
        for (int j = 0; j < arraySize_Object/2; j++)
        {
            for (int i = 0; i < arraySize_Object; i++)
            {
                //x,z = [0,0] 초기화
                carObj[j,i] = Instantiate(Prefab_carObj, new Vector3(pos.x + ((i - 50) * 15)-200, pos.y, pos.z + ((j - 50) * 15)+1100), Quaternion.identity);
                //countNumber++;
                //carObjList.Add(_obj);

            }
        } 
       //1,2차분면

        for (int j = arraySize_Object/2; j < arraySize_Object; j++)
        {
            for (int i = 0; i < arraySize_Object; i++)
            {
                carObj[j, i] = Instantiate(Prefab_carObj, new Vector3(pos.x + ((i - 50) * 15)-200, pos.y, pos.z + ((j - 50) * 15)+1100), Quaternion.identity);
                //countNumber++;
                //carObjList.Add(_obj);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= waitingTime)
        { //시간 3초뒤에
            for (int j = 0; j < arraySize_Object; j++) {
                for (int i = 0; j < arraySize_Object; i++) {
                    if (carObj[j, i] != null)
                    {
                        snrResult[j, i] = carObj[j, i].GetComponent<ObjDestroy>().distance;
                        if(snrResult[j,i] > 0)Debug.Log(snrResult[j, i]);
                    }
                }
            }
        }


    }




}

