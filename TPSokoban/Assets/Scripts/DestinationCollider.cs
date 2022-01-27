using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationCollider : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (_gameManager.State == GameManager.GameState.Running && other.gameObject.CompareTag("Box"))
        {
            _gameManager.IncrementScore();
        }
       
    }

    void OnTriggerExit(Collider other)
    {
        if (_gameManager.State == GameManager.GameState.Running && other.gameObject.CompareTag("Box"))
        {
            _gameManager.DecrementScore();
        }    
    }
}
