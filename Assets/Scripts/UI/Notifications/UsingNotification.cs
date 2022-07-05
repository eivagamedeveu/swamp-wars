using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsingNotification : MonoBehaviour
{
    [SerializeField] private UseZone _useZone;
    [SerializeField] private GameObject _textObject;

    private void OnEnable()
    {
        _useZone.IsTargetChanged += OnTargetChange;
    }

    private void OnDisable()
    {
        _useZone.IsTargetChanged -= OnTargetChange;
    }
    
    private void OnTargetChange(bool value)
    {
        _textObject.SetActive(value);
    }
}
