using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ImageColorToObject : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    public Color[] ObjectColor;
    public AreaTemplate Bioma;
    public Sprite sprite;
    public GameObject agent;
    public List<Transform> Portas = new List<Transform>();
    public List<GameObject> agentsList = new List<GameObject>();
    public Transform[] obj;
    public RoomTemplate[] templates;
    public Transform[] SummonPoints;
    public int rand;
    private void Start()
    {
        //RenderRooms();

        //TODO: colocar a entrada em uma posição aleatoria, a partir da entrada spawnar as proximas salas de uma forma que elas fiquem próximas a sala que eu
        //desejo fazer um caminho.

        for (int i = 0; i < Bioma.TipoDaSala.Length; i++)
        {
            for (int j = 0; j < templates.Length; j++)
            {
                if (Bioma.TipoDaSala[i] == templates[j].RoomID && Bioma.NumberOfConections[i] == templates[j].Conections)
                {
                    rand = Random.Range(0, templates[j].RoomsToInstatiate.Length);
                    var room = Instantiate(templates[j].RoomsToInstatiate[rand].gameObject);
                    room.gameObject.name = templates[j].RoomName;
                    rand = Random.Range(0, SummonPoints.Length);
                    room.transform.position = SummonPoints[rand].position;
                }
                //if(obj[j].tag == "Porta")
                //{
                //    Portas.Add(obj[j].transform);
                //    var agent0 = Instantiate(agent, obj[j].transform.position, Quaternion.identity, obj[j].parent);
                //    agent0.transform.localScale = new Vector3(0.03f, 0.03f);
                //    agentsList.Add(agent0);
                //}
            }
        }

        //for (int i = 0; i < Portas.Count - 1; i++)
        //{
        //    var aiDestSetter = agentsList[i].GetComponent<AIDestinationSetter>();
        //    aiDestSetter.target = Portas[i + 1].transform;
        //}
    }

    //public void InstatiateObjectByPixelColor(Color color, Vector3 position, GameObject parent)
    //{
    //    for (int i = 0; i < objectPrefabs.Length; i++)
    //    {
    //        if (ObjectColor[i].gamma == color.gamma)
    //        {
    //            var obj = Instantiate(objectPrefabs[i], position, Quaternion.identity);
    //            obj.transform.parent = parent.transform;
    //        }
    //    }
    //}

    //public void RenderRooms()
    //{
    //    for (int a = 0; a < Regulador.Length; a++)
    //    {
    //        sprite = Regulador[a].GetComponent<SpriteRenderer>().sprite;
    //        for (int i = 0; i < sprite.rect.size.x; i++)
    //        {
    //            for (int j = 0; j < sprite.rect.size.y; j++)
    //            {
    //                Color pixelcolor = sprite.texture.GetPixel(i, j);

    //                InstatiateObjectByPixelColor(pixelcolor, new Vector3(Regulador[a].transform.position.x +i, Regulador[a].transform.position.y+j), Regulador[a]);
    //            }
    //        }
    //    }
    //}

}
