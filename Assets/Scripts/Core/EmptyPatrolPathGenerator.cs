using RPG.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty patrol path generator", menuName="ScriptableObjects/SpawnEmptyPatrolPathGenerator")]
public class EmptyPatrolPathGenerator : ScriptableObject
{
    [SerializeField] private PatrolPath prefab;

    public PatrolPath CreateEmptyPatrolPath(Vector3 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}
