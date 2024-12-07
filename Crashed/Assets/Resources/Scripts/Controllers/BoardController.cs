using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
	[HideInInspector]public int columns = 5;                                         //Number of columns in our game board.
	[HideInInspector] public int rows = 5;                                            //Number of rows in our game board.

	[HideInInspector] public CountModel blockadeCount = new CountModel(1, Mathf.FloorToInt(25 / 3));                   //Lower and upper limit for our random number of walls per level.
	[HideInInspector] public CountModel itemCount = new CountModel(1, 1);                      //Lower and upper limit for our random number of items per level.
	[HideInInspector] public CountModel energyCount = new CountModel(1, 1);                      //Lower and upper limit for our random number of items per level.

	public GameObject exitTile;                                         //Prefab to spawn for exit.
	public GameObject[] floorTiles;                                 //Array of floor prefabs.
	public GameObject[] blockadePrefabs;                                  //Array of blockade prefabs.
	public float[] blockadePrefabProbabilities;
	public GameObject[] itemPrefabs;                                  //Array of item prefabs.
	public float[] itemPrefabProbabilities;
	public GameObject[] energyPrefabs;                                  //Array of energy prefabs.
	public float[] energyPrefabProbabilities;                                  //Array of energy prefab probabilities.
	public GameObject mapPrefab;                                  //map prefab.
	public GameObject[] enemyPrefabs;                                 //Array of enemy prefabs.
	public float[] enemyPrefabProbabilities;
	public GameObject[] playerPrefabs;                                 //Array of player prefabs.

	public GameObject wallLeftTile;                                //Array of outer tile prefabs.
	public GameObject wallRightTile;                               //Array of outer tile prefabs.
	public GameObject wallTopTile;                              //Array of outer tile prefabs.
	public GameObject wallDownTile;                                //Array of outer tile prefabs.
	public GameObject wallDownLeftTile;                                //Array of outer tile prefabs.
	public GameObject wallDownRightTile;                               //Array of outer tile prefabs.
	public GameObject wallTopLeftTile;                              //Array of outer tile prefabs.
	public GameObject wallTopRightTile;                             //Array of outer tile prefabs.

	private Transform tileHolder;                                  //A variable to store a reference to the parent transform of our Tile objects.
	private Transform wallHolder;                                  //A variable to store a reference to the parent transform of our Wall object.
	private List<Vector3> gridPositions = new List<Vector3>();   //A list of possible locations to place tiles.


	private Transform blockadeHolder;
	private Transform itemHolder;
	private Transform energyHolder;
	private Transform enemyHolder;



	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseGridPositions()
	{


        //Clear our list gridPositions.
        gridPositions.Clear();



        blockadeHolder = new GameObject("Blockades").transform;
		itemHolder = new GameObject("Items").transform;
		energyHolder = new GameObject("Energies").transform;
		enemyHolder = new GameObject("Enemies").transform;

		//Loop through x axis (columns).
		for (int x = 1; x < columns - 1; x++)
		{
			//Within each column, loop through y axis (rows).
			for (int y = 1; y < rows - 1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add(new Vector3(x, y, 0f));
			}
		}
	}


	//Sets up the outer walls and floor tiles of the game board.
	void BoardSetup()
	{
	
		//Instantiate Board and set boardHolder to its transform.
		wallHolder = new GameObject("Walls").transform;
		tileHolder = new GameObject("Tiles").transform;
	




		bool isWall = false;
		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for (int x = -1; x < columns + 1; x++)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for (int y = -1; y < rows + 1; y++)
			{
				isWall = false;

				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];

				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if (x == -1 && y == -1)
                {
					isWall = true;
					toInstantiate = wallDownLeftTile;
				}
				else if (x == -1 && y == rows+1)
                {
					isWall = true;
					toInstantiate = wallTopLeftTile;
				}
				else if (x == columns && y == -1)
                {
					isWall = true;
					toInstantiate = wallDownRightTile;
				}
				else if (x == columns+1 && y == rows)
                {
					isWall = true;
					toInstantiate = wallTopRightTile;
				}
				else if (x == -1)
                {
					isWall = true;
					toInstantiate = wallLeftTile;
				}
				else if (x == columns)
                {
					isWall = true;
					toInstantiate = wallRightTile;
				}
				else if (y == -1)
                {
					isWall = true;
					toInstantiate = wallDownTile;
				}
				else if (y == rows)
                {
					isWall = true;
					toInstantiate = wallTopTile;
				}
					

				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;


				if(isWall)
                {
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent(wallHolder);
				}
				else
                {
					//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
					instance.transform.SetParent(tileHolder);
				}
				
			}
		}
	}


	//RandomPosition returns a random position from our list gridPositions and the removes the position from the list.
	Vector3 RandomPosition()
	{
		Vector3 randomPosition;
		
			//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
			int randomIndex = Random.Range(0, gridPositions.Count);

			//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
			randomPosition = new Vector3(gridPositions[randomIndex].x, gridPositions[randomIndex].y, gridPositions[randomIndex].z);


		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt(randomIndex);
		
		

		//Return the randomly selected Vector3 position.
		return randomPosition;
	}


	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom(GameObject[] prefabsToCreate, float[] prefabsToCreateProbabilities, int minimumNumber, int maximumNumber, Transform parentGO)
	{

		if (prefabsToCreate.Length == 0)
			return;

		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range(minimumNumber, maximumNumber + 1);

		//Instantiate objects until the randomly chosen limit objectCount is reached
		for (int i = 0; i < objectCount; i++)
		{

			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();

			int randomIndex = MeysamUtility.GetRandomIndexByChance(prefabsToCreateProbabilities);

			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = prefabsToCreate[randomIndex];

			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			GameObject go = Instantiate(tileChoice, randomPosition, Quaternion.identity);
			go.transform.SetParent(parentGO);

		}
	}

	


	//SetupScene initializes our level and calls the related functions to lay out the game board
	public void SetupScene(int level)
	{

		////meysam - must comment
		//columns = 5 + Mathf.FloorToInt(level / 3);
		//rows = 5 + Mathf.FloorToInt(level / 3);
		//blockadeCount = new CountModel(level, Mathf.FloorToInt(((columns * rows) - ((4 * columns) - 4)) / 4));
		//itemCount = new CountModel(level, Mathf.FloorToInt(((columns * rows) - ((4 * columns) - 4)) / 8));

	
        //Creates the outer walls and floor.
        BoardSetup();

		//Reset our list of gridpositions.
		InitialiseGridPositions();


		


		//filter blockade prefabs based on born day!
		List<GameObject> blockadePrefabTemps = new List<GameObject>();
		List<float> blockadePrefabTempProbabilities = new List<float>();
        for (int i = 0; i < blockadePrefabs.Length; i++)
        {
			if (BlockadeModel.GetBornDayByTag(blockadePrefabs[i].tag) <= GameController.instance.level)
			{
				blockadePrefabTemps.Add(blockadePrefabs[i]);
				blockadePrefabTempProbabilities.Add(blockadePrefabProbabilities[i]);
				//if (EnemyModel.GetBornDayByTag(enemy.tag) == GameController.instance.level)
				//{
				//    //show introduction dialog!
				//    MessageWithPictureDialogController dialog2 = MessageWithPictureDialogController.Instance();
				//    dialog2.Title("New enemy!");
				//    dialog2.Message("Ohhh Sh*t!");
				//    dialog2.Picture(RefrenceRepository.GetSpriteByTag(enemy.tag));
				//    dialog2.OnOk("Got It!", () =>
				//    { // define what happens when user clicks Yes:

				//        dialog2.Hide();


				//    });

				//    dialog2.Show();
				//}

			}

		}
		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom(blockadePrefabTemps.ToArray(), blockadePrefabTempProbabilities.ToArray(), blockadeCount.minimum, blockadeCount.maximum, blockadeHolder);

		////tune blockade tree layers
		//GameObject[] trees = BlockadeModel.GetAllTreeBlockadeGOs();
  //      foreach (GameObject item in trees)
  //      {
		//	SortingLayerController.TuneDynamicSortingLayer(item.gameObject.GetComponent<SpriteRenderer>(), item.gameObject.GetComponent<BoxCollider2D>());

		//}
		//////////////////////////////////////////////////////////////

		//filter item prefabs based on born day!
		List<GameObject> itemPrefabTemps = new List<GameObject>();
		List<float> itemPrefabTempProbabilities = new List<float>();
        for (int i = 0; i < itemPrefabs.Length; i++)
        {
			if (WeaponModel.IsTagWeapon(itemPrefabs[i].tag))
			{
				if (WeaponModel.GetBornDayByTag(itemPrefabs[i].tag) <= GameController.instance.level)
				{
					itemPrefabTemps.Add(itemPrefabs[i]);
					itemPrefabTempProbabilities.Add(itemPrefabProbabilities[i]);
					//if (WeaponModel.GetBornDayByTag(item.tag) == GameController.instance.level)
					//{
					//    //show introduction dialog!
					//    MessageWithPictureDialogController dialog2 = MessageWithPictureDialogController.Instance();
					//    dialog2.Title("New tools!");
					//    dialog2.Message(WeaponModel.GetDescription(item.tag));
					//    dialog2.Picture(RefrenceRepository.GetSpriteByTag(item.tag));
					//    dialog2.OnOk("Got It!", () =>
					//    { // define what happens when user clicks Yes:

					//        dialog2.Hide();


					//    });

					//    dialog2.Show();
					//}
				}
			}
			else
            {
				itemPrefabTemps.Add(itemPrefabs[i]);
				itemPrefabTempProbabilities.Add(itemPrefabProbabilities[i]);
			}
				
		}

		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.
		
		LayoutObjectAtRandom(itemPrefabTemps.ToArray(), itemPrefabTempProbabilities.ToArray(), itemCount.minimum, itemCount.maximum, itemHolder);





		//filter energy prefabs based on born day!
		List<GameObject> energyPrefabTemps = new List<GameObject>();
		List<float> energyPrefabTempProbabilities = new List<float>();
        for (int i = 0; i < energyPrefabs.Length; i++)
        {
			if (EnergyModel.IsTagEnergy(energyPrefabs[i].tag))
			{
				if (EnergyModel.GetBornDayByTag(energyPrefabs[i].tag) <= GameController.instance.level)
				{
					energyPrefabTemps.Add(energyPrefabs[i]);
					energyPrefabTempProbabilities.Add(energyPrefabProbabilities[i]);

				}
			}
		}

		//Instantiate a random number of food tiles based on minimum and maximum, at randomized positions.

		LayoutObjectAtRandom(energyPrefabTemps.ToArray(), energyPrefabTempProbabilities.ToArray(), energyCount.minimum, energyCount.maximum, energyHolder);



		//Determine number of enemies based on current level number, based on a logarithmic progression
		//int enemyCount = (int)Mathf.Log(level, 2f);

		int enemyCount = 0;
		if (GameController.instance.level >= 4)
			enemyCount = Mathf.FloorToInt(level / 4);
		else
			if (GameController.instance.level == 3)
				enemyCount = 1;

        ////meysam - must comment
        //enemyCount = Mathf.FloorToInt(level / 4);

        //filter enemy prefabs based on born day!
        List<GameObject> enemyPrefabTemps = new List<GameObject>();
        List<float> enemyPrefabTempProbabilities = new List<float>();
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
			if (EnemyModel.GetBornDayByTag(enemyPrefabs[i].tag) <= GameController.instance.level)
			{
				enemyPrefabTemps.Add(enemyPrefabs[i]);
				enemyPrefabTempProbabilities.Add(enemyPrefabProbabilities[i]);
				//if (EnemyModel.GetBornDayByTag(enemy.tag) == GameController.instance.level)
				//{
				//	//show introduction dialog!
				//	MessageWithPictureDialogController dialog2 = MessageWithPictureDialogController.Instance();
				//	dialog2.Title("New enemy!");
				//	dialog2.Message(EnemyModel.GetDescription(enemy.tag));
				//	dialog2.Picture(RefrenceRepository.GetSpriteByTag(enemy.tag));
				//	dialog2.OnOk("Got It!", () =>
				//	{ // define what happens when user clicks Yes:

				//			dialog2.Hide();


				//	});

				//	dialog2.Show();
				//}

			}
		}
		//Instantiate a random number of enemies based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom(enemyPrefabTemps.ToArray(), enemyPrefabTempProbabilities.ToArray(), enemyCount, enemyCount, enemyHolder);



		if(level >= MapModel.GetBornDay())
        {

			if (level == 100)
			{
				for (int i = GameController.instance.pc.playerData.collectedMapCount; i < 10; i++)
				{
					//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
					Vector3 randomPosition = RandomPosition();

					//Instantiate the map tile
					Instantiate(mapPrefab, randomPosition, Quaternion.identity);
				}

			}
			else if (level == MapModel.GetBornDay() ||
				(level > 10 && GameController.instance.pc.playerData.collectedMapCount < 1) ||
				(level > 20 && GameController.instance.pc.playerData.collectedMapCount < 2) ||
				(level > 30 && GameController.instance.pc.playerData.collectedMapCount < 3) ||
				(level > 40 && GameController.instance.pc.playerData.collectedMapCount < 4) ||
				(level > 50 && GameController.instance.pc.playerData.collectedMapCount < 5) ||
				(level > 60 && GameController.instance.pc.playerData.collectedMapCount < 6) ||
				(level > 70 && GameController.instance.pc.playerData.collectedMapCount < 7) ||
				(level > 80 && GameController.instance.pc.playerData.collectedMapCount < 8) ||
				(level > 90 && GameController.instance.pc.playerData.collectedMapCount < 9))
			{
					//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
					Vector3 randomPosition = RandomPosition();

					//Instantiate the map tile
					Instantiate(mapPrefab, randomPosition, Quaternion.identity);
				

			}
			else if((1 < level && level <= 10 && GameController.instance.pc.playerData.collectedMapCount >= 1) || 
				(10 < level && level <= 20 && GameController.instance.pc.playerData.collectedMapCount >= 2) ||
				(20 < level && level <= 30 && GameController.instance.pc.playerData.collectedMapCount >= 3) ||
				(30 < level && level <= 40 && GameController.instance.pc.playerData.collectedMapCount >= 4) ||
				(40 < level && level <= 50 && GameController.instance.pc.playerData.collectedMapCount >= 5) ||
				(50 < level && level <= 60 && GameController.instance.pc.playerData.collectedMapCount >= 6) ||
				(60 < level && level <= 70 && GameController.instance.pc.playerData.collectedMapCount >= 7) ||
				(70 < level && level <= 80 && GameController.instance.pc.playerData.collectedMapCount >= 8) ||
				(80 < level && level <= 90 && GameController.instance.pc.playerData.collectedMapCount >= 9))
            {
				//do nothing...
            }
			else
            {
				float randomIndex = Random.Range(0, 1);
				if (randomIndex <= 0.2f)
				{
					//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
					Vector3 randomPosition = RandomPosition();

					//Instantiate the map tile
					Instantiate(mapPrefab, randomPosition, Quaternion.identity);
				}
			}
			
		}


		if(level < 100)
			//Instantiate the exit tile in the upper right hand corner of our game board
			Instantiate(exitTile, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);

		if(level == 1)
        {
			MessageDialogController dialog = MessageDialogController.Instance();
			dialog.Title(MeysamLocalization.GetLocalizaStringByKey("How it all started"));
			dialog.Message(MeysamLocalization.GetLocalizaStringByKey("Our plane CRASHED in the middle of nowhere! I'm alone, scared, and hungry. I must find a way out of here alive."));
			dialog.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
			{ // define what happens when user clicks Yes:

				dialog.Hide();

				MessageDialogController dialog2 = MessageDialogController.Instance();
				dialog2.Title(MeysamLocalization.GetLocalizaStringByKey("Help"));
				dialog2.Message(MeysamLocalization.GetLocalizaStringByKey("It's a turn-base game!\n Tap on the screen to move around.\n You can kill the enemies with a spear.\n You can destroy rocks with a pickaxe.\n You can remove bushes with an axe.\n Pay attention to your energy!\n By consuming meat, you will get hyped and can move double the distance on the game by touch and hold!"));
				dialog2.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
				{ // define what happens when user clicks Yes:

					dialog2.Hide();

					GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("New Day!"), 3f, MeysamUtility.ColorYellowText, 40);


				});
				dialog2.Show();

			});

			dialog.Show();
		}
		else
			GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("New Day!"), 3f, MeysamUtility.ColorYellowText, 40);

	}

	public void InitializePlayer()
    {
		//instantiate Player
		int randomIndex = Random.Range(0, playerPrefabs.Length);
		GameObject player = Instantiate(playerPrefabs[randomIndex], Vector3.zero, Quaternion.identity);

		player.GetComponent<PlayerController>().playerData.prefabName = playerPrefabs[randomIndex].name;
	}
}

