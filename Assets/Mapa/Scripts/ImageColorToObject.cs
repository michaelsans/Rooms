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
    public Transform[] StartingPoints;
    public int rand;
    public Collider2D col;
    private void Start()
    {
        col = gameObject.GetComponent<Collider2D>();
        //RenderRooms();
        //Testing pushs
        //TODO: colocar a entrada em uma posição aleatoria, a partir da entrada spawnar as proximas salas de uma forma que elas fiquem próximas a sala que eu
        //desejo fazer um caminho.

        rand = Random.Range(0, templates[2].RoomsToInstatiate.Length);
        var room = Instantiate(templates[2].RoomsToInstatiate[rand].gameObject);
        rand = Random.Range(0, StartingPoints.Length);
        room.transform.position = StartingPoints[rand].position;
        transform.position = room.transform.position;

        for (int i = 1; i < Bioma.TipoDaSala.Length;i++)
        {
            var control = transform.position;
            if (transform.position == control)
            {
                Sensing(col, transform.position);
            }
            for (int j = 0; j < templates.Length; j++)
            {
                if (Bioma.TipoDaSala[i] == templates[j].RoomID && Bioma.NumberOfConections[i] == templates[j].Conections)
                {
                    rand = Random.Range(0, templates[j].RoomsToInstatiate.Length);
                    room = Instantiate(templates[j].RoomsToInstatiate[rand].gameObject);
                    room.gameObject.name = templates[j].RoomName;
                    transform.position = room.transform.position;
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


    private Vector3 Sensing(Collider2D collision, Vector3 currentPoint)
    {
        Debug.Log("tentou");
        rand = Random.Range(0, 8);
        if (rand <= 2)
        {
            transform.position += new Vector3(50, 0);
            if (collision.tag == "BuildSite")
                return transform.position;
            else
            {
                transform.position = currentPoint;
                return transform.position;
            }

        }
        else if(rand > 2 && rand < 4)
        {
           transform.position += new Vector3(-50, 0);
            if (collision.tag == "BuildSite")
                return transform.position;
            else
            {
                transform.position = currentPoint;
                return transform.position;
            }
        }
        else if( rand > 4 && rand < 6 )
        {
            transform.position += new Vector3(0, 50);
            if (collision.tag == "BuildSite")
                return transform.position;
            else
            {
                transform.position = currentPoint;
                return transform.position;
            }
        }
        else
        {
            transform.position += new Vector3(0, -50);
            if (collision.tag == "BuildSite")
                return transform.position;
            else
            {
                transform.position = currentPoint;
                return transform.position;
            }
        }

    }

}
