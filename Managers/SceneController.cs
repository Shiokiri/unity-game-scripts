 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using StarterAssets;

public class SceneController : Singleton<SceneController>, IEndGameObserver
{
    public GameObject playerPrefab;
    private GameObject player;
    private CharacterStats characterStats;
	public SceneFader sceneFaderPrefab;
	private bool fadeFinished;
	public float fadeTime;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
		GameManager.Instance.AddObserver(this);
		fadeFinished = true;
		fadeTime = 1f;
	}
    
    public void TransitionToDestination(TransitionPoint transitionPoint)
    {
        switch(transitionPoint.transitionType)
        {
            case TransitionPoint.TransitionType.SameScene:
                StartCoroutine(Transition(SceneManager.GetActiveScene().name, transitionPoint.destinationTag));
                break;
            case TransitionPoint.TransitionType.DifferentScene:
				// player = GameManager.Instance.playerStats.gameObject;
				// Destroy(player);
				StartCoroutine(Transition(transitionPoint.sceneName, transitionPoint.destinationTag));
                break;
        }
    }

    IEnumerator Transition(string sceneName, TransitionDestination.DestinationTag destinationTag)
    {
		SaveManager.Instance.SavePlayerData();
        if(SceneManager.GetActiveScene().name != sceneName)
        {
			SceneFader fade = Instantiate(sceneFaderPrefab);
			yield return StartCoroutine(fade.FadeOut(fadeTime));
			yield return SceneManager.LoadSceneAsync(sceneName);
            yield return Instantiate(playerPrefab, GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
			SaveManager.Instance.LoadPlayerData();
			GameManager.Instance.setCursor();
			yield return StartCoroutine(fade.FadeIn(fadeTime));
			yield break;
        }
        else if (SceneManager.GetActiveScene().name == sceneName)
		{
			SceneFader fade = Instantiate(sceneFaderPrefab);
			yield return StartCoroutine(fade.FadeOut(fadeTime));
			player = GameManager.Instance.playerStats.gameObject;
            ThirdPersonController thirdPersonController = player.GetComponent<ThirdPersonController>();
            thirdPersonController.enabled = false;
            player.transform.SetPositionAndRotation(GetDestination(destinationTag).transform.position, GetDestination(destinationTag).transform.rotation);
            thirdPersonController.enabled = true;
			yield return StartCoroutine(fade.FadeIn(fadeTime));
			yield return null;
        }
    }
    private TransitionDestination GetDestination(TransitionDestination.DestinationTag destinationTag)
    {
        var entrances = FindObjectsOfType<TransitionDestination>();
        for(int i = 0; i < entrances.Length; i++)
        {
            if (entrances[i].destinationTag == destinationTag)
                return entrances[i];
        }
        return null;
    }

	public void TransitionToMain()
	{
		StartCoroutine(LoadMain());
	}

	public void TransitionToFirstLevel()
	{
		StartCoroutine(LoadLevel("Corridor"));
	}

	public void TransitionToLoadGame()
	{
		StartCoroutine(LoadLevel("Corridor"));
		//StartCoroutine(LoadLevel(SaveManager.Instance.SceneName));
	}

	IEnumerator LoadLevel(string scene)
	{
		SceneFader fade = Instantiate(sceneFaderPrefab);
		if(scene != "")
		{
			yield return StartCoroutine(fade.FadeOut(fadeTime));
			yield return SceneManager.LoadSceneAsync(scene);
			//yield return Instantiate(playerPrefab, GameManager.Instance.GetEntrance().position, GameManager.Instance.GetEntrance().rotation);
			yield return Instantiate(playerPrefab, new Vector3(7.30834436f, 6.34947062f, 8.09599209f), new Quaternion(0f, 0f, 0f, 0f));

			SaveManager.Instance.SavePlayerData();
			yield return StartCoroutine(fade.FadeIn(fadeTime));
			yield break;
		}

	}

	IEnumerator LoadMain()
	{
		SceneFader fade = Instantiate(sceneFaderPrefab);
		yield return StartCoroutine(fade.FadeOut(fadeTime));
		yield return SceneManager.LoadSceneAsync("Forest UI");
		yield return StartCoroutine(fade.FadeIn(fadeTime));
		yield break;
	}

	public void EndNotify()
	{
		if(fadeFinished)
		{
			fadeFinished = false;
			StartCoroutine("LoadMain");
		}
	}
}
