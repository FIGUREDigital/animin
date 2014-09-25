using UnityEngine;
using System.Collections.Generic;


public class PersistentData
{
	public static PersistentData Singleton = new PersistentData();

	static PersistentData()
	{
		
	}

	public AniminId PlayerAniminId;
	public AniminEvolutionStageId AniminEvolutionId;
	public const float MaxHappy = 125.0f;
	public const float MaxHungry = 100;
	public const float MaxFitness = 100;
	public const float MaxHealth = 100;
	public int ZefTokens;
	public List<AniminSubevolutionStageId> SubstagesCompleted = new List<AniminSubevolutionStageId>(); 
	public string Username;
	
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
	
	public void SetDefault()
	{
		SubstagesCompleted.Clear();
		PlayerAniminId = AniminId.Tbo;
		AniminEvolutionId = AniminEvolutionStageId.Baby;
		
		Happy = MaxHappy;
		Hungry = MaxHungry;
		Fitness = MaxFitness;
		Health = MaxHealth;
		ZefTokens = 0;
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