using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
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
    }

    public void OnQuitGame(){
        // Quit / Exit from the game
        Debug.Log("OnExitGame called!");
    }


}
