using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public enum SceneIndex {
    BOOT_SCENE = 0,
    TITLE_SCENE = 1,
    GAME_SCENE = 2 
};



public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;

    public bool loadTitleScene = true;


    private List<AsyncOperation> sceneLoadingOperations = new List<AsyncOperation>();


    private void Start() 
    {   
        if(loadTitleScene){
            LoadTitleScene();
        }
        
    }

    private void LoadTitleScene(){    
        Debug.Log("Loading title Scene");
        if(SceneManager.GetSceneByBuildIndex((int)SceneIndex.TITLE_SCENE).isLoaded == true){
            Debug.LogWarning("LoadTitleScene -> TitleScene was already loaded");
            return;
        }
        SceneManager.LoadSceneAsync((int)SceneIndex.TITLE_SCENE, LoadSceneMode.Additive);
    }

    public void InitGameScene(){

        Debug.Log("SceneLoader -> InitGameSCene called");
        ShowLoadingScreen();

        UnloadScene(SceneIndex.TITLE_SCENE);
        LoadScene(SceneIndex.GAME_SCENE);
        
        StartCoroutine(HandleSceneLoadingProgress());

    }

    private void LoadScene(SceneIndex sceneIndex){
        if(SceneManager.GetSceneByBuildIndex((int)sceneIndex).isLoaded == false){
            Debug.LogFormat("SceneLoader->LoadScene called for idx:{0}",sceneIndex);
             sceneLoadingOperations.Add(SceneManager.LoadSceneAsync((int)sceneIndex, LoadSceneMode.Additive));
        }
    }

    private void UnloadScene(SceneIndex sceneIndex){
        if(SceneManager.GetSceneByBuildIndex((int)sceneIndex).isLoaded == true){
            Debug.LogFormat("SceneLoader->Unload called for idx:{0}",sceneIndex);
             sceneLoadingOperations.Add(SceneManager.UnloadSceneAsync((int)sceneIndex));
        }
    }

    public void ShowLoadingScreen(){
        loadingScreen.SetActive(true);
    }

    public void HideLoadingScreen(){
        loadingScreen.SetActive(false);
    }

    public IEnumerator HandleSceneLoadingProgress(){
        for( int idx=0; idx<sceneLoadingOperations.Count; idx++)
        {
            while(!sceneLoadingOperations[idx].isDone){
                yield return null;
            }
        }        

        yield return new WaitForSeconds(0.5f);
        HideLoadingScreen();
        sceneLoadingOperations.Clear();
    }


}
