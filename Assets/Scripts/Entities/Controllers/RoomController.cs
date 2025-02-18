using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "FirstLevel";

    public RoomInfo currentLoadRoomData;

    Room currRoom;

    public Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();


    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;

    bool spawnedBossRoom = false;

    bool spawnedShopRoom = false;

    bool updatedRooms = false;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
        loadedRooms = new List<Room>();
        loadRoomQueue = new Queue<RoomInfo>();
        currentLoadRoomData = null;
    }

    private void Start()
    {
    }

    private void Update()
    {
        UpdateRoomQueue();
    }


    void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            else if (!spawnedShopRoom)
            {
                StartCoroutine(SpawnShopRoom());
            }
            else if(spawnedBossRoom && spawnedShopRoom && !updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                UpdateRooms();
                updatedRooms = true;
                FindAnyObjectByType<NavegationBake>().BakeNavMesh();
                Player.IsLoading = false;
                Time.timeScale = 1;
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnShopRoom()
    {
        spawnedShopRoom = true;
        yield return new WaitForSeconds(0.7f);
        if (loadRoomQueue.Count == 0)
        {
            Room shopRoom = loadedRooms[(int)(loadedRooms.Count/2)];
            Room tempRoom = new Room(shopRoom.X, shopRoom.Y);
            Destroy(shopRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Shop", tempRoom.X, tempRoom.Y);
        }
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0)
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }

    public void LoadRoom(string name, int x, int y)
    {
        if (DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (loadRoom.isDone == false)
        {
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.width,
                currentLoadRoomData.Y * room.height,
                0
            );

            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentWorldName + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }
    
    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoomName()
    {
        string[] possibleRooms = new string[]
        {
            "Empty",
            "Basic"
        };

        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;

        UpdateRooms();
    }

    private void UpdateRooms()
    {
        foreach (Room room in loadedRooms)
        {
            if(currRoom != room)
            {
                EnemyBombFSM[] enemies = room.GetComponentsInChildren<EnemyBombFSM>();
                if(enemies != null)
                {
                    foreach(EnemyBombFSM enemy in enemies)
                    {
                        enemy.notInRoom = false;
                    }
                }
            }
        }
    }
}
