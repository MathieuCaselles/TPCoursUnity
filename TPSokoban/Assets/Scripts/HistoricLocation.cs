using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoricLocation : MonoBehaviour
{
    private List<Vector3> _oldLocations = new List<Vector3>();
    private List<Quaternion> _oldRotations = new List<Quaternion>();
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SavePositionAndAddToGameManager()
    {
        _oldLocations.Add(transform.position);
        _gameManager.PushToHistoricLocationScript(GetComponent<HistoricLocation>());
    }
    
    public void SavePosition()
    {
        _oldLocations.Add(transform.position);
    }
    public void SaveRotation()
    {
        _oldRotations.Add(transform.rotation);
    }
    
    public void SavePositionAndRotation()
    {
        SavePosition();
        SaveRotation();
    }
    
    public void UndoPosition()
    {
        int lastIndex = _oldLocations.Count - 1;

        if (lastIndex >= 0 )
        {
            transform.position = _oldLocations[lastIndex];
            _oldLocations.RemoveAt(lastIndex);
        }
    }
    public void UndoRotation()
    {
        int lastIndex = _oldLocations.Count - 1;

        if (lastIndex >= 0 )
        {
            transform.rotation = _oldRotations[lastIndex];
            _oldRotations.RemoveAt(lastIndex);
        }
    }
    
    public void UndoPositionAndRotation()
    {
         UndoPosition();
         UndoRotation();
    }
}
