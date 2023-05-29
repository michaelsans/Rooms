using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Room", menuName = "Room")]
public class RoomTemplate : ScriptableObject
{
    public string RoomName;
    public int Conections;
    public int[] ConectionType;
    public int RoomID;

    public GameObject[] RoomsToInstatiate;
}
