using RobbieWagnerGames.UI;
using RobbieWagnerGames.Utilities;
using RobbieWagnerGames.Utilities.SaveData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

		protected override void Awake()
		{
			base.Awake();

			LoadData();
			MainMenuManager.Instance.InitializeMenu();
		}

		public void StartGame(bool newGame = false)
		{
			if (newGame)
				Debug.Log("new game");
			else
				Debug.Log("continue");
		}

		private void LoadData()
		{
			if (JsonDataService.Instance == null)
				new JsonDataService();

			gameData = JsonDataService.Instance.LoadDataRelative<GameData>(GAME_DATA_FILE_PATH, defaultGameData);
		}
	}
}