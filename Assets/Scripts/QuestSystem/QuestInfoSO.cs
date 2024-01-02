using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Scriptable Objects/Quest Info SO", order = 1)]
public class QuestInfoSO : ScriptableObject
{
    [field: SerializeField] public string id {get; private set;}

    [Header("General")]
   
    public string displayName;

    public int levelRequirements;

    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefabs;

    [Header("Reward")]
    public int experienceReward;

    private void OnValidate() {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this);
        #endif
    }

}
