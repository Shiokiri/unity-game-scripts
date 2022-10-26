using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterStats characterStats;
	private bool isDead;

    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }

	private void OnEnable()
	{
		GameManager.Instance.RegisterPlayer(characterStats);
	}

	private void Start()
    {
        
		SaveManager.Instance.LoadPlayerData();
    }

    private void Update()
    {
		isDead = characterStats.CurrentHealth == 0;
		if(isDead)
		{
			GameManager.Instance.NotifyObservers();
		}
    }

}
