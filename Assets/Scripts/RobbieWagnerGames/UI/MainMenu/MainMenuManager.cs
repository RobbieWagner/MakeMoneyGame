using RobbieWagnerGames.MakeMoney;
using RobbieWagnerGames.Managers;
using RobbieWagnerGames.Utilities.SaveData;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RobbieWagnerGames.UI
{
    public class MainMenuManager : Menu
    {
        public static MainMenuManager Instance { get; set; }

        [SerializeField] private Button continueButton;
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button controlsButton;
        [SerializeField] private Button creditsButton;
        [SerializeField] private Button quitButton;

        [SerializeField] private string sceneToGoTo;

        [SerializeField] private Menu settings;
        [SerializeField] private Menu controls;
        [SerializeField] private Menu credits;

        protected override void Awake()
        {
            if (Instance != null)
                Destroy(this.gameObject);
            else
                Instance = this;

            base.Awake();
            Cursor.lockState = CursorLockMode.None;

            if (JsonDataService.Instance == null)
                new JsonDataService();
        }

        protected override void OnEnable()
        {
            newGameButton.onClick.AddListener(StartNewGame);
            continueButton.onClick.AddListener(ContinueGame);
            settingsButton.onClick.AddListener(OpenSettings);
            //controlsButton.onClick.AddListener(OpenControls);
            //creditsButton.onClick.AddListener(OpenCredits);
            quitButton.onClick.AddListener(QuitGame);

            //if(JsonDataService.Instance.LoadData())

            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            newGameButton.onClick.RemoveListener(StartNewGame);
            continueButton.onClick.RemoveListener(ContinueGame);
            settingsButton.onClick.RemoveListener(OpenSettings);
            //controlsButton.onClick.RemoveListener(OpenControls);
            //creditsButton.onClick.RemoveListener(OpenCredits);
            quitButton.onClick.RemoveListener(QuitGame);
        }

        private void StartNewGame()
        {
            GameManager.Instance.StartGame(true);
        }

        private void ContinueGame() 
        {
            GameManager.Instance.StartGame();
        }

        private void OpenSettings()
        {
            StartCoroutine(SwapMenu(this, settings));
        }

        //private void OpenControls()
        //{
        //    StartCoroutine(SwapCanvases(thisCanvas, controls));
        //}

        //private void OpenCredits()
        //{
        //    StartCoroutine(SwapCanvases(thisCanvas, credits));
        //}

        private void QuitGame()
        {
            ToggleButtonInteractibility(false);

            //save any new save data
            Application.Quit();
        }

        protected override void ToggleButtonInteractibility(bool toggleOn)
        {
            base.ToggleButtonInteractibility(toggleOn);

            continueButton.interactable = toggleOn;
            settingsButton.interactable = toggleOn;
            //controlsButton.interactable = toggleOn;
            //creditsButton.interactable = toggleOn;
            quitButton.interactable = toggleOn;
        }

        protected override IEnumerator SwapMenu(Menu active, Menu next, bool setAsLastMenu = true)
        {
            yield return StartCoroutine(base.SwapMenu(active, next));

            StopCoroutine(SwapMenu(active, next));
        }

		public void InitializeMenu()
		{
			if (GameManager.Instance.hasGameData)
            {
                continueButton.gameObject.SetActive(true);

                Navigation cNav = new Navigation();
                cNav.mode = Navigation.Mode.Explicit;
                cNav.selectOnDown = newGameButton;
                continueButton.navigation = cNav;

				Navigation nNav = new Navigation();
				nNav.mode = Navigation.Mode.Explicit;
                nNav.selectOnUp = continueButton;
				nNav.selectOnDown = settingsButton;
				continueButton.navigation = nNav;
			}
            else
            {
                continueButton.gameObject.SetActive(false);

				Navigation nNav = new Navigation();
				nNav.mode = Navigation.Mode.Explicit;
				nNav.selectOnDown = settingsButton;
				continueButton.navigation = nNav;
			}

			Navigation sNav = new Navigation();
			sNav.mode = Navigation.Mode.Explicit;
			sNav.selectOnUp = newGameButton;
			sNav.selectOnDown = quitButton;
			continueButton.navigation = sNav; 
            
            Navigation qNav = new Navigation();
			qNav.mode = Navigation.Mode.Explicit;
			qNav.selectOnUp = settingsButton;
			continueButton.navigation = qNav;
		}
	}
}