using Pathfinding;
using UnityEngine;
public class Pathfinder : AIPath  {
    public GameObject go;
    public System.Action IsReached;

    public override void OnTargetReached() {
        base.OnTargetReached();
        IsReached?.Invoke();
    }
}