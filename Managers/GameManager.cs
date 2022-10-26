using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    private CinemachineVirtualCamera virtualCamera;
    public CharacterStats playerStats;
    public Texture2D cursor;

	List<IEndGameObserver> endGameObservers = new List<IEndGameObserver>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        Cursor.SetCursor(cursor, new Vector2(0, 0), CursorMode.Auto);
    }

	public void setCursor()
	{
		Cursor.SetCursor(cursor, new Vector2(0, 0), CursorMode.Auto);
	}

    public void RegisterPlayer(CharacterStats player)
    {
		playerStats = player;
		var virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            if (virtualCameras[i] != null)
            {
                virtualCameras[i].Follow = playerStats.transform.GetChild(0);
            }
        }
    }

	public void AddObserver(IEndGameObserver observer)
	{
		//Debug.Log("add");
		endGameObservers.Add(observer);
	}

	public void RemovedObserver(IEndGameObserver observer)
	{
		endGameObservers.Remove(observer);
	}

	public void NotifyObservers()
	{
		foreach(var observer in endGameObservers)
		{
			observer.EndNotify();
		}
	}

	public Transform GetEntrance()
	{
		foreach(var item in FindObjectsOfType<TransitionDestination>())
		{
			if (item.destinationTag == TransitionDestination.DestinationTag.ENTER)
			return item.transform;
		}
		return null;
	}
}
