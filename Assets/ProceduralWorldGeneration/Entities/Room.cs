using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public int width;

    public int height;

    public int X;

    public int Y;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public DoorC leftDoor;

    public DoorC rightDoor;

    public DoorC topDoor;

    public DoorC bottomDoor;

    public List<DoorC> doors = new List<DoorC>();


    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("You pressed play in the wrong scene");
            return;
        }

        DoorC[] ds = GetComponentsInChildren<DoorC>();
        foreach(DoorC d in ds)
        {
            doors.Add(d);
            switch(d.doorType)
            {
                case DoorC.DoorType.right:
                    rightDoor = d; 
                    break;
                case DoorC.DoorType.left:
                    leftDoor = d;
                    break;
                case DoorC.DoorType.top:
                    topDoor = d;
                    break;
                case DoorC.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }
        

        RoomController.instance.RegisterRoom(this);
    }

    void Update()
    {
        if(name.Contains("end") && !updatedDoors)
        {
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }



    public void RemoveUnconnectedDoors()
    {
        foreach(DoorC door in doors)
        {
            switch (door.doorType)
            {
                case DoorC.DoorType.right:
                    if(GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorC.DoorType.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorC.DoorType.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case DoorC.DoorType.bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }

    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }

    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }

    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * width, Y * height);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
