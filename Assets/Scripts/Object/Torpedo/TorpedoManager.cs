using UnityEngine;
using System.Collections;

public class TorpedoManager : MonoBehaviour {

    private ArrayList childrenArray = new ArrayList();
    private ArrayList sonarArray = new ArrayList();

    void OnInstantiatedChild(GameObject target)
    {
        Debug.Log("TorpedManager.OnInstantiatedChild");
        childrenArray.Add(target);
        sonarArray.Add(target);
    }

    void OnDestroyChild(GameObject target)
    {
        Debug.Log("TorpedManager.OnDestroyChild");
        childrenArray.Remove(target);
        sonarArray.Remove(target);
    }

    public int ChildrenNum()
    {
        if (childrenArray != null) return childrenArray.Count;
        return 0;
    }

    // 管理している子の参照
    public ArrayList Children() { return childrenArray; }
    // ソナーにあたった分をとっておく
    public ArrayList SonarChildren() { return sonarArray; }
}
