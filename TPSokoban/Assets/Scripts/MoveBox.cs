using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _normal;
    private bool _playerCollidesWall = false;
    private GameManager _gameManager;
    private HistoricLocation _historicLocation;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _historicLocation = GetComponent<HistoricLocation>();

    }

    private bool CanPushBox() => _gameManager.State == GameManager.GameState.Running && _playerCollidesWall && Input.GetKeyDown(KeyCode.Space) &&
               !Physics.Raycast(transform.position, transform.TransformDirection(_normal), 0.5f);
        private void Update()
    {
        if (CanPushBox())
        {
            _historicLocation.SavePositionAndAddToGameManager();
            
            if (_normal == transform.forward)
            {
                transform.Translate(0,0,1);
                return;
            }

            if (_normal == transform.right)
            {
                transform.Translate(1,0,0);
                return;
            }

            if (_normal == -(transform.forward))
            {
                transform.Translate(0,0,-1);
                return;
            }

            transform.Translate(-1,0,0);
        }        
    }
    

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            _normal = other.contacts[0].normal;
            _playerCollidesWall = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            _playerCollidesWall = false;
        }    
    }
}
