using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>  
/// This function is used to counting progress of addressables.
/// </summary>
public class UILoadingSpinner : MonoBehaviour
{

    #region Variables

    public Slider loadingBar;
    
    public TextMeshProUGUI loadingPercent;
    public DownloadProgress downloadProgress;

    private int percentComplete;
    private int cachedPercentComplete;

    #endregion

    #region Unity Methods

    void OnEnable()
    {
        percentComplete = 0;
    }
    void Start()
    {
        percentComplete = 0;
    }

    void Update()
    {
        if (percentComplete != downloadProgress.downloadProgressOutput)
        {
            loadingPercent.text = downloadProgress.downloadProgressOutput.ToString() + "%";
            percentComplete = downloadProgress.downloadProgressOutput;
        }

        UpdateProgressBar(percentComplete);
    }

    public float SetValue(float value)
    {
        loadingBar.value = value;
        return loadingBar.value;
    }

    public void UpdateProgressBar(float progress)
    {
        loadingBar.normalizedValue = progress;
        Debug.Log(string.Format($"Downloaded {0:P1}", progress));

        if (progress >= 1.0f) this.gameObject.SetActive(false);
    }

    #endregion
}
