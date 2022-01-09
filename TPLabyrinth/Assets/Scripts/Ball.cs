using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject playerStart;
    void Start()
    {
        playerStart = GameObject.Find("PlayerStart").gameObject;
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Trap")
        {
            transform.position = new Vector3(playerStart.transform.position.x,playerStart.transform.position.y + transform.localScale.y ,playerStart.transform.position.z);
        }else if (other.gameObject.tag == "Arrival")
        {
            Debug.Log("Bravo !");        
        }
    }
}
