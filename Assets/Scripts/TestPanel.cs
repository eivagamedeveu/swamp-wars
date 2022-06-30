using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPanel : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void DisableEnemy()
    {
        _enemy.SetActive(!_enemy.activeSelf);
    }
}
