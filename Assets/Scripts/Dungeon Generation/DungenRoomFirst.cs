using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungenRoomFirst : MonoBehaviour
{

	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private bool randomizeSeed;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private string seed;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private Vector2Int dungeonSize = Vector2Int.zero;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] [Range( 1, 30 )] private int amountOfRooms = 2;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private int amountOfTries = 1000;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private Vector2Int minRoomSize;
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] private Vector2Int maxRoomSize;
	[Space]
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] List<GameObject> groundTileObjects;
	[Space]
	[BoxGroup( "Dungeon Generation Settings" )] [SerializeField] Transform roomsParent;

	[BoxGroup( "Dungeon Data" )] [SerializeField] private List<Room> roomsInDungeon = new List<Room>();
	[BoxGroup( "Dungeon Data" )] [SerializeField] private List<Tile> tilesInDungeon = new List<Tile>();

	private DateTime startTime; // At which time we started generating the dungeon.
	private int roomIndex = 0;
	private int totalTries = 0;

	private void Start()
	{
		Generate();
	}

	public void Generate()
	{
		startTime = DateTime.Now;

		if( randomizeSeed ) seed = Random.Range( 0, int.MaxValue ).ToString();
		Random.InitState( seed.GetHashCode() );

		roomIndex = 0;

		ClearDungeon();
		while( totalTries < amountOfTries && roomsInDungeon.Count < amountOfRooms )
		{
			SpawnRoomWithingRegion();
		}
		//GetIntersectingRooms();
		//do
		//{
		//	SeparateRooms();
		//} while( intersectingRooms.Count > 0 );

		Debug.Log( "Dungeon Generation Took: " + ( DateTime.Now - startTime ).Milliseconds + "ms" );
	}

	public void ClearDungeon()
	{
		// Destroy all Tiles in the scene.
		if( tilesInDungeon.Count > 0 )
		{
			for( int t = 0; t < tilesInDungeon.Count; t++ )
			{
				Tile tile = tilesInDungeon[t];

				if( tile != null ) DestroyImmediate( tile.gameObject );
			}
		}

		// Destroy all Room in the scene.
		if( roomsInDungeon.Count > 0 )
		{
			for( int r = 0; r < roomsInDungeon.Count; r++ )
			{
				Room room = roomsInDungeon[r];

				if( room != null ) DestroyImmediate( room.gameObject );
			}
		}

		tilesInDungeon.Clear();
		roomsInDungeon.Clear();
		totalTries = 0;
		roomIndex = 0;
	}

	private void SpawnRoomWithingRegion()
	{
		Vector2Int roomSize = new Vector2Int( Random.Range( minRoomSize.x, maxRoomSize.x ), Random.Range( minRoomSize.y, maxRoomSize.y ) );
		Vector2Int roomStartCoordinates = new Vector2Int( Random.Range( -dungeonSize.x / 2 + roomSize.x / 2, dungeonSize.x / 2 - roomSize.x / 2 ), Random.Range( -dungeonSize.y / 2 + roomSize.y / 2, dungeonSize.y / 2 - roomSize.y / 2 ) );

		// Force the width and height to be an Odd number
		if( roomSize.x % 2 == 0 )
			roomSize.x -= 1;
		if( roomSize.y % 2 == 0 )
			roomSize.y -= 1;

		GameObject newRoomGO = new GameObject();
		Room room = newRoomGO.AddComponent<Room>();

		room.name = room.name;
		room.Coordinates = roomStartCoordinates;
		room.ID = roomIndex;
		room.Size = roomSize;

		newRoomGO.name = room.Name;
		newRoomGO.transform.parent = roomsParent;
		newRoomGO.transform.position = new Vector3( roomStartCoordinates.x, 0, roomStartCoordinates.y );

		// Check if the new Room doesn't overlap any existing rooms, if so, stop, Destroy and try again.
		for( int r1 = 0; r1 < roomsInDungeon.Count; r1++ )
		{
			for( int r2 = 0; r2 < roomsInDungeon.Count; r2++ )
			{
				Room room1 = room;
				Room room2 = roomsInDungeon[r2];
				if( room1 != room2 )
				{
					if( room1.Coordinates.x + room1.Size.x / 2 >= room2.Coordinates.x - room2.Size.x / 2 &&
						room1.Coordinates.x - room1.Size.x / 2 <= room2.Coordinates.x + room2.Size.x / 2 &&
						room1.Coordinates.y + room1.Size.y / 2 >= room2.Coordinates.y - room2.Size.y / 2 &&
						room1.Coordinates.y - room1.Size.y / 2 <= room2.Coordinates.y + room2.Size.y / 2 )
					{
						DestroyImmediate( newRoomGO );
						totalTries++;
						return;
					}
				}
			}
		}

		// Fill room with tiles.
		for( int x = 0; x < roomSize.x; x++ )
		{
			for( int y = 0; y < roomSize.y; y++ )
			{
				int graphicIndex = Random.Range( 0, groundTileObjects.Count );

				GameObject newTileGO = Instantiate( groundTileObjects[graphicIndex], newRoomGO.transform.parent );
				Tile tile = newTileGO.AddComponent<Tile>();

				tile.Coordinates = new Vector2Int( roomStartCoordinates.x - ( roomSize.x / 2 ) + x, roomStartCoordinates.y - ( roomSize.y / 2 ) + y );
				tile.ParentRoom = room;
				tile.Type = TileType.GROUND;

				newTileGO.transform.position = new Vector3( tile.Coordinates.x, 0, tile.Coordinates.y );
				newTileGO.transform.name = tile.name;
				newTileGO.transform.parent = newRoomGO.transform;

				room.AddTile( tile );

				tilesInDungeon.Add( tile );
			}
		}

		roomIndex++;
		roomsInDungeon.Add( room );
	}

	//private void GetIntersectingRooms()
	//{
	//	for( int r1 = 0; r1 < roomsIndungeon.Count; r1++ )
	//	{
	//		for( int r2 = 0; r2 < roomsIndungeon.Count; r2++ )
	//		{
	//			Room room1 = roomsIndungeon[r1];
	//			Room room2 = roomsIndungeon[r2];
	//			if( room1 != room2 )
	//			{
	//				if( room1.Coordinates.x + room1.Size.x / 2 >= room2.Coordinates.x - room2.Size.x / 2 &&
	//					room1.Coordinates.x - room1.Size.x / 2 <= room2.Coordinates.x + room2.Size.x / 2 &&
	//					room1.Coordinates.y + room1.Size.y / 2 >= room2.Coordinates.y - room2.Size.y / 2 &&
	//					room1.Coordinates.y - room1.Size.y / 2 <= room2.Coordinates.y + room2.Size.y / 2 )
	//				{

	//					if( !intersectingRooms.Contains( room1 ) ) intersectingRooms.Add( room1 );
	//					if( !intersectingRooms.Contains( room2 ) ) intersectingRooms.Add( room2 );

	//					Debug.Log( string.Format( "{0} and {1} are intersecting!", room1.name, room2.name ) );
	//				}
	//			}
	//		}
	//	}
	//}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube( transform.position, new Vector3( dungeonSize.x, 1, dungeonSize.y ) );
	}
}

