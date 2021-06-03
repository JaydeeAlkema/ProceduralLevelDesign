using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

/// <summary>
/// This is my imagination of a possible loot item.
/// Lots of things are said in the comments below that are not pressent within the game, but this should provide a image of how it could have been.
/// </summary>

public enum LootRarity
{
	TRASH,          // Like the rarity says, pure trash.
	COMMON,         // Common loot. Drop Chance: 90%
	UNCOMMON,       // Uncommon loot has improved stats. Drop Chance: 50%
	RARE,           // Rare loot has greatly improved stats. Drop Chance: 10%
	EPIC,           // Epic loot has massivly improved stats and a small chance for a extra ability. Drop Chance: 5%
	LEGENDARY,      // Legendary loot has highest possible stats and a good chance for 1-2 extra stats. Drop Chance: 1%
	MYTHIC,         // Mythic loot has highest possible stats and a guaranteed chance for 2-4 extra stats. Drop Chance: 0.1%
	UNIQUE          // Unique loot has highest possible stats and a guaranteed chance for 4-6 extra stats and a Unique ability. Drop Chance: 0.001%
}

public class Loot : MonoBehaviour
{
	public new string name;
	public GameObject prefabObject;
	public int itemLevel;               // the level of the item is not connected to the level of the player. Max 100
	public int requiredLevel;           // Required player level to equip.
	[ResizableTextArea] public string itemDescription;          // Description of the item.
	public List<Stat> stats = new List<Stat>(); // List off all the item stats.
}

public enum StatType
{
	Inteligence,
	Strength,
	Stamina,
	Mastery,
	Versitality,
	Dodge,
	CritialHitDamage,
	CriticalHitChance
}

[System.Serializable]
public struct Stat
{
	public StatType type;
	public int amount;
}