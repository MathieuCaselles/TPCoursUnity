using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlateform : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.1f;
    
    [SerializeField]
    private bool CanMove = true;
    
    // Update is called once per frame
    void Start()
    {
        StartCoroutine(DestoryAfterFiveSecond());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Jump") == 1)
        {
            CanMove = true;
        }
        if (CanMove)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
    
    
    private IEnumerator DestoryAfterFiveSecond()
    {
        yield return new WaitForSeconds(5);
        Destroy(this.gameObject);
    }
}
