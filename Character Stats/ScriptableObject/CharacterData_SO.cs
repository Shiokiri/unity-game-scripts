using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Character Stats/Data")]
public class CharacterData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int maxHealth;
    public int currentHealth;
    public int maxBulletsNumber;
    public int currentBulletsNumber;
	public int maxDefence;
	public int currentDefence;

	[Header("Kill")]
	public int killPoint;

	[Header("Level")]
	public int currentLevel;
	public int maxLevel;
	public int baseExp;
	public int currentExp;
	public float levelBuff;

	public string key;
	public string trueKey;

	public float LevelMultiplier
	{
		get { return 1 + (currentLevel - 1) * levelBuff; }
	}
	public void UpdateExp(int point)
	{
		Debug.Log("Update exp");
		currentExp += point;
		if (currentExp >= baseExp)
		{
			LevelUp();
		}
	}

	private void LevelUp()
	{
		currentLevel = Mathf.Clamp(currentLevel + 1, 0, maxLevel);
		baseExp += (int)(baseExp * LevelMultiplier);

		maxHealth = (int)(maxHealth * LevelMultiplier);
		currentHealth = maxHealth;

		maxDefence = (int)(maxDefence + currentLevel - 1);
		currentDefence = maxDefence;

		maxBulletsNumber = (int)(maxBulletsNumber * LevelMultiplier);
		currentBulletsNumber = maxBulletsNumber;
	}
}
