using RobbieWagnerGames.TileSelectionGame;
using RobbieWagnerGames.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobbieWagnerGames.MakeMoney
{
    public class ClickerManager : MonoBehaviourSingleton<ClickerManager>
    {
		[SerializeField] private Camera clickerCamera;

		protected override void Awake()
		{
			base.Awake();

			GameManager.Instance.OnGameStarted += LoadGame;
		}

		public void LoadGame(GameData gameData)
        {
			Debug.Log($"clicks: {gameData.clicks}, workers: {gameData.workers}" );
			CameraManager.Instance.SetGameCamera(clickerCamera);
        }
    }
}