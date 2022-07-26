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


    #endregion

    #region Unity Methods

    void OnEnable()
    {
        loadingPercent.text = "0%";
        loadingBar.value = 0;
    }
    void Start()
    {
        LoadAddressableScene.OnProgress.AddListener(UpdateProgressBar);
        LoadAddressableScene.OnUIEnable.AddListener(OnActive);
    }
    private void UpdateProgressBar(float progress)
    {
        loadingPercent.text = ((int)progress * 100).ToString() + " %";
        Debug.Log($"Progress: {progress*100}");
        loadingBar.normalizedValue = progress;
        if (progress >= 1.0f) this.gameObject.SetActive(false);
    }

    private void OnActive(bool active)
    {
        this.gameObject.SetActive(active);
    }


    #endregion
}
