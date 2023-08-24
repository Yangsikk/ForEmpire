using UnityEngine;

public abstract class UIProcess { 
    protected Transform canvas;
    public virtual void Initialize() {
        canvas = GameObject.Find("Canvas").transform;
    }
}