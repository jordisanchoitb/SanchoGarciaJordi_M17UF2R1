using System.Collections.Generic;
using UnityEngine;

public class RoomGenerationManager : MonoBehaviour
{
    public GameObject tilePrefab; // Prefab para los tiles de suelo
    public int numberOfRooms = 10; // Total de salas a generar
    public Vector2Int roomSizeRange = new Vector2Int(5, 10); // Dimensiones mínimas y máximas de las salas (en tiles)
    public Vector2Int gridSize = new Vector2Int(50, 50); // Tamaño de la cuadrícula general

    private List<Room> rooms = new List<Room>();
    private HashSet<Vector2Int> occupiedPositions = new HashSet<Vector2Int>();

    void Start()
    {
        GenerateRooms();
        ConnectRooms();
    }

    void GenerateRooms()
    {
        for (int i = 0; i < numberOfRooms; i++)
        {
            Vector2Int position;

            // Evitar posiciones ocupadas
            do
            {
                position = new Vector2Int(
                    Random.Range(0, gridSize.x),
                    Random.Range(0, gridSize.y)
                );
            } while (occupiedPositions.Contains(position));

            int width = Random.Range(roomSizeRange.x, roomSizeRange.y);
            int height = Random.Range(roomSizeRange.x, roomSizeRange.y);

            CreateRoom(position, width, height);

            occupiedPositions.Add(position);
        }

        // Asignar roles especiales
        AssignSpecialRooms();
    }

    void CreateRoom(Vector2Int position, int width, int height)
    {
        Room room = new Room(position, width, height);
        rooms.Add(room);

        // Generar los tiles de la sala
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = new Vector3(position.x + x, position.y + y, 0);
                Instantiate(tilePrefab, tilePosition, Quaternion.identity);
            }
        }
    }

    void ConnectRooms()
    {
        for (int i = 0; i < rooms.Count - 1; i++)
        {
            CreatePath(rooms[i], rooms[i + 1]);
        }
    }

    void CreatePath(Room roomA, Room roomB)
    {
        Vector2Int start = roomA.Center;
        Vector2Int end = roomB.Center;

        Vector2Int current = start;

        while (current != end)
        {
            if (current.x != end.x)
                current.x += current.x < end.x ? 1 : -1;
            else if (current.y != end.y)
                current.y += current.y < end.y ? 1 : -1;

            Vector3 tilePosition = new Vector3(current.x, current.y, 0);
            Instantiate(tilePrefab, tilePosition, Quaternion.identity);
        }
    }

    void AssignSpecialRooms()
    {
        // Asignar una sala para la tienda
        Room shopRoom = rooms[Random.Range(0, rooms.Count)];
        shopRoom.IsShop = true;

        // Asignar otra sala para la puerta especial
        Room specialRoom;
        do
        {
            specialRoom = rooms[Random.Range(0, rooms.Count)];
        } while (specialRoom == shopRoom);

        specialRoom.IsSpecial = true;
    }
}
