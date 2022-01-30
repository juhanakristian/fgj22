using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameEventHandler : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject FailCurtain;
    public GameObject FailTextGO; 

    private TextMeshProUGUI failTextTMPro;
    bool failedState = false;

    public SceneLoadEvent sceneLoadEvent;

    private void Start() {
        FailCurtain.SetActive(false);
        failTextTMPro = FailTextGO.GetComponent<TextMeshProUGUI>();
        if (failTextTMPro == null){
            Debug.LogError("Failed to find required TextMeshPro component");
        }
        failTextTMPro.SetText("Haha");
    }


    void Update (){

        if(Input.GetKey(KeyCode.Escape)){
            sceneLoadEvent.Raise("Exit");
        }

        if(!failedState){
            return;
        }

        if(Input.anyKey){
            ReloadScene();
        }

    }


    [ContextMenu("Debug OnFailEvent")]
    private void DebugOnFailEvent(){
        Debug.Log("DebugonFailEvent -> Simulating OnFailEvent..");
        OnFailEvent("Testing data");
    }


    public void OnFailEvent(string failReasonMessage){

        Debug.LogFormat("Player failed, reason message: {0}", failReasonMessage);
        FailCurtain.SetActive(true);
        failTextTMPro.SetText(failReasonMessage.ToUpper());
        failedState = true;


    }


    public void ReloadScene()
     {

        if (sceneLoadEvent){
            sceneLoadEvent.Raise("RestartGame");
        }


     }
}
