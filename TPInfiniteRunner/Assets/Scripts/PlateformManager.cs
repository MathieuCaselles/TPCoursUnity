using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlateformManager : MonoBehaviour
{
    [SerializeField]
    private GameObject PlateformPrefab;
    
    [SerializeField]
    private GameObject DoublePlateformPrefab;

    private bool CanSpawn = false;
    


    private void Update()
    {
        if (!CanSpawn)
        {
            if (Input.GetAxis("Jump") == 1)
            {
                CanSpawn = true;
                StartCoroutine(SpawnPlateforms());
            }
        }

    }

    private IEnumerator SpawnPlateforms()
    {
        int randomNumber =Random.Range(0, 3);

        if (randomNumber != 2)
        {
            GameObject ball = Instantiate(PlateformPrefab);
            Bounds aera = GetComponent<Collider>().bounds;
            float posX = transform.position.x;
            float posY = Random.Range(aera.min.y, aera.max.y);
            ball.transform.position = new Vector3(posX, posY, transform.position.z);
            
            Vector3 ballLocalScale = ball.transform.localScale;
 
            ball.transform.localScale = new Vector3(Random.Range(3f, 6.1f), ballLocalScale.y, ballLocalScale.z);
        }
        else
        {
            GameObject ball = Instantiate(DoublePlateformPrefab);
            Bounds aera = GetComponent<Collider>().bounds;
            Vector3 ballPosition = transform.position;
            float posX = ballPosition.x;
            float posY = Random.Range(aera.min.y, aera.max.y);
            ball.transform.position = new Vector3(posX, posY, ballPosition.z);
            
            Vector3 ballLocalScale = ball.transform.localScale;
            
            ball.transform.localScale = new Vector3(Random.Range(3f, 6.1f), ballLocalScale.y, ballLocalScale.z);
           
            foreach (Transform child in transform)
            {
                Debug.Log(child.name);
            }
            Debug.Log("transform.childCount");
            Debug.Log(transform.childCount);
            Transform childs = transform.GetChild(0);
            Debug.Log(childs.gameObject);
            childs.localScale = new Vector3(Random.Range(3f, 6.1f), ballLocalScale.y, ballLocalScale.z);
        }
        
       
        yield return new WaitForSeconds(5);

        StartCoroutine(SpawnPlateforms());
    }
}
