using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "Loot Table", menuName = "Scriptable Objects/New Loot Table" )]
public class LootTableScriptableObject : ScriptableObject
{
	public new string name;

	public List<Loot> dropableLoot = new List<Loot>();

	public Loot GetRandomLoot()
	{
		return dropableLoot[Random.Range( 0, dropableLoot.Count )];
	}

	public List<Loot> GetRandomLoot( int amount )
	{
		List<Loot> droppedLootList = new List<Loot>();

		for( int i = 0; i < amount; i++ )
		{
			int randLootIndex = Random.Range( 0, dropableLoot.Count );
			Loot droppedLoot = dropableLoot[randLootIndex];

			droppedLootList.Add( droppedLoot );
		}

		return droppedLootList;
	}
}
