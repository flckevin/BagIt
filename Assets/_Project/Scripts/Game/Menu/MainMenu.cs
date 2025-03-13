using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("GAME UI")]
    public TextMeshProUGUI playButtonText;

    // Start is called before the first frame update
    void Start()
    {
        //================= GET =======================
        //================= GET =======================

        //================= SET =======================
        playButtonText.text = $"Level {GameData.Level}";
        //================= SET =======================
    }

    #region GENERAL

    /// <summary>
    /// function to change scene
    /// </summary>
    /// <param name="_sceneIndex"> scene to change to </param>
    public void General_ChangeScene(int _sceneIndex) 
    { 
        //load to given scene
        SceneManager.LoadScene( _sceneIndex );
    }

    /// <summary>
    /// function to toggle on and off of an gameobject
    /// </summary>
    /// <param name="_target"> target to toggle </param>
    public void General_targetToggle(GameObject _target) 
    { 
        switch(_target.activeSelf) 
        {
            case true:
                _target.gameObject.SetActive(false);
                break;
            case false:
                _target.gameObject.SetActive(true);
                break;
        }
    }

    #endregion
}
