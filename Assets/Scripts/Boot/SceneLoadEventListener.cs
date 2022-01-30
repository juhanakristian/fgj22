using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoadEventListener : MonoBehaviour
{
    public SceneLoadEvent Event;

    SceneLoader sceneLoader=null;
    void Start() {
        sceneLoader = GetComponent<SceneLoader>();
        if(!sceneLoader){
            Debug.LogError("Missing required SceneLoader component");
        }
    }

    private void OnEnable(){ 
        Event.RegisterListener(this);
    }

    private void OnDisable(){ 
        Event.UnregisterListener(this);
    }

    public void OnSceneLoadEventRaised(string type){ 
        Debug.LogFormat("OnSceneLoadEventRaised  type:{0}", type);

        switch(type){
            case "StartGame":
                sceneLoader.InitGameScene();
            break;

            case "RestartGame":
                sceneLoader.ResetGameScene();
            break;

            default:
                Debug.LogErrorFormat("OnSceneLoadEventRaised -> Unsupported type:{0}",type);
            break;

        }
        
    }
}
