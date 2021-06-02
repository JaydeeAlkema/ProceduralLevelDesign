using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	[SerializeField] private new string name;
	[SerializeField] private GameObject prefabObject;
	[SerializeField] private int spawnChance;
	[SerializeField] private Vector3 scale;

	public string Name { get => name; set => name = value; }
	public GameObject PrefabObject { get => prefabObject; set => prefabObject = value; }
	public int SpawnChance { get => spawnChance; set => spawnChance = value; }
	public Vector3 Scale { get => scale; set => scale =  value ; }
}
