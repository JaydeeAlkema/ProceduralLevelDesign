using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DungeonGenerationPathFirst;

[CreateAssetMenu( fileName = "Dungen Settings", menuName = "Scriptable Objects/ New Dungen Settings" )]
public class DungenSettingsScriptableObjects : ScriptableObject
{
	[BoxGroup( "Generation Settings" )] public bool randomizeSeed = false;               // Determines if the seed should be randomized each time.
	[BoxGroup( "Generation Settings" )] public string seed = "";

	[BoxGroup( "Generation Settings" )] public Vector2Int minRoomSize = Vector2Int.zero;  // Min Room size.
	[BoxGroup( "Generation Settings" )] public Vector2Int maxRoomSize = Vector2Int.zero;  // Max Room size.

	[BoxGroup( "Generation Settings" )] public int minPathwayCount = 15;                  // Minimum amount of pathways that will be generated.
	[BoxGroup( "Generation Settings" )] public int maxPathwayCount = 30;                  // Maximum amount of pathways that will be generated.
	[BoxGroup( "Generation Settings" )] public int minPathwayLength = 10;                 // Minimum length of the pathway before making a turn.
	[BoxGroup( "Generation Settings" )] public int maxPathwayLength = 20;                 // Maximum length of the pathway before making a turn.

	[BoxGroup( "Tile Objects" )] public int tileSize = 1;                                 // Tile Size.
	[BoxGroup( "Tile Objects" )] public List<GameObject> tileGroundObjects = new List<GameObject>();              // Tile Ground Objects.
	[BoxGroup( "Tile Objects" )] public List<GameObject> tileWallObjects = new List<GameObject>();                // Tile Wall Left Objects.
	[BoxGroup( "Tile Objects" )] public List<GameObject> tileOuterCornerObjects = new List<GameObject>();         // Tile Outer Corner Left Sprite
	[BoxGroup( "Tile Objects" )] public List<GameObject> tileInnerCornerObjects = new List<GameObject>();         // Tile Inner Corner Left Sprite

	[BoxGroup( "Enemy Settings" )] public List<EnemyList> enemyLists = new List<EnemyList>();     // List with Enemy Lists. Within these lists are the enemies that can be spawned per theme.
	[BoxGroup( "Enemy Settings" )] public int spawnChance = 1;                                    // How much percentage chance there is to spawn an enemy.

	[BoxGroup( "Dungeon Details" )] public int propsAmount;                                    // How many props will be spawned within the dungeon.
	[BoxGroup( "Dungeon Details" )] public List<Prop> spawnableProps = new List<Prop>();       // List with all the spawnable props within the dungeon.
	[BoxGroup( "Dungeon Details" )] public List<Trap> spawnableTraps = new List<Trap>();      // List with all the spawnable traps within the dungeon.
}

[System.Serializable]
public struct EnemyList
{
	[SerializeField] private string name;
	[SerializeField] private List<GameObject> enemies;

	public List<GameObject> Enemies { get => enemies; set => enemies = value; }
	public string Name { get => name; set => name = value; }
}