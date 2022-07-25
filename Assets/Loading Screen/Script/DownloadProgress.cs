using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>  
/// This function is used to counting progress of addressables.
/// </summary>
public class DownloadProgress : MonoBehaviour
{

    #region Variables

    public int downloadProgressInput;
    public int downloadProgressOutput;
    private int cachedDownloadProgressInput;


    #endregion

    #region Unity Methods

    void Start()
    {
        downloadProgressOutput = 0;
    }

    void Update()
    {
        if (cachedDownloadProgressInput != downloadProgressInput)
        {
            downloadProgressOutput = downloadProgressInput;
            cachedDownloadProgressInput = downloadProgressInput;
        }
    }

    #endregion
}
