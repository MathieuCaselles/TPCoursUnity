using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private float speed = 2;

    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_gameManager.State == GameManager.GameState.Running)
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveY = Input.GetAxis("Vertical");

            if (moveX !=0 || moveY !=0)
            {
                transform.rotation = Quaternion.LookRotation(moveY * Vector3.forward + moveX * Vector3.right);

                float movement = Math.Abs(moveY) + Math.Abs(moveX);
        
                transform.Translate((movement > 1 ? 1f : movement) * Vector3.forward * Time.deltaTime * speed);
            }
        }
       
    }
}
