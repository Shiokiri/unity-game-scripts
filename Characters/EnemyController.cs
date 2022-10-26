using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyStates { GUARD, PATROL, CHASE, DEAD }
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour, IEndGameObserver
{
	private EnemyStates enemyStates;
	private NavMeshAgent agent;
	private Animator animator;
	private CharacterStats characterStats;
	private Collider enemyCollider;

	[Header("Basic Settings")]
	public float sightRadius;
	public bool isGuard;
	private float speed;
	private GameObject attackTarget;
	public float LookAtTime;
	private float remainLookAtTime;
	private float lastAttackTime;
	
	[Header("Patrol State")]
	public float patrolRange;
	private Vector3 wayPoint;
	private Vector3 guardPosition;
	private Quaternion guardRotation;

	private bool isWalk;
	private bool isChase;
	private bool isFollow;
	private bool isDead;
	private bool isPlayerDead;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		characterStats = GetComponent<CharacterStats>();
		enemyCollider = GetComponent<Collider>();
		speed = agent.speed;
		guardPosition = transform.position;
		guardRotation = transform.rotation;
		remainLookAtTime = LookAtTime;
	}

	private void Start()
	{
		if(isGuard)
		{
			enemyStates = EnemyStates.GUARD;
		}
		else
		{
			enemyStates = EnemyStates.PATROL;
			GetNewWayPoint();
		}
		GameManager.Instance.AddObserver(this);
	}

	void OnDisable()
	{
		if (!GameManager.IsInitialized) return;
		GameManager.Instance.RemovedObserver(this);
	}

	private void Update()
	{
		if(characterStats.CurrentHealth == 0)
		{
			isDead = true;
		}
		if(!isPlayerDead)
		{
			SwitchStates();
			SwitchAnimation();
			lastAttackTime -= Time.deltaTime;
		}
		
	}

	private void SwitchAnimation()
	{
		animator.SetBool("Walk", isWalk);
		animator.SetBool("Chase", isChase);
		animator.SetBool("Follow", isFollow);
		animator.SetBool("Critical", characterStats.isCritical);
		animator.SetBool("Death", isDead);
	}

	private void SwitchStates()
	{
		if(isDead)
		{
			enemyStates = EnemyStates.DEAD;
		}
		else if(FoundPlayer())
		{
			enemyStates = EnemyStates.CHASE;
		}

		switch(enemyStates)
		{
			case EnemyStates.GUARD:
				isChase = false;
				if(transform.position != guardPosition)
				{
					isWalk = true;
					agent.isStopped = false;
					agent.destination = guardPosition;
					if (Vector3.SqrMagnitude(guardPosition - transform.position) <= agent.stoppingDistance)
					{
						isWalk = false;
						transform.rotation = Quaternion.Lerp(transform.rotation, guardRotation, 0.01f);
					}
				}
				break;
			case EnemyStates.PATROL:

				isChase = false;
				agent.speed = speed * 0.5f;

				// 判断是否到了随机巡逻点
				if(Vector3.Distance(wayPoint, transform.position) <= agent.stoppingDistance)
				{
					isWalk = false;
					if(remainLookAtTime > 0)
					{
						remainLookAtTime -= Time.deltaTime;
					}
					else
					{
						GetNewWayPoint();
					}
					
				}
				else
				{
					isWalk = true;
					agent.destination = wayPoint;
				}
				break;
			case EnemyStates.CHASE:

				isWalk = false;
				isChase = true;

				agent.speed = speed;
				if(!FoundPlayer())
				{
					isFollow = false;
					if(remainLookAtTime > 0)
					{
						agent.destination = transform.position;
						remainLookAtTime -= Time.deltaTime;
					}
					else
					{
						if(isGuard)
						{
							enemyStates = EnemyStates.GUARD;
						}
						else
						{
							enemyStates = EnemyStates.PATROL;
						}
					}
				}
				else
				{
					isFollow = true;
					agent.isStopped = false;
					agent.destination = attackTarget.transform.position;
				}

				if(TargetInAttackRange() || TargetInSkillRange())
				{
					isFollow = false;
					agent.isStopped = true;

					if(lastAttackTime < 0)
					{
						lastAttackTime = characterStats.attackData.coolDown;

						characterStats.isCritical = Random.value < characterStats.attackData.criticalChance;
						Attack();
					}
				}

				break;
			case EnemyStates.DEAD:
				enemyCollider.enabled = false;
				agent.enabled = false;
				Destroy(gameObject, 2f);
				break;
		}
	}
	private void Attack()
	{
		transform.LookAt(attackTarget.transform);
		if(TargetInAttackRange())
		{
			animator.SetTrigger("Attack");
		}
		if(TargetInSkillRange())
		{
			animator.SetTrigger("Skill");
		}
	}

	private bool FoundPlayer()
	{
		var colliders = Physics.OverlapSphere(transform.position, sightRadius);
		foreach(var target in colliders)
		{
			
			if (target.CompareTag("Player"))
			{
				attackTarget = target.gameObject;
				return true;
			}
			/*
			if(target.CompareTag("Bullet"))
			{
				attackTarget = GameObject.FindGameObjectWithTag("Player");
				return true;
			}
			*/
		}
		attackTarget = null;
		return false;
	}


	private bool TargetInAttackRange()
	{
		if (attackTarget != null)
			return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.attackRange;
		else
			return false;
	}

	private bool TargetInSkillRange()
	{
		if (attackTarget != null)
			return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterStats.attackData.skillRange;
		else
			return false;
	}

	private void GetNewWayPoint()
	{
		remainLookAtTime = LookAtTime;

		float randomX = Random.Range(-patrolRange, patrolRange);
		float randomZ = Random.Range(-patrolRange, patrolRange);

		Vector3 randomPoint = new Vector3(guardPosition.x + randomX, transform.position.y, guardPosition.z + randomZ);
		NavMeshHit hit;
		if(NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1))
		{
			wayPoint = hit.position;
		}
		else
		{
			wayPoint = transform.position;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, sightRadius);
	}

	private void Hit()
	{
		if(attackTarget != null)
		{
			var targetStats = attackTarget.GetComponent<CharacterStats>();
			targetStats.TakeDamage(characterStats, targetStats);
		}
	}

	public void EndNotify()
	{
		animator.SetBool("Win", true);
		isPlayerDead = true;
		isChase = false;
		isWalk = false;
		attackTarget = null;
		
	}
}
