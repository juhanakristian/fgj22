using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneLoadEvent", menuName = "fgj22/SceneLoadEvent", order = 0)]
public class SceneLoadEvent : ScriptableObject {

    private List<SceneLoadEventListener> listeners = new List<SceneLoadEventListener>();
    
    public void Raise(string type) 
    {
        for(int i = listeners.Count -1; i >= 0; i--) {
            listeners[i].OnSceneLoadEventRaised(type);
        }
    }

 
    public void RegisterListener(SceneLoadEventListener listener){ 
        listeners.Add(listener); 
    }

    public void UnregisterListener(SceneLoadEventListener listener){ 
    listeners.Remove(listener); 
    }
    
}
