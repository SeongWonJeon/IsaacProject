using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro.EditorUtilities;
using UnityEditor.EditorTools;
using UnityEditor.SearchService;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const string DefaultName = "GameManager";

    private static GameManager instance;
    private static DataManager data;
    private static PoolManager pool;
    private static ResourceManager resource;
    private static UiManager ui;
    private static SoundManager sound;

    public static GameManager Instance { get { return instance; } }
    public static DataManager Data { get { return data; } }
    public static PoolManager Pool { get { return pool; } }
    public static ResourceManager Resource { get { return resource; } }
    public static UiManager UI { get { return ui; } }
    public static SoundManager Sound { get { return sound; } }


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this);
        InitManagers();
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    private void InitManagers()
    {
        GameObject resourceManager = new GameObject();
        resourceManager.name = "ResourceManager";
        resourceManager.transform.parent = transform;
        resource = resourceManager.AddComponent<ResourceManager>();         // 순서도 중요하다

        GameObject soundManager = new GameObject();
        soundManager.name = "SoundManager";
        soundManager.transform.parent = transform;
        sound = soundManager.AddComponent<SoundManager>();

        GameObject poolObj = new GameObject();      // 처음으로 풀오브젝트를 만들어주고 
        poolObj.name = "PoolManager";               // 풀오브젝트의 이름
        poolObj.transform.parent = transform;       // 게임매니저 하위자식으로 두는 방법
        pool = poolObj.AddComponent<PoolManager>(); // 풀매니저한테 AddComponent사용해서 붙혀준다

        GameObject uiObj = new GameObject();
        uiObj.name = "UiManager";
        uiObj.transform.parent = transform;
        ui = uiObj.AddComponent<UiManager>();

        GameObject dataManager = new GameObject();
        dataManager.name = "DataManager";
        dataManager.transform.parent = transform;
        data = dataManager.AddComponent<DataManager>();

    }
    public static void Clear()
    {
        Sound.Clear();
    }
}
