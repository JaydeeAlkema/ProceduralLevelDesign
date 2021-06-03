using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonGenerationPathFirst
{
	public class Prop : MonoBehaviour
	{
		[SerializeField] private new string name;
		[SerializeField] private GameObject prefabObject;
		[SerializeField] private float spawnChance;
		[SerializeField] private LootTableScriptableObject lootTable;
		[Space]
		[SerializeField] private Vector3 positionOffset;
		[SerializeField] private Vector3 rotationOffset;

		public string Name { get => name; set => name = value; }
		public GameObject PrefabObject { get => prefabObject; set => prefabObject = value; }
		public float SpawnChance { get => spawnChance; set => spawnChance = value; }
		public Vector3 RotationOffset { get => rotationOffset; set => rotationOffset = value; }
		public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }

		public void Destroy()
		{
			lootTable.GetRandomLoot();
		}
	}
}
