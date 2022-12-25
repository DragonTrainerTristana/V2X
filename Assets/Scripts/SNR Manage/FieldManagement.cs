using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManagement : MonoBehaviour
{
    float[,] array; 
    Vector3 pos;
    Vector3 newpos;
    public GameObject[,] carObj; // ������Ʈ ���� �Լ�  
    public GameObject Prefab_carObj;
    public int arraySize_Object = 100; // �ʱ� �� 100 by 100
    public float[,] snrResult; // snr ���� �Լ�
    //List<GameObject> carObjList = new List<GameObject>(); -> List ���� �κ� �ʿ� X

    float timer;
    int waitingTime;
    int countNumber;
   
    // Start is called before the first frame update
    void Start()
    {
        countNumber = 0;
        timer = 0f;
        waitingTime = 100;
        //***** Start �κ� ���� Ȱ��ȭ 

        pos = this.gameObject.transform.position; // ������Ʈ ���� �߽ɰ� ���
        //Debug.Log(pos.x); // �߽ɰ� x ��ǥ
        //Debug.Log(pos.z); // �߽ɰ� y ��ǥ

        carObj = new GameObject[arraySize_Object, arraySize_Object]; // �迭 �� �Ҵ� -> Public ������ Inspector â���� ��ȯ ����
        snrResult = new float[arraySize_Object, arraySize_Object];

        //***** Start �κ� ���� Ȱ��ȭ 

        //Laser�� �ϴ� ��� ô �ϰ� �Ÿ� ����ұ�?


        //***** Obj ���� ��� 

        //3,4���и�

        //�����ϰ� Bubble_Sort�� �ߴµ� QuickSort�� ���濹��
        for (int j = 0; j < arraySize_Object/2; j++)
        {
            for (int i = 0; i < arraySize_Object; i++)
            {
                //x,z = [0,0] �ʱ�ȭ
                carObj[j,i] = Instantiate(Prefab_carObj, new Vector3(pos.x + ((i - 50) * 15)-200, pos.y, pos.z + ((j - 50) * 15)+1100), Quaternion.identity);
                //countNumber++;
                //carObjList.Add(_obj);

            }
        } 
       //1,2���и�

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
        { //�ð� 3�ʵڿ�
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

