using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LocalDataManager : Singleton<LocalDataManager>
{
    private string _key = "Config";
    private string _configUrl = "https://drive.google.com/uc?export=download&id=1L0tQsah_8JmVlD4WOfGbAf9ME3pXeZo-";
    
    public Config Config { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        
        StartCoroutine(LoadConfig());
    }

    private IEnumerator LoadConfig()
    {
        var request = UnityWebRequest.Get(_configUrl);

        yield return request.SendWebRequest();

        while (!request.isDone)
            yield return null;
        
        if (request.result != UnityWebRequest.Result.Success)
        {
            if (PlayerPrefs.HasKey(_key))
            {
                Config = JsonUtility.FromJson<Config>(PlayerPrefs.GetString(_key));
            }
        }
        else
        {
            Config = JsonUtility.FromJson<Config>(request.downloadHandler.text);
            PlayerPrefs.SetString(_key, JsonUtility.ToJson(Config));
        }

        request.Abort();
        request.Dispose();
        
        SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Single);
    }

    public void CreateConfigTemplate()
    {
        var path = Path.Combine(Application.dataPath, "ConfigTemplate.json");

        Config = new Config();
        
        File.WriteAllText(path, JsonUtility.ToJson(Config));
    }
}

public class Config
{
    public float DieDelay;
    public float StaminaStunlock;
    public float BlockPercent;
    public float BlockStaminaConsume;
    public float RollStaminaConsume;
    public float ShortWeaponRange;
    public float MediumWeaponRange;
    public float LongWeaponRange;
    public float RangeWeaponRange;
    public float ThrowWeaponRange;
    public float ThrowWeaponStunlock;
    public float ShortWeaponWeightMultiplier;
    public float MediumWeaponWeightMultiplier;
    public float LongWeaponWeightMultiplier;
    
    public Config()
    {
        DieDelay = 2f;
        StaminaStunlock = 2f;
        BlockPercent = 0.5f;
        BlockStaminaConsume = 5f;
        RollStaminaConsume = 2f;
        ShortWeaponRange = .5f;
        MediumWeaponRange = 1f;
        LongWeaponRange = 2f;
        RangeWeaponRange = 4f;
        ThrowWeaponRange = 3f;
        ThrowWeaponStunlock = 2f;
        ShortWeaponWeightMultiplier = .5f;
        MediumWeaponWeightMultiplier = 1.5f;
        LongWeaponWeightMultiplier = 3f;
    }
}