using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Area", menuName = "Area")]
public class AreaTemplate : ScriptableObject
{
    public int[] TipoDaSala;
    public int[] NumberOfConections;


    //0-BossFoyer 1-Boss 2-Entrance 3-Exit 4-HUB 5-Normal 6-Shop 7-Treasure
}
