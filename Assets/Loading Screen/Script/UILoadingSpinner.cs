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
    public float speed;
    
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

    public float SetValue(float value)
    {
        loadingBar.value = value;
        return loadingBar.value;
    }

    #endregion
}
