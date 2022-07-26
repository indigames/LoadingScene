using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.Events;
using System;

/// <summary>  
/// This class is used to load the addressable scene.
/// </summary>
public class LoadAddressableScene : MonoBehaviour
{

    #region Variables

    // [SerializeField] private List<AssetReference> _sceneAssets = new List<AssetReference>(); // If have multiple scenes
    [SerializeField] private AssetReference _sceneAsset;
    private AsyncOperationHandle<SceneInstance> _sceneHandle;

    public static UnityEvent<float> OnProgress;
    public static UnityEvent<bool> OnUIEnable;
    [SerializeField] private GameObject _uiCamera;
    #endregion


    #region Unity Methods
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        StartCoroutine(DownloadScene());
        OnProgress = new UnityEvent<float>();
        OnUIEnable = new UnityEvent<bool>();
    }

    #endregion

    #region Class

    // <Summary>
    // This function is used to download the scene.
    // </Summary>
    private IEnumerator DownloadScene()
    {
        var downloadScene = Addressables.LoadSceneAsync(_sceneAsset, LoadSceneMode.Additive, true);
        downloadScene.Completed += SceneDownloadComplete;
        Debug.Log($"Scene{_sceneAsset.SubObjectName} Starting download");

        while (!downloadScene.IsDone)
        {
            var status = downloadScene.GetDownloadStatus(); // Get the status of the download
            float progress = status.Percent; //get current progress

            LoadAddressableScene.OnUIEnable?.Invoke(true);
            LoadAddressableScene.OnProgress?.Invoke(progress);

            Debug.Log($"Scene{_sceneAsset.SubObjectName} Downloading {progress * 100}%...");
            yield return null;
        }

        LoadAddressableScene.OnProgress?.Invoke(1);
        LoadAddressableScene.OnUIEnable?.Invoke(false);
        Debug.Log($"Scene{_sceneAsset.SubObjectName} Download Complete");
    }

    // <Summary>
    // This function is used to download the scene.
    // </Summary>
    private void SceneDownloadComplete(AsyncOperationHandle<SceneInstance> _handle)
    {
        if (_handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log($"{_handle.Result.Scene.name} is successfully loaded.");
            LoadAddressableScene.OnUIEnable?.Invoke(false);
            _sceneHandle = _handle;
            _uiCamera.SetActive(false);

            // StartCoroutine(UnloadScene()); // TODO: Unload the scene after it is loaded
        }
    }

    // <Summary>
    // This function is used to unload the scene.
    // </Summary>
    private IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(10f);
        Addressables.UnloadSceneAsync(_sceneHandle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                _uiCamera.SetActive(true);
                LoadAddressableScene.OnUIEnable?.Invoke(true);
                Debug.Log($"{_sceneHandle.Result.Scene.name} is successfully unloaded.");
            }
        };
        yield return new WaitForSeconds(5f);
        StartCoroutine(DownloadScene());
    }

    #endregion
}
