using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class SpawnedObjectSO : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
}
