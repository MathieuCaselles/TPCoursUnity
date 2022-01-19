using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    public GameObject ballPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ResetBallPosition();
    }

    public void ResetBallPosition()
    {
        GameObject ball = Instantiate<GameObject>(ballPrefab);
        ball.transform.position = new Vector3(transform.position.x,transform.position.y + ballPrefab.transform.localScale.y ,transform.position.z);
    }
    
}
