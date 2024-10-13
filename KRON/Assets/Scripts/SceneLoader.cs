using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void LoadLevel(string iTargetLevel)
    {
        StartCoroutine(AsyncSceneLoadCo(iTargetLevel));
    }

    IEnumerator AsyncSceneLoadCo(string iTargetLevel)
    {
        Access.CameraManager().CullAll(true);

        Scene currentScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(iTargetLevel, LoadSceneMode.Additive);
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }

        Access.Player().previousScene = currentScene.name;
        SceneManager.MoveGameObjectToScene(Access.Player().gameObject, SceneManager.GetSceneByName(iTargetLevel));
        asyncLoad.allowSceneActivation = true;
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        // move objects to other scene
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(iTargetLevel));
        //
        SceneManager.UnloadSceneAsync(currentScene);
        // 
        Access.CameraManager().OnSceneLoaded();

        Access.CameraManager().CullAll(false);
    }

}
