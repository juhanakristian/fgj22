using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject creditsVCamera;
    public GameObject creditsLight;
    private bool showCreditsState=false;


    void Start()
    {
        
    }


    public void OnStartGame(){
        // Load the scene in a dirty way..
        Debug.Log("OnStartGame called");
    }

    public void OnShowCredits(){
        // Present developers somehow
         Debug.Log("OnShowCredits called");
        creditsLight.SetActive(true);

        if(!showCreditsState){
            creditsVCamera.SetActive(true);
            showCreditsState = true;
        }
        else {
            Debug.LogWarning("Nuup");
            creditsVCamera.SetActive(false);
            showCreditsState = false;
        }
    }

    public void OnQuitGame(){
        // Quit / Exit from the game
        Debug.Log("OnExitGame called!");
    }


}
