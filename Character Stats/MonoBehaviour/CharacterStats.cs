using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public event Action<int, int> UpdateHealthBarOnAttack;
	public CharacterData_SO templateData;
	public CharacterData_SO characterData;
	public AttackData_SO attackData;

	[HideInInspector]
	public bool isCritical;

	private void Awake()
	{
		if (templateData != null)
			characterData = Instantiate(templateData);
	}

	#region Read from Data_so
	public int MaxHealth
    {
        get { if (characterData != null) return characterData.maxHealth; else return 0; } 
        set { characterData.maxHealth = value; }
    }

    public int CurrentHealth
    {
        get { if (characterData != null) return characterData.currentHealth; else return 0; }
        set { characterData.currentHealth = value; }
    }
    public int MaxBulletsNumber
    {
        get { if (characterData != null) return characterData.maxBulletsNumber; else return 0; }
        set { characterData.maxBulletsNumber = value; }
    }
    public int CurrentBulletsNumber
    {
        get { if (characterData != null) return characterData.currentBulletsNumber; else return 0; }
        set { characterData.currentBulletsNumber = value; }
    }
	public int MaxDefence
	{
		get { if (characterData != null) return characterData.maxDefence; else return 0; }
		set { characterData.maxDefence = value; }
	}
	public int CurrentDefence
	{
		get { if (characterData != null) return characterData.currentDefence; else return 0; }
		set { characterData.currentDefence = value; }
	}
	#endregion

	#region Character Combat

	public void TakeDamage(CharacterStats attacker, CharacterStats defender)
	{
		int damage = Mathf.Max(attacker.CurrentDamage(attacker) - defender.CurrentDefence, 1);
		defender.CurrentHealth = Mathf.Max(defender.CurrentHealth - damage, 0);
		
		if(isCritical)
		{
			defender.GetComponent<Animator>().SetTrigger("Hit");
		}
		defender.UpdateHealthBarOnAttack?.Invoke(CurrentHealth, MaxHealth);
		Debug.Log(defender.CurrentHealth);
		if(defender.CurrentHealth <= 0)
		{
			Debug.Log("Enemy die");
			attacker.characterData.UpdateExp(defender.characterData.killPoint);
		}
	}

	private int CurrentDamage(CharacterStats attacker)
	{
		float coreDamage = UnityEngine.Random.Range(attacker.attackData.minDamage, attacker.attackData.maxDamage);
		if(attacker.isCritical)
		{
			coreDamage *= attacker.attackData.criticalMultiplier;
		}
		return (int)coreDamage;
	}
	#endregion

}
