using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enterence : MonoBehaviour, IUsable
{
    [SerializeField] private UIScreen _targetScreen;
    [SerializeField] private string _sceneName;
    
    public void Use()
    {
        if (_targetScreen != null)
        {
            _targetScreen.Enable();
        }

        if(_sceneName != "")
        {
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}
