using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObject", menuName = "ISpy/ObjectData")]
public class ObjectData : ScriptableObject
{
    [Header("Visuals")]
    public List<GameObject> objectPrefabs;
    public GameObject objectVFX;
    public AudioClip selectClip;
    public Color outlineColor;

    [Header("Object Data")]
    public string objectName;
    public string objectDescription;
    public List<string> objectHints;
    public float rewardPoints;
}
