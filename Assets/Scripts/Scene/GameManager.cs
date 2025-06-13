using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject blackScreen;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SwitchToScene(string sceneName)
    {
        StartCoroutine(SwitchToSceneCoroutine(sceneName));
    }

    private IEnumerator SwitchToSceneCoroutine(string sceneName)
    {
        if (blackScreen != null){
            blackScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForSeconds(1);
        }

        Scene currentActiveScene = SceneManager.GetActiveScene();

        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            while (load.progress < 0.90f)
                yield return null;
        }

        DisableAllRootObjectsInScene(currentActiveScene);
        DisableAllCamerasInScene(currentActiveScene);
        if (blackScreen != null)
            blackScreen.SetActive(false);
        
        while (!SceneManager.GetSceneByName(sceneName).isLoaded)
            yield return null;

        Scene sceneToActivate = SceneManager.GetSceneByName(sceneName);
        if (sceneToActivate.IsValid())
        {
            SceneManager.SetActiveScene(sceneToActivate);
        }

        EnableAllRootObjectsInScene(sceneToActivate);
        EnableFirstCameraInScene(sceneToActivate);
        Debug.Log("Switched to scene: " + SceneManager.GetActiveScene().name);
    }


    public void RestartScene(string sceneName)
    {
        StartCoroutine(RestartSceneCoroutine(sceneName));
    }

    private IEnumerator RestartSceneCoroutine(string sceneName)
    {
        if (blackScreen != null){
            blackScreen.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            yield return new WaitForSeconds(1);
        }
        
        Scene currentActiveScene = SceneManager.GetActiveScene();
        DisableAllRootObjectsInScene(currentActiveScene);
        DisableAllCamerasInScene(currentActiveScene);

        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            AsyncOperation unload = SceneManager.UnloadSceneAsync(sceneName);
            while (!unload.isDone)
                yield return null;
        }

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (!load.isDone)
            yield return null;

        Scene reloadedScene = SceneManager.GetSceneByName(sceneName);
        if (reloadedScene.IsValid())
        {
            SceneManager.SetActiveScene(reloadedScene);
        }

        EnableAllRootObjectsInScene(reloadedScene);
        EnableFirstCameraInScene(reloadedScene);
    }

    private void DisableAllRootObjectsInScene(Scene scene)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            if (go.GetComponent<GameManager>() != null) continue; // Skip disabling the GameManager
            go.SetActive(false);
        }
    }

    private void EnableAllRootObjectsInScene(Scene scene)
    {
        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (GameObject go in rootObjects)
        {
            go.SetActive(true);
        }
    }

    private void DisableAllCamerasInScene(Scene scene)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            foreach (Camera cam in go.GetComponentsInChildren<Camera>(true))
            {
                cam.enabled = false;
            }
        }
    }

    private void EnableFirstCameraInScene(Scene scene)
    {
        foreach (GameObject go in scene.GetRootGameObjects())
        {
            foreach (Camera cam in go.GetComponentsInChildren<Camera>(true))
            {
                cam.enabled = true;
                Camera.SetupCurrent(cam); // Optional: forces it as current
                return;
            }
        }
    }

    public void Quit() 
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
