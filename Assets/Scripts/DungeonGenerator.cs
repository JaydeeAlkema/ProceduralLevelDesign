using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class DungeonGenerator : MonoBehaviour
{
	[Foldout( "Dungeon Parameters" )] [SerializeField] private string seed;
	[Foldout( "Dungeon Parameters" )] [SerializeField] private Vector2Int DungeonGenerationZone;
	[Foldout( "Dungeon Parameters" )] [SerializeField] private Vector2Int minMaxRoomWidth;
	[Foldout( "Dungeon Parameters" )] [SerializeField] private Vector2Int minMaxRoomHeight;
	[Foldout( "Dungeon Parameters" )] [SerializeField] private Vector2Int minMaxRoomAmount;

	[Foldout( "Dungeon Tiles Components" )] [SerializeField] private Mesh tileMesh;
	[Foldout( "Dungeon Tiles Components" )] [SerializeField] private Material tileMeshMaterial;

	private int roomCount = 0;

	private void Awake()
	{
		if( seed == "" ) seed = Random.Range( 0, int.MaxValue ).ToString();

		Random.InitState( seed.GetHashCode() );
	}

	private void Start()
	{
		GenerateRoomsWithincCircle();
	}

	/// <summary>
	/// Generates rooms withing a unit circle.
	/// </summary>
	private void GenerateRoomsWithincCircle()
	{
		int AmountOfRoomsToGenerate = Random.Range( minMaxRoomAmount.x, minMaxRoomAmount.y );

		for( int i = 0; i < AmountOfRoomsToGenerate; i++ )
		{
			Vector2Int coords = new Vector2Int( Random.Range( -DungeonGenerationZone.x, DungeonGenerationZone.x ), Random.Range( -DungeonGenerationZone.y, DungeonGenerationZone.y ) );
			Vector2Int size = new Vector2Int( Random.Range( minMaxRoomWidth.x, minMaxRoomWidth.y ), Random.Range( minMaxRoomHeight.x, minMaxRoomHeight.y ) );

			CreateRoom( new Vector2Int( ( int )coords.x, ( int )coords.y ), size );
		}
	}

	/// <summary>
	/// Creates a room.
	/// </summary>
	/// <param name="coordinates"> Room Coordinates. </param>
	/// <param name="size"> Room Size. </param>
	private void CreateRoom( Vector2Int coordinates, Vector2Int size )
	{
		// Create a new room
		GameObject roomGO = new GameObject();

		Room room = roomGO.AddComponent<Room>();
		room.name = "Room [" + roomCount + "]";
		room.Coordinates = coordinates;
		room.Size = size;

		roomGO.name = room.name;
		roomGO.transform.position = new Vector3( coordinates.x, 0, coordinates.y );

		// Fill the room with tiles.
		for( int y = 0; y < size.y; y++ )
		{
			for( int x = 0; x < size.x; x++ )
			{
				CreateTile( new Vector2Int( coordinates.x + x - ( room.Size.x / 2 ), coordinates.y + y - ( room.Size.y / 2 ) ), roomGO, room, "Tile [" + x + "] " + "[" + y + "]" );
			}
		}

		roomCount++;
	}

	/// <summary>
	/// Create a Tile.
	/// </summary>
	/// <param name="coordinates"> Tile Coordinates. </param>
	/// <param name="parentGO"> Parent of the tile. </param>
	/// <param name="room"> Room Reference. </param>
	private void CreateTile( Vector2Int coordinates, GameObject parentGO, Room room, string name )
	{
		GameObject TileGO = new GameObject();

		MeshFilter meshFilter = TileGO.AddComponent<MeshFilter>();
		meshFilter.mesh = tileMesh;

		MeshRenderer meshRenderer = TileGO.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = tileMeshMaterial;

		Tile tile = TileGO.AddComponent<Tile>();
		tile.name = name;
		tile.Coordinates = new Vector2Int( coordinates.x, coordinates.y );

		TileGO.name = tile.name;
		TileGO.transform.position = new Vector3( coordinates.x, 0, coordinates.y );
		TileGO.transform.parent = parentGO.transform;

		room.Tiles.Add( tile );
	}
}

[System.Serializable]
public class Tile : MonoBehaviour
{
	public new string name;
	[SerializeField] private Vector2Int coordinates;

	public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
}

[System.Serializable]
public class Room : MonoBehaviour
{
	public new string name;
	[SerializeField] private Vector2Int coordinates;
	[SerializeField] private Vector2Int size;
	[SerializeField] private List<Tile> tiles = new List<Tile>();
	[SerializeField] private List<Transform> enemies = new List<Transform>();

	public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
	public Vector2Int Size { get => size; set => size = value; }
	public List<Tile> Tiles { get => tiles; set => tiles = value; }
	public List<Transform> Enemies { get => enemies; set => enemies = value; }
}
