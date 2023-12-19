using UnityEngine;

[CreateAssetMenu(fileName = "InteractData", menuName = "ScriptableObjects/InteractData", order = 1)]
public class InteractData : ScriptableObject
{
    public Transform lookAt;
    public Vector3 offset;
}