public enum TileType
{
	GROUND,     // A normal ground tile. (surrounded by other tiles)
	WALL,       // Wall Tile. (Missing Neighbour on one side)
	CORNER,     // Corner Tile. (Missing Neighbour on atleast 2 sides)
	DOOR        // Door Tile. (Missing Corner neighbours)
}

public enum RoomType
{
	SPAWN,          // Player Spawn Room. (Always only 1 per level)
	EXIT,           // Player Exit Room. (Always only 1 per level)
	ENEMYSPAWN,     // Enemy Spawn Room. Most commong room type
	SHOP,           // Shop where player can buy/sell items. 30% of spawning per level.
	BOSS,           // Boss room, always present and always in the way of the exit.
	TREASURE,       // Treasure rooms are rare, but grant some nice loot.
	HUB,            // A hub room is nothing more than a room that connects to other rooms. See it as a place to breath and regen.
	OBJECTIVE       // Objective rooms are rooms that hold the item(s) required to finish the objective.
}

[System.Serializable]
public class Tile : MonoBehaviour
{
	[SerializeField] private new string name;
	[SerializeField] private TileType type = TileType.GROUND;
	[SerializeField] private Vector2Int coordinates = Vector2Int.zero;
	[SerializeField] private Room parentRoom = default;

	public string Name { get => string.Format( "Tile [{0}] [{1}]", coordinates.x, coordinates.y ); private set => name = value; }
	public TileType Type { get => type; set => type = value; }
	public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }
	public Room ParentRoom { get => parentRoom; set => parentRoom = value; }
}

[System.Serializable]
public class Room : MonoBehaviour
{
	[SerializeField] private new string name;
	[SerializeField] private RoomType type = RoomType.HUB;
	[SerializeField] private Vector2Int coordinates = Vector2Int.zero;
	[SerializeField] private int iD = 0;
	[SerializeField] private List<Tile> tilesInRoom = new List<Tile>();
	[SerializeField] private Vector2Int size = Vector2Int.zero;

	public string Name { get => string.Format( "Room [{0}]", ID ); private set => name = value; }
	public int ID { get => iD; set => iD = value; }
	public Vector2Int Size { get => size; set => size = value; }
	public Vector2Int Coordinates { get => coordinates; set => coordinates = value; }

	/// <summary>
	/// Add Tile to tiles in room list.
	/// </summary>
	/// <param name="tile"> Tile to Add. </param>
	public void AddTile( Tile tile )
	{
		tilesInRoom.Add( tile );
	}
	/// <summary>
	/// Removes a tile from the tiles in room list.
	/// </summary>
	/// <param name="tile"> Tile to remove. </param>
	/// <param name="destroyOnRemove"> Should the tile also be destroyed on removal. </param>
	public void RemoveTile( Tile tile, bool destroyOnRemove )
	{
		Tile _tile = tile; ;
		tilesInRoom.Remove( _tile );
		if( destroyOnRemove ) Destroy( _tile.gameObject );
	}
	/// <summary>
	/// Removes a tile from the tiles in room list.
	/// </summary>
	/// <param name="index"> Index of Tile to remove. </param>
	/// <param name="destroyOnRemove"> Should the tile also be destroyed on removal. </param>
	public void RemoveTile( int index, bool destroyOnRemove )
	{
		Tile _tile = tilesInRoom[index]; ;
		tilesInRoom.RemoveAt( index );
		if( destroyOnRemove ) Destroy( _tile.gameObject );
	}
}
