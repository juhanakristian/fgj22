using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[CreateAssetMenu(fileName = "BrainSharer", menuName = "fgj22/BrainSharer", order = 0)]
public class BrainSharer : ScriptableObject
{

    private CinemachineBrain sharedBrain = null;

    public void SetBrain(CinemachineBrain brain){
        sharedBrain = brain;
    }

    public CinemachineBrain GetBrain(){
        if(sharedBrain == null){
            Debug.LogError("BrainSharer has null brain!");
        }
        return sharedBrain;
    }




}
