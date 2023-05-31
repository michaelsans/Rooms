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
    public GameObject Sala;
    public Transform[] SalaParaGO;
    public RoomTemplate[] templates;
    public Transform[] StartingPoints;
    private int rand;
    public LayerMask whatIsGround;
    public LayerMask whatIsRoom;
    private void Start()
    {
        //TODO: colocar a entrada em uma posição aleatoria, a partir da entrada spawnar as proximas salas de uma forma que elas fiquem próximas a sala que eu
        //desejo fazer um caminho.
        SpawnMap();
        StartCoroutine(WaitTime());
    }

    public bool CheckSite()
    {
        return Physics2D.OverlapCircle(transform.position, 10f, whatIsGround);//This collider will see if im on a build site
    }
    public bool CheckRoom()
    {
        return Physics2D.OverlapCircle(transform.position, 10f, whatIsRoom);//This collider will see if im on a room
    }

    public void SpawnMap()
    {
        // Começando a ficar certo, agora eu tenho que fazer um loop pelos templates, achar o tipo de sala certa e instanciar a sala
        //no momento eu estou apenas pegando uma sala de templates mas como eu tenho templates diferentes para o mesmo tipo de sala isso esta
        //errado
        rand = Random.Range(0, templates[2].RoomsToInstatiate.Length);//Pick a random room
        var room = Instantiate(templates[2].RoomsToInstatiate[rand].gameObject);//Instantiate that room
        rand = Random.Range(0, StartingPoints.Length);//Pick a random starting point
        room.transform.position = StartingPoints[rand].position;//Make that point the room position;
        transform.position = room.transform.position;//Go to room position
        room.name = templates[2].RoomName;//nomeia a sala
        room.transform.parent = Sala.transform;
    }

    IEnumerator WaitTime()
    {
        int contador = 1;
        while (contador < 16)
        {
            Debug.Log(contador);
            for (int j = 0; j < templates.Length; j++)
            {   //Esse Loop checa se estou pegando o tipo de sala certo com o numero de conexões certo

                if (Bioma.TipoDaSala[contador] == templates[j].RoomID && Bioma.NumberOfConections[contador] == templates[j].Conections)
                {
                    //Preciso que o j só avance quando gera uma sala, subtrair quando nao gera uma sala leva a problemas
                    //for loops geram loops infinitos, o que fazer?

                    rand = Random.Range(1, 8);//Choosing a random direction to walk

                    if (rand <= 2)// if rand is 1 or 2 i walk to the right
                    {
                        transform.position += new Vector3(50, 0);
                        if (CheckSite() && !CheckRoom())//Check if im on a build site, if yes instantiate a room
                        {
                            GenerateRoom(j);
                            contador++;
                            break;
                        }
                        else//go back and choose a new direction
                        {
                            transform.position += new Vector3(-50, 0);
                        }
                    }
                    if (rand > 2 && rand < 5)//if rand is 3 or 4 walk to the left
                    {
                        transform.position += new Vector3(-50, 0);
                        if (CheckSite() && !CheckRoom())//Check if im on a build site, if yes instantiate a room
                        {
                            GenerateRoom(j);
                            contador++;
                            break;
                        }
                        else//go back and choose a new direction
                        {
                            transform.position += new Vector3(+50, 0);
                        }
                    }
                    if (rand > 4 && rand < 7)// if rand is 5 or 6 walk up
                    {
                        transform.position += new Vector3(0, 50);
                        if (CheckSite() && !CheckRoom())//Check if im on a build site, if yes instantiate a room
                        {
                            GenerateRoom(j);
                            contador++;
                            break;
                        }
                        else//go back and choose a new direction
                        {
                            transform.position += new Vector3(0, -50);
                        }
                    }
                    else// if rand is 7 or 8 walk down
                    {
                        transform.position += new Vector3(0, -50);
                        if (CheckSite() && !CheckRoom())//Check if im on a build site, if yes instantiate a room
                        {
                            GenerateRoom(j);
                            contador++;
                            break;
                        }
                        else//go back and choose a new direction
                        {
                            transform.position += new Vector3(0, +50);
                        }
                    }
                }
                yield return new WaitForSeconds(.0001f);
            }
        }
        Debug.Log("terminou");
        RenderRooms();
    }

    private void GenerateRoom(int n)
    {
        rand = Random.Range(0, templates[n].RoomsToInstatiate.Length);//Choose a random room of the right type
        var room = Instantiate(templates[n].RoomsToInstatiate[rand].gameObject);//Instantiate that room
        room.transform.position = transform.position;
        room.name = templates[n].RoomName;
        room.transform.parent = Sala.transform;
    }

    public void InstatiateObjectByPixelColor(Color color, Vector3 position, GameObject parent)
    {
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            if (ObjectColor[i].gamma == color.gamma)
            {
                var obj = Instantiate(objectPrefabs[i], position, Quaternion.identity);
                obj.transform.parent = parent.transform;
            }
        }
    }

    public void RenderRooms()
    {
        Debug.Log("começou");
        SalaParaGO = Sala.GetComponentsInChildren<Transform>();

        for (int a = 1; a < SalaParaGO.Length; a++)
        {
            sprite = SalaParaGO[a].GetComponent<SpriteRenderer>().sprite;
            for (int i = 0; i < sprite.rect.size.x; i++)
            {
                for (int j = 0; j < sprite.rect.size.y; j++)
                {
                    Color pixelcolor = sprite.texture.GetPixel(i, j);

                    InstatiateObjectByPixelColor(pixelcolor, new Vector3(SalaParaGO[a].transform.position.x + i, SalaParaGO[a].transform.position.y + j), Sala);
                    Destroy(SalaParaGO[a].gameObject);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
