using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
	[HideInInspector] public static int version = 4;
	[HideInInspector] public static string showVersion = "1.0.4";
    //[HideInInspector] public static string language = "pr";
    [HideInInspector] public static string language = "en";


    [HideInInspector] public static int StatusPlayerTurn = 0;//to check if it's players turn, hidden in inspector but public.
    [HideInInspector] public static int StatusPlayerMoving = 4;//to check if player is moving, hidden in inspector but public.
    [HideInInspector] public static int StatusEnemiesTurn = 1;//to check if enemies are moving.
    [HideInInspector] public static int StatusDoingSetup = 2;//to check if we're setting up board, prevent PlayerController from moving during setup.
    [HideInInspector] public static int StatusEnemiesMoving = 3;//to check if we're moving enemies, prevent PlayerController from moving during enemies moving.

	[HideInInspector] public int gameStatus = StatusPlayerTurn;//Current status number.

	public static GameController instance = null;//Static instance of GameController which allows it to be accessed by any other script.

    [HideInInspector] public int level = 1;//Current level number, expressed in game as "Day 1".
    private int levelStartDelay = 3;//Level start delay in seconds.
    
    private List<EnemyController> enemies;//List of all EnemyController units, used to issue them move commands.
        
    [HideInInspector] public BoardController bc;//Store a reference to our BoardManager which will set up the level.
	[HideInInspector] public PlayerController pc;//Store a reference to our PlayerController.

	//UI
	private TMP_Text txtLevel;
	[HideInInspector] public TMP_Text txtEnergy;
	[HideInInspector] public TMP_Text txtHype;
	[HideInInspector] public TMP_Text txtMapCurrent;
	[HideInInspector] public TMP_Text txtMapMax;

	//private Coroutine checkPlayerMovementRoutine;
	//private Coroutine checkEnemyMovementRoutine;
	[HideInInspector] public int showingDialogCount;
	//Awake is always called before any Start functions
	void Awake()
	{
		//Check if instance already exists
		if (instance == null)
			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameController.
			Destroy(gameObject);


		////Sets this to not be destroyed when reloading scene
		//DontDestroyOnLoad(gameObject);

		//Assign enemies to a new List of EnemyController objects.
		enemies = new List<EnemyController>();

		//Get a component reference to the attached BoardManager script
		bc = GetComponent<BoardController>();

        //level = 100;//meysam - remove for production

        bc.InitializePlayer();
		
	}

	private void Start()
	{
		pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		txtLevel = GameObject.Find("txtLevel").GetComponent<TMP_Text>();
		txtEnergy = GameObject.Find("txtEnergy").GetComponent<TMP_Text>();
		txtMapCurrent = GameObject.Find("txtMapCurrent").GetComponent<TMP_Text>();
		txtMapMax = GameObject.Find("txtMapMax").GetComponent<TMP_Text>();
		txtHype = GameObject.Find("txtHype").GetComponent<TMP_Text>();
		//imgLevel = GameObject.Find("imgLevel").GetComponent<Image>();

		//Call the InitGame function to initialize the first level 
		InitGame();

		//StartCoroutine(CheckEndPlayerMovement());
		//checkPlayerMovementRoutine = StartCoroutine(CheckEndPlayerMovement());
		//checkEnemyMovementRoutine = null;

	}


	//Update is called every frame.
	void Update()
	{

		//if (Input.GetKeyDown(KeyCode.X))
		//{
		//	Debug.Log("haha");
		//	pc.spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/3_ Player/Player_1_1");
		//}
		try
        {
			//If it's enemies turn.
			if (gameStatus == StatusEnemiesTurn)
			{
				gameStatus = StatusEnemiesMoving;
				//Start moving enemies.
				MoveEnemies();
			}
		}
        catch (System.Exception ex)
        {
			MessageDialogController dialog2 = MessageDialogController.Instance();
			dialog2.Title("Error!");
			string message = "error:" + "\n";
			if (!string.IsNullOrEmpty(ex.Message) && !string.IsNullOrWhiteSpace(ex.Message))
				message += "Message:" + ex.Message + "\n";
			//if (ex.InnerException.Message != null)
			//	message += ex.InnerException.Message + "\n";
			if (!string.IsNullOrEmpty(ex.StackTrace) && !string.IsNullOrWhiteSpace(ex.StackTrace))
				message += "StackTrace:" + ex.StackTrace + "\n";
			message += "\n"+ "To help resolve this error, please take an screen shot and send it to the developers. Thanks!" + "\n";
			dialog2.Message(message);
			dialog2.OnOk("Got It!", () =>
			{ // define what happens when user clicks Yes:

				dialog2.Hide();
				

			});

			dialog2.Show();
			throw;
		}
		
	}

	//Initializes the game for each level.
	void InitGame()
	{

		

		GameObject.Find("_Music").GetComponent<MusicController>().StopMusic();
		GameObject.Find("_Music").GetComponent<MusicController>().PlayGamePeaceMusic();

		if (PlayerPrefs.HasKey("LOADGAME"))
		{
			PlayerPrefs.DeleteKey("LOADGAME");
			SaveModel sm = SaveController.LoadSavedPlayerData();

			

			level = sm.level;
			bc.columns = 5 + Mathf.FloorToInt(level / 3);
			bc.rows = 5 + Mathf.FloorToInt(level / 3);
			//bc.blockadeCount = new CountModel(level,Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns)-4)) / 4));
			//bc.itemCount = new CountModel(level, Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 8));
			//bc.energyCount = new CountModel(level, Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 8));

			//bc.blockadeCount = new CountModel( Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 3),Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 3));
			//bc.itemCount = new CountModel(1 + Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 16),  Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 16));
			//bc.energyCount = new CountModel(1 + Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 64),  Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) / 64));

			bc.blockadeCount = new CountModel(Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.40f), Mathf.FloorToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.40f));
			bc.itemCount = new CountModel(Mathf.CeilToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.10f), Mathf.CeilToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.10f));
			bc.energyCount = new CountModel(Mathf.CeilToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.04f), Mathf.CeilToInt(((bc.columns * bc.rows) - ((4 * bc.columns) - 4)) * 0.04f));


			// meysam - change player to saved player appiarance
			string playerPrefabName = sm.playerPrefabName;
			Destroy(pc.gameObject);
			GameObject newPlayer = Instantiate(RefrenceRepository.GetPrefabByName(playerPrefabName), Vector3.zero, Quaternion.identity) as GameObject;

			pc = newPlayer.GetComponent<PlayerController>();
			pc.playerData.energy = sm.energy;
			pc.playerData.collectedMapCount = sm.collectedMaps;
			pc.playerData.prefabName = sm.playerPrefabName;

			if (!string.IsNullOrEmpty(sm.equipedWeaponeTag))
            {
				pc.EquipWeapon(sm.equipedWeaponeTag);
			}
				

		}


		//While doingSetup is true the player can't move, prevent player from moving while title card is up.
		gameStatus = StatusDoingSetup;

		//Get a reference to our image LevelImage by finding it by name.
		//levelImage = GameObject.Find("LevelImage");

		//Set the text of levelText to the string "Day" and append the current level number.
		txtLevel.font = MeysamLocalization.GetCurrentAllowedFontAsset();
		txtLevel.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
		txtLevel.text = MeysamLocalization.GetLocalizaStringByKey("Day ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(level);
		


		//Set the text of energy
		txtEnergy.font = MeysamLocalization.GetCurrentAllowedFontAsset();
		txtEnergy.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
		txtEnergy.text = MeysamLocalization.GetLocalizaStringByKey("Energy: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(pc.playerData.energy);
		if(pc.playerData.energy < 10)
			txtEnergy.color = MeysamUtility.ColorBloodRed;
		else
			txtEnergy.color = MeysamUtility.ColorYellowText;


		txtHype.text = "";

		//txtMap.text = pc.playerData.collectedMapCount.ToString()+ "/10";
		txtMapMax.font = MeysamLocalization.GetCurrentAllowedFontAsset();
		txtMapMax.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
        txtMapMax.text = MeysamLocalization.GetLocalizaNumberStringByLanguage(10);

		txtMapCurrent.font = MeysamLocalization.GetCurrentAllowedFontAsset();
		txtMapCurrent.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
		txtMapCurrent.text = MeysamLocalization.GetLocalizaNumberStringByLanguage(pc.playerData.collectedMapCount);


		////Set levelImage to active blocking player's view of the game board during setup.
		//imgLevel.gameObject.SetActive(true);



		//Clear any EnemyController objects in our List to prepare for next level.
		enemies.Clear();

		//Call the SetupScene function of the BoardManager script, pass it current level number.
		bc.SetupScene(level);
	
		pc.transform.position = new Vector3(0f, 0f, 0f);

		//Set doingSetup to false allowing player to move again.
		gameStatus = StatusPlayerTurn;
		pc.enabled = true;
		StartCoroutine(CheckEndPlayerMovement());


    }



	//GameOver is called when the player reaches 0 energy points
	public void GameOver(float delay)
	{
		StopAllCoroutines();

		//imgLevel.gameObject.SetActive(true);

		//Disable this GameController.
		enabled = false;

		Invoke("ReturnToMainMenu", delay);
	}

	//GameOver is called when the player reaches 0 energy points
	public void GameWin()
	{
		StopAllCoroutines();

		GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("You escaped!"), 5f, MeysamUtility.ColorYellowText, 40);


		//Disable this GameController.
		enabled = false;

		Invoke("ReturnToMainMenu", 5f);
	}

	//Restart reloads the scene when called.
	private void ReturnToMainMenu()
	{
		//Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
		//and not load all the scene object in the current scene.
		SceneManager.LoadScene(1, LoadSceneMode.Single);
	}


    //Coroutine to move enemies in sequence.
    private void MoveEnemies()
	{
		//Loop through List of EnemyController objects.
		foreach (EnemyController enemy in enemies)
		{
			//Call the MoveEnemy function of EnemyController at index i in the enemies List.
			enemy.Move();

		}

		StartCoroutine(CheckEndEnemyMovement());
		//if (checkEnemyMovementRoutine == null)
		//	checkEnemyMovementRoutine = StartCoroutine(CheckEndEnemyMovement());

	}

	IEnumerator CheckEndEnemyMovement()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			bool allStopped = true;
            foreach (EnemyController enemy in enemies)
            {
				if (enemy.IsMoving())
                {
					allStopped = false;
				}
				else
					enemy.animator.SetBool("Moving", false);

			}
			if (allStopped)
				break;
			
		}

		//Once Enemies are done moving, set playersTurn to true so player can move.
		gameStatus = StatusPlayerTurn;

		if (!pc.CheckIfHavePlaceToMove())
        {
			GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("You got caught!"), 5f, MeysamUtility.ColorBloodRed, 40);
			GameOver(5f);
		}
		

		StartCoroutine(CheckEndPlayerMovement());

		//checkEnemyMovementRoutine = null;

		//if(checkPlayerMovementRoutine == null)
		//	checkPlayerMovementRoutine = StartCoroutine(CheckEndPlayerMovement());

		yield return null;
	}

	IEnumerator CheckEndPlayerMovement()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			if (gameStatus == StatusPlayerMoving)
			{
				if (!pc.IsMoving())
				{
					pc.animator.SetBool("Moving", false);
					break;
				}
			
			}
		}

		//Once Enemies are done moving, set playersTurn to true so player can move.
		gameStatus = StatusEnemiesTurn;

		//checkPlayerMovementRoutine = null;

		yield return null;
	}

	//Call this to add the passed in EnemyController to the List of EnemyController objects.
	public void AddEnemyToList(EnemyController script)
	{
		//Add EnemyController to List enemies.
		enemies.Add(script);
	}
	//Call this to add the passed in EnemyController to the List of EnemyController objects.
	public void RemoveEnemyFromList(EnemyController script)
	{
		//Add EnemyController to List enemies.
		enemies.Remove(script);
	}

	//NextLevel is called when the player reaches the exit
	public void NextLevel()
	{
		

		StartCoroutine(Reset(levelStartDelay));
    }

	IEnumerator Reset(float Count)
	{

		yield return new WaitForSeconds(Count); //Count is the amount of time in seconds that you want to wait.
												//Load the last scene loaded, in this case Main, the only scene in the game. And we load it in "Single" mode so it replace the existing one
												//and not load all the scene object in the current scene.
		ClearScene();
		
		level++;
		
		SaveController.SavePlayerData();

		InitGame();
		yield return null;
	}

	private void ClearScene()
    {
		StopAllCoroutines();

	



		Destroy(GameObject.Find("Energies"));


		Destroy(GameObject.Find("Walls"));
		Destroy(GameObject.Find("Tiles"));
		Destroy(GameObject.Find("Enemies"));
		Destroy(GameObject.Find("Blockades"));
		Destroy(GameObject.Find("Items"));
		Destroy(GameObject.FindGameObjectWithTag("Exit"));
		Destroy(GameObject.FindGameObjectWithTag(MapModel.TAG_MAP));
		
    }

	public bool IsDialogVisible()
    {
		if (showingDialogCount > 0)
			return true;
		return false;
    }
}