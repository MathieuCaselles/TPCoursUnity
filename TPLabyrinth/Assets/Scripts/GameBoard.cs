using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private int rotateSpeed = 100;
    // Update is called once per frame
    void Update()
    {
        float rotateX = Input.GetAxis("Vertical");
        float rotateY = Input.GetAxis("Horizontal");

        Vector3 rotation = new Vector3(rotateX, 0, -rotateY);

        if(rotation.magnitude > 1.0)
        {
            rotation = rotation.normalized;
        }

        transform.Rotate(rotation * Time.deltaTime * rotateSpeed);
    }
}
