using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BrainVatter : MonoBehaviour
{

    public BrainSharer brainSharer;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineBrain brain = GetComponent<CinemachineBrain>();
        brainSharer.SetBrain(brain);
        
    }


}
