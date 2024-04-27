using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    // 0 --> Bottom
    // 1 --> Top
    // 2 --> Left
    // 3 --> Right

    private int bottom = 0;
    private int top = 1;
    private int left = 2;
    private int right = 3;

    public static int rooms = 0;
    public static int maxOpenRooms = 4;

    private RoomTemplates roomTemplates;
    private int index;
    private bool spawned = false;

    private void Start()
    {
        roomTemplates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {
            if (openingDirection == bottom)
            {
                //Spawn a room with a bottom door 
                if (rooms >= maxOpenRooms - 1)
                {
                    index = bottom;
                }
                else
                {
                    index = Random.Range(0, roomTemplates.bottomRooms.Length);
                }

                Instantiate(roomTemplates.bottomRooms[index], transform.position, Quaternion.identity);
            }
            else if (openingDirection == top)
            {
                //Spawn a room with a top door 
                if (rooms >= maxOpenRooms - 1)
                {
                    index = top;
                }
                else
                {
                    index = Random.Range(0, roomTemplates.topRooms.Length);
                }

                Instantiate(roomTemplates.topRooms[index], transform.position, Quaternion.identity);
            }
            else if (openingDirection == left)
            {
                //Spawn a room with a left door
                if (rooms >= maxOpenRooms - 1)
                {
                    index = left;
                }
                else
                {
                    index = Random.Range(0, roomTemplates.leftRooms.Length);
                }

                Instantiate(roomTemplates.leftRooms[index], transform.position, Quaternion.identity);
            }
            else if (openingDirection == right)
            {
                //Spawn a room with a right door 
                if (rooms >= maxOpenRooms - 1)
                {
                    index = right;
                }
                else
                {
                    index = Random.Range(0, roomTemplates.rightRooms.Length);
                }

                Instantiate(roomTemplates.rightRooms[index], transform.position, Quaternion.identity);
            }
            spawned = true;
            rooms++;
        }
    }

    async private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 localSpawnPointPosition = transform.position;
        Vector3 globalSpawnPointPosition = transform.TransformPoint(localSpawnPointPosition);
        if (collision.CompareTag("SpawnPoint"))
        {
            if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)

            if (globalSpawnPointPosition == new Vector3 (0, 0, 0))
            {
                Destroy(gameObject);
            }
            else if (collision.GetComponent<RoomSpawner>().spawned == false && spawned == false)
            {
                await Task.Delay(100);
                Instantiate(roomTemplates.closedRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            spawned = true;
        }
    }
}

