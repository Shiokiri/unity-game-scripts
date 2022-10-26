using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPoint : MonoBehaviour
{
    public enum TransitionType
    {
        SameScene, DifferentScene
    }
    [Header("Transition Information")]
    public string sceneName;
    public TransitionType transitionType;
	public bool isKeyDestination;

	public TransitionDestination.DestinationTag destinationTag;

    private bool canTrans;

    private void Awake()
    {

	}

    private void Update()
    {
        if (canTrans)
        {
			if(isKeyDestination)
			{
				if(GameManager.Instance.playerStats.characterData.key
					== GameManager.Instance.playerStats.characterData.trueKey)
				{
					Debug.Log("Trans");
					canTrans = false;
					this.GetComponent<BoxCollider>().enabled = false;
					SceneController.Instance.TransitionToDestination(this);
				}
			}
			else
			{
				Debug.Log("Trans");
				canTrans = false;
				this.GetComponent<BoxCollider>().enabled = false;
				SceneController.Instance.TransitionToDestination(this);
			}
			
		}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) canTrans = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) canTrans = false;
    }
}
