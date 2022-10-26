using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    [SerializeField] private Transform vfxHitGreen;
    [SerializeField] private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
	}

    private void Start() {
        float speed = 35f;
        bulletRigidbody.velocity = transform.forward * speed;
        GameManager.Instance.playerStats.CurrentBulletsNumber -= 1;
    }

	private void Hit(CharacterStats attackerStats, CharacterStats targetStats)
	{
		Debug.Log("Bullet Hit!");
		if (targetStats != null)
		{
			Debug.Log("TakeDamage");
			targetStats.TakeDamage(attackerStats, targetStats);
		}
	}

	private void OnTriggerEnter(Collider other) {
		/*
        Debug.Log(other.gameObject.name);
        if (other.GetComponent<BulletTarget>() != null) {
            // Hit target
            Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
            BulletTarget target = other.GetComponent<BulletTarget>();
            target.reduceHealthPoint(10);
        } else {
            // Hit something else
            Instantiate(vfxHitRed, transform.position, Quaternion.identity);
        }
		*/
		CharacterStats targetStats = other.GetComponent<CharacterStats>();
		if (targetStats != null)
		{
			Hit(GameManager.Instance.playerStats, targetStats);
			Instantiate(vfxHitGreen, transform.position, Quaternion.identity);
		}
		else
		{
			// Hit something else
			Instantiate(vfxHitRed, transform.position, Quaternion.identity);
		}
		Destroy(gameObject);
    }

}