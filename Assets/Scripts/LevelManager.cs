using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level", order = 0)]
public class LevelManager : ScriptableObject
{
    public Chunk[] chunks;
}
