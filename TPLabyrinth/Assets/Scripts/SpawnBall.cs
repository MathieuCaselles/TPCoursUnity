using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameObject ball = Instantiate(ballPrefab);
        ball.transform.position = new Vector3(transform.position.x,transform.position.y + ballPrefab.transform.localScale.y ,transform.position.z);
    }
    
}
