using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : MainUIButton
{
    public SceneAsset sceneToLoad;

    public override void Select()
    {
        SceneManager.LoadScene(sceneToLoad.name, LoadSceneMode.Single);
    }
}
