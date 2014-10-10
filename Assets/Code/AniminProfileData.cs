using UnityEngine;
using System.Collections.Generic;


public class PersistentData
{
	public static PersistentData Singleton = new PersistentData();

	static PersistentData()
	{
		
	}

	public List<InventoryItemData> Inventory = new List<InventoryItemData>();
	public AniminId PlayerAniminId;
	public AniminEvolutionStageId AniminEvolutionId;
	public const float MaxHappy = 125.0f;
	public const float MaxHungry = 100;
	public const float MaxFitness = 100;
	public const float MaxHealth = 100;
	public int ZefTokens;
	public List<AniminSubevolutionStageId> SubstagesCompleted = new List<AniminSubevolutionStageId>(); 
	public string Username;
	public System.DateTime CreatedOn;
	
	private bool audioIsOn;
	private float happy;
	private float hungry;
	private float fitness;
	private float evolution;
	private float health;
	
	public float Happy
	{
		get
		{
			return happy;
		}
		set
		{
			happy = value;
			if(happy > MaxHappy) happy = MaxHappy;
			if(happy < 0) happy = 0;
		}
	}
	public float Hungry
	{
		get
		{
			return hungry;
		}
		set
		{
			hungry = value;
			if(hungry > 100) hungry = 100;
			if(hungry < 0) hungry = 0;
		}
	}
	public float Fitness
	{
		get
		{
			return fitness;
		}
		set
		{
			fitness = value;
			if(fitness > 100) fitness = 100;
			if(fitness < 0) fitness = 0;
		}
	}
	public float Evolution
	{
		get
		{
			return evolution;
		}
		set
		{
			evolution = value;
			if(evolution > 100) evolution = 100;
			if(evolution < 0) evolution = 0;
		}
	}
	public float Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if(health > 100) health = 100;
			if(health < 0) health = 0;
		}
	}
	
	public void SetDefault(AniminId animin)
	{
		SubstagesCompleted.Clear();
		PlayerAniminId = animin;
		AniminEvolutionId = AniminEvolutionStageId.Baby;
		CreatedOn = System.DateTime.Now;
		
		Happy = MaxHappy;
		Hungry = MaxHungry;
		Fitness = MaxFitness;
		Health = MaxHealth;
		ZefTokens = 0;

		AddItemToInventory(InventoryItemId.AlmondMilk, 3);
		AddItemToInventory(InventoryItemId.Avocado, 1);
		//AddItemToInventory(InventoryItemId.Banana, 1);
		AddItemToInventory(InventoryItemId.Blueberry, 1);
		AddItemToInventory(InventoryItemId.Boombox, 1);
		AddItemToInventory(InventoryItemId.Camera, 1);
		AddItemToInventory(InventoryItemId.Carrot, 1);
		AddItemToInventory(InventoryItemId.Chips, 1);
		AddItemToInventory(InventoryItemId.Clock, 4);
		AddItemToInventory(InventoryItemId.EDM808, 1);
		AddItemToInventory(InventoryItemId.EDMJuno, 1);
		AddItemToInventory(InventoryItemId.EDMKsynth, 1);
		AddItemToInventory(InventoryItemId.FartButton, 1);
		AddItemToInventory(InventoryItemId.Lightbulb, 1);
		//AddItemToInventory(InventoryItemId.mintclock, 1);
		//AddItemToInventory(InventoryItemId.Noodles, 1);
		AddItemToInventory(InventoryItemId.paperCalendar, 1);
		AddItemToInventory(InventoryItemId.Pill, 1);
		AddItemToInventory(InventoryItemId.Plaster, 1);
		AddItemToInventory(InventoryItemId.Spinach, 1);
		AddItemToInventory(InventoryItemId.Strawberry, 1);
		AddItemToInventory(InventoryItemId.Syringe, 1);
		AddItemToInventory(InventoryItemId.Toast, 1);
		AddItemToInventory(InventoryItemId.watermelon, 1);
		AddItemToInventory(InventoryItemId.woodFrame, 1);
		AddItemToInventory(InventoryItemId.woodSword, 1);
	}

	public void AddItemToInventory(InventoryItemId id, int count)
	{
		for(int i=0;i<Inventory.Count;++i)
		{
			if(Inventory[i].Id == id)
			{
				Inventory[i].Count += count;
				return;
			}
		}

		Inventory.Add(new InventoryItemData() { Id = id, Count = count });
	}

	public bool HasItem(InventoryItemId id)
	{
		for(int i=0;i<Inventory.Count;++i)
		{
			if(Inventory[i].Id == id)
			{
				return true;
			}
		}

		return false;
	}

	public InventoryItemData GetNextItemType(PopupItemType type)
	{
		for(int i=0;i<Inventory.Count;++i)
		{
			if(InventoryItemData.Items[(int)Inventory[i].Id].ItemType == type)
			{
				return Inventory[i];
			}
		}
		
		return null;
	}

	public bool RemoveItemFromInventory(InventoryItemId id, int count)
	{
		for(int i=0;i<Inventory.Count;++i)
		{
			if(Inventory[i].Id == id)
			{
				Inventory[i].Count -= count;
				if(Inventory[i].Count <= 0)
				{
					Inventory.RemoveAt(i);
					return false;
				}
				else
				{
					return true;
				}

			}
		}

		return false;
	}
	
	public void Save(SaveLoadDictionary dictionary)
	{
		dictionary.Write("Hungry", Hungry);
		dictionary.Write("Fitness", Fitness);
		dictionary.Write("Evolution", Evolution);
		dictionary.Write("AniminId", (int)PlayerAniminId);
		dictionary.Write("AniminEvolutionId", (int)AniminEvolutionId);
		dictionary.Write("ZefTokens", ZefTokens);
	}


	public void Load(SaveLoadDictionary dictionary)
	{
		dictionary.ReadFloat("Hungry", ref hungry);
		dictionary.ReadFloat("Fitness", ref fitness);
		dictionary.ReadFloat("Evolution", ref evolution);
		dictionary.ReadAniminId("AniminId", ref PlayerAniminId);
		dictionary.ReadAniminEvolutionId("AniminEvolutionId", ref AniminEvolutionId);
		dictionary.ReadInt("ZefTokens", ref ZefTokens);
	
	}
}