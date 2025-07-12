using RobbieWagnerGames.UI;
using RobbieWagnerGames.Utilities;
using RobbieWagnerGames.Utilities.SaveData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace RobbieWagnerGames.MakeMoney
{
	public class GameManager : MonoBehaviourSingleton<GameManager>
	{
		#region saveFilePaths
		public const string GAME_DATA_FILE_PATH = "gameData";
		#endregion

		public GameData gameData;
		public GameData defaultGameData = new GameData();
		public bool hasGameData => gameData != null && !defaultGameData.Equals(gameData);

		public Action<GameData> OnGameStarted;

		protected override void Awake()
		{
			base.Awake();

			LoadData();
			MainMenuManager.Instance.InitializeMenu();
		}

		public void StartGame(bool newGame = false)
		{
			if (newGame)
			{
				JsonDataService.Instance.PurgeData();
				gameData = defaultGameData;
				JsonDataService.Instance.SaveData(GAME_DATA_FILE_PATH, defaultGameData);
			}

			StartCoroutine(StartGameCo());
		}

		private IEnumerator StartGameCo()
		{
			yield return StartCoroutine(ScreenCover.Instance.FadeCoverIn());
			MainMenuManager.Instance.enabled = false;
			SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
			yield return new WaitForSeconds(1f);
			OnGameStarted?.Invoke(gameData);
			MainMenuManager.Instance.enabled = false;
			yield return StartCoroutine(ScreenCover.Instance.FadeCoverOut());
		}

		private void LoadGameData()
		{
			ClickerManager.Instance.LoadGame(gameData);
		}

		private void LoadData()
		{
			if (JsonDataService.Instance == null)
				new JsonDataService();

			gameData = JsonDataService.Instance.LoadDataRelative(GAME_DATA_FILE_PATH, defaultGameData);
		}
	}
}