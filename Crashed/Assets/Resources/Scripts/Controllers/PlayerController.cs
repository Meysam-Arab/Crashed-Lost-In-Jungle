using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.SceneManagement;

public class PlayerController : EntityController
{

    [HideInInspector] public PlayerModel playerData;

  
    public AudioClip[] screamSounds;//scream sounds


    //#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
    private Vector2 touchOrigin = -Vector2.one; //Used to store location of screen touch origin for mobile controls.
                                                //#endif
    private CapsuleCollider2D boxCollider; 		//The BoxCollider2D component attached to this object.
    private MovementController mc;
    [HideInInspector]public EmotionController ec;
    public LayerMask blockingLayer;			//Layer on which collision will be checked.

    private UnityEngine.U2D.Animation.SpriteResolver weaponHolderSpriteResolver;
    protected SpriteRenderer weaponSpriteRenderer;

    private Vector3 lastPlayerPosition;

    public TouchState touchState;

    protected int remainingHypeCount;

    protected override void Awake()
    {
        base.Awake();
        playerData = new PlayerModel();
        playerData.Initialize();

        mc = gameObject.GetComponent<MovementController>();
        ec = gameObject.GetComponent<EmotionController>();
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<CapsuleCollider2D>();

        lastPlayerPosition = -1 * Vector3.one;
        //TraumaInducerController tic = GetComponent<TraumaInducerController>();
        //tic.StartInduceStress();


        weaponHolderSpriteResolver = transform.Find("WeaponHolder").gameObject.GetComponent<UnityEngine.U2D.Animation.SpriteResolver>();
        weaponSpriteRenderer = transform.Find("WeaponHolder").gameObject.GetComponent<SpriteRenderer>();

    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        remainingHypeCount = 0;

        ec.StartBlinking();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() ||
            EventSystem.current.currentSelectedGameObject != null)
        {
            return;
        }

        

        //if (IsPointerOverUIObject())
        //    return;


        //// Check if there is a touch
        //// Check touches
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    var touch = Input.GetTouch(i);


        //    //if (touch.phase == TouchPhase.Began)
        //    //{
        //        // Check if finger is over a UI element
        //        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //        {
        //            return;
        //        }
        //    //}

        //}

        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        //{
        //    // Check if finger is over a UI element
        //    if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //    {
        //        return;
        //    }
        //}

        //if (IsPointerOverGameObject())
        //    return;

        //// Check mouse
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}

        //// Check touches
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    var touch = Input.GetTouch(i);
        //    if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //    {
        //        return;
        //    }

        //}

        if (GameController.instance.IsDialogVisible())
            return;

        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        //if (IsPointerOverUIObject())
        //{
        //    return;
        //}
        

        //If it's not the player's turn, exit the function.
        if (GameController.instance.gameStatus != GameController.StatusPlayerTurn || Time.timeScale == 0) return;


        int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.
    
        //Check if Input has registered more than zero touches
        if (Input.touchCount > 0)
        {
            //if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            //    return;

            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                touchState = new TouchState();
                touchState.state = TouchState.TOUCH_STATE_NON;
                touchState.holdDuration = TouchState.HOLD_DURATION;
               
            }
            else if (myTouch.phase == TouchPhase.Stationary)
            {
                touchState.holdDuration -= Time.deltaTime;

                if (touchState.holdDuration <= 0)
                {
                    //Do Stuff when timer is finished
                    touchState.state = TouchState.TOUCH_STATE_HOLD;
                    
                }
            }
            else if(myTouch.phase == TouchPhase.Ended)
            {
                if (touchState.state == TouchState.TOUCH_STATE_NON)
                    touchState.state = TouchState.TOUCH_STATE_TAP;
                
            }


            if (touchState.state != TouchState.TOUCH_STATE_NON)
            {
                
                //If so, set touchOrigin to the position of that touch
                touchOrigin = gameObject.transform.position;
                Vector2 touchEnd = Camera.main.ScreenToWorldPoint(myTouch.position);
                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Calculate the difference between the beginning and end of the touch on the y axis.
                float y = touchEnd.y - touchOrigin.y;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;

                //Check if the difference along the x axis is greater than the difference along the y axis.
                if (Mathf.Abs(x) >= Mathf.Abs(y))
                {
                    //If x is greater than zero, set horizontal to 1, otherwise set it to -1
                    horizontal = x > 0 ? 1 : -1;
                    vertical = 0;
                }
                else
                {
                    //If y is greater than zero, set horizontal to 1, otherwise set it to -1
                    vertical = y > 0 ? 1 : -1;
                    horizontal = 0;
                }
                    

                if (touchState.state == TouchState.TOUCH_STATE_TAP)
                {
                    //#endif //End of mobile platform dependendent compilation section started above with #elif
                    //Check if we have a non-zero value for horizontal or vertical
                    if (horizontal != 0 || vertical != 0)
                    {

                        //Set canMove to true if Move was successful, false if failed.
                        bool canMove = CanMove(horizontal, vertical);

                        if (canMove)
                        {

                            //Every time player moves, subtract from Energy points total.
                            playerData.energy--;

                            if(playerData.energy < 10)
                            {

                                //tell player that energy is low
                                float probAppear = Random.Range(0f, 1f);
                                if (probAppear <= 0.75f)
                                {
                                    GetComponentInChildren<SpeechController>().ShowADSpeech();

                                }
                            }
                            

                            //Update food text display to reflect current Energy.
                            GameController.instance.txtEnergy.font = MeysamLocalization.GetCurrentAllowedFontAsset();
                            GameController.instance.txtEnergy.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
                            GameController.instance.txtEnergy.text = MeysamLocalization.GetLocalizaStringByKey("Energy: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(playerData.energy);
                            if (playerData.energy < 10)
                                GameController.instance.txtEnergy.color = MeysamUtility.ColorBloodRed;
                            else
                                GameController.instance.txtEnergy.color = MeysamUtility.ColorYellowText;


                            lastPlayerPosition = gameObject.transform.position;

                            SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt((transform.position + new Vector3(horizontal, vertical)).y), Mathf.RoundToInt((transform.position + new Vector3(horizontal, vertical)).x));
                            if(playerData.equipedWeaponeTag != null)
                            {
                                weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder+1;
                            }

                            animator.SetBool("Moving", true);
                         
                            mc.StartMove(transform.position + new Vector3(horizontal, vertical));
                            GameController.instance.gameStatus = GameController.StatusPlayerMoving;


                            if (PlayerPrefs.GetInt("AUDIO") == 1)
                            {

                                int indexRandom = Random.Range(0, moveSounds.Length);
                                audioSource.clip = moveSounds[indexRandom];
                                audioSource.Play();

                            }

                            ////Call RandomizeSfx of SoundController to play the move sound, passing in two audio clips to choose from.
                            //SoundController.instance.RandomizeSfx(moveSound1, moveSound2);

                            //Since the player has moved and lost food points, check if the game has ended.
                            CheckIfGameOver();
                            
                        }

                    }
                }
                else if(touchState.state == TouchState.TOUCH_STATE_HOLD)
                {

                    if (remainingHypeCount > 0)
                    {
                        // meysam - for boost movements!
                        if (horizontal > 0)
                            horizontal++;
                        else if (vertical > 0)
                            vertical++;
                        else if (horizontal < 0)
                            horizontal--;
                        else if (vertical < 0)
                            vertical--;

                    }


                    //same tap movement as pervious code!
                    //#endif //End of mobile platform dependendent compilation section started above with #elif
                    //Check if we have a non-zero value for horizontal or vertical
                    if (horizontal != 0 || vertical != 0)
                        {

                            //Set canMove to true if Move was successful, false if failed.
                            bool canMove = CanMove(horizontal, vertical);

                        if(!canMove)
                        {
                            if(horizontal > 1)
                                horizontal--;
                            else if (vertical > 1)
                                vertical--;
                            else if (horizontal < 1)
                                horizontal++;
                            else if (vertical < 1)
                                vertical++;

                            canMove = CanMove(horizontal, vertical);
                        }

                        if (canMove)
                        {

                            if (remainingHypeCount > 0)
                            {
                                
                                remainingHypeCount--;
                                if (remainingHypeCount == 0)
                                {
                                    //show hype animation as well as sadnss emotion
                                    ec.StartFearing();
                                    animator.SetTrigger("Hype");

                                    //Update hype count.
                                    GameController.instance.txtHype.text = "";
                                }
                                else
                                {
                                    //Update hype count.
                                    GameController.instance.txtHype.font = MeysamLocalization.GetCurrentAllowedFontAsset();
                                    GameController.instance.txtHype.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
                                    GameController.instance.txtHype.text = MeysamLocalization.GetLocalizaStringByKey("Remaining Hype: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(remainingHypeCount);

                                }


                            }

                            //Every time player moves, subtract from Energy points total.
                            playerData.energy--;
                            if (playerData.energy < 10)
                            {

                                //tell player that energy is low
                                float probAppear = Random.Range(0f, 1f);
                                if (probAppear <= 0.75f)
                                {
                                    GetComponentInChildren<SpeechController>().ShowADSpeech();

                                }
                            }

                            //Update food text display to reflect current Energy.

                            GameController.instance.txtEnergy.font = MeysamLocalization.GetCurrentAllowedFontAsset();
                            GameController.instance.txtEnergy.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
                            GameController.instance.txtEnergy.text = MeysamLocalization.GetLocalizaStringByKey("Energy: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(playerData.energy);
                            if (playerData.energy < 10)
                                GameController.instance.txtEnergy.color = MeysamUtility.ColorBloodRed;
                            else
                                GameController.instance.txtEnergy.color = MeysamUtility.ColorYellowText;

                            lastPlayerPosition = gameObject.transform.position;

                                SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt((transform.position + new Vector3(horizontal, vertical)).y), Mathf.RoundToInt((transform.position + new Vector3(horizontal, vertical)).x));
                                if (playerData.equipedWeaponeTag != null)
                                {
                                    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder+1;
                                }
                            animator.SetBool("Moving", true);
                            mc.StartMove(transform.position + new Vector3(horizontal, vertical));


                            if (PlayerPrefs.GetInt("AUDIO") == 1)
                            {

                                int indexRandom = Random.Range(0, moveSounds.Length);
                                audioSource.clip = moveSounds[indexRandom];
                                audioSource.Play();

                            }

                            GameController.instance.gameStatus = GameController.StatusPlayerMoving;

                                ////Call RandomizeSfx of SoundController to play the move sound, passing in two audio clips to choose from.
                                //SoundController.instance.RandomizeSfx(moveSound1, moveSound2);

                                //Since the player has moved and lost food points, check if the game has ended.
                                CheckIfGameOver();


                        }

                    }
                }
                
            }
        }
    }
    

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and check collision.
    protected bool CanMove(int xDir, int yDir)
    {

        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit2D[] hits;

        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(xDir, yDir);

 

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hits = Physics2D.LinecastAll(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider.enabled = true;

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (BlockadeModel.IsTagBlockade(hit.transform.gameObject.tag))
                {

                    //Get a component reference to the component of type T attached to the object that was hit
                    BlockadeController hitComponent = hit.transform.GetComponent<BlockadeController>();

                    //if (playerData.equipedWeaponeTag != null)
                    //{
                    //    if (BlockadeModel.IsBlockadeRemovable(hitComponent.tag, playerData.equipedWeaponeTag))
                    //    {
                    //        return true;
                    //    }
                    //}

                    //if (BlockadeModel.IsBlockadeWalkableUpon(hit.transform.gameObject.tag))
                    //    return true;
                    if (playerData.equipedWeaponeTag == null && !BlockadeModel.IsBlockadeWalkableUpon(hit.transform.gameObject.tag))
                        return false;
                    if (playerData.equipedWeaponeTag != null)
                        if (!BlockadeModel.IsBlockadeRemovable(hitComponent.tag, playerData.equipedWeaponeTag) && !BlockadeModel.IsBlockadeWalkableUpon(hit.transform.gameObject.tag))
                        {
                            
                             GetComponentInChildren<SpeechController>().ShowForcedSpeech(WeaponModel.GetWeaponDescriptionError(playerData.equipedWeaponeTag));
                    
                            return false;
                        }


                    //if (BlockadeModel.IsBlockadeWalkableUpon(hit.transform.gameObject.tag))
                    //{
                    //    //enemy is moving so player layer must go one layer down - player going to die

                    //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
                    //}

                }
                //else if (EnergyModel.IsTagEnergy(hit.transform.gameObject.tag))
                //{
                //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
                //}
                //else if (WeaponModel.IsTagWeapon(hit.transform.gameObject.tag))
                //{
                //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
                //}
                //else if (hit.transform.gameObject.tag == "Exit")
                //{
                //    return true;
                //}
                //else if (MapModel.IsTagMap(hit.transform.gameObject.tag))
                //{
                //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
                //}
                else if (EnemyModel.IsTagEnemy(hit.transform.gameObject.tag))
                {
                    //if (playerData.equipedWeaponeTag != null && EnemyModel.IsEnemyKillable(hit.transform.gameObject.tag, playerData.equipedWeaponeTag))
                    //    return true;
                    if (playerData.equipedWeaponeTag == null)
                        return false;
                    if (playerData.equipedWeaponeTag != null && !EnemyModel.IsEnemyKillable(hit.transform.gameObject.tag, playerData.equipedWeaponeTag))
                    {
                        
                        GetComponentInChildren<SpeechController>().ShowForcedSpeech(WeaponModel.GetWeaponDescriptionError(playerData.equipedWeaponeTag));

                        return false;
                    }



                    //if (playerData.equipedWeaponeTag != null && EnemyModel.IsEnemyKillable(hit.transform.gameObject.tag, playerData.equipedWeaponeTag))
                    //{
                    //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                    //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
                    //}
                    //else
                    //{
                    //    //enemy is moving so player layer must go one layer down - player going to die

                    //    spriteRenderer.sortingOrder = hit.transform.gameObject.GetComponent<EnemyController>().spriteRenderer.sortingOrder - 2;
                    //    weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

                    //   }


                }
                else if (WallModel.IsTagWall(hit.transform.gameObject.tag))
                {
                    return false;
                }
                //else
                //    //If something was hit, return false, Move was unsuccesful.
                //    return false;
            }


            //If something was hit, return false, Move was unsuccesful.
            return true;
        }
        else
        {
            return true;
        }
    }


    //CheckIfGameOver checks if the player is out of food points and if so, ends the game.
    private void CheckIfGameOver()
    {
        //Check if food point total is less than or equal to zero.
        if (playerData.energy <= 0)
        {
            ec.StartFearing();
        

            GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("You malnourished!"), 5f, MeysamUtility.ColorBloodRed, 40);

            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name), 0f);

            }

            //Call the GameOver function of GameController.
            GameController.instance.GameOver(RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name).length);
        }
    }


    public bool IsMoving()
    {
        if (mc.onMove)
            return true;
        else
            return false;
    }

    public void EquipWeapon(string weaponTag)
    {

        weaponHolderSpriteResolver.SetCategoryAndLabel(weaponTag, weaponTag);
        weaponHolderSpriteResolver.ResolveSpriteToSpriteRenderer();
        playerData.equipedWeaponeTag = weaponTag;
      
        weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder+1;
        
    }

    private void UnEquipWeapon()
    {
        
        weaponSpriteRenderer.sprite = null;
        playerData.equipedWeaponeTag = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (WeaponModel.IsTagWeapon(collision.gameObject.tag))
        {
            ec.StartHappiness();

            //player is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

            if (playerData.equipedWeaponeTag != null)
            {
                //drop current weapon
                int index = 0;
                Vector3 emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);
            
                while (emptyTile == -1 * Vector3.one)
                {
                    index++;
                    emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);
                }
                GameObject itemGO = Instantiate(RefrenceRepository.GetPrefabByTag(playerData.equipedWeaponeTag), emptyTile, Quaternion.identity) as GameObject;
                itemGO.transform.SetParent(GameObject.Find("Items").transform);

                UnEquipWeapon();
            }
            EquipWeapon(collision.gameObject.tag);
            Destroy(collision.gameObject);

        }
        else if (EnergyModel.IsTagEnergy(collision.gameObject.tag))
        {
            if (PlayerPrefs.GetInt("AUDIO") == 1)
            {
                GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Eat_Food_Sound_Name), 0f);
            }

            ec.StartHappiness();

            //player is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

            if (EnergyModel.GetHypeTurnCountByTag(collision.gameObject.tag) > 0)
            {
                remainingHypeCount += EnergyModel.GetHypeTurnCountByTag(collision.gameObject.tag);
               
                animator.SetTrigger("Hype");

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Hype_Sound_Name), 0f);
                }

                //Update hype count.
           
                GameController.instance.txtHype.font = MeysamLocalization.GetCurrentAllowedFontAsset();
                GameController.instance.txtHype.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
                GameController.instance.txtHype.text = MeysamLocalization.GetLocalizaStringByKey("Remaining Hype: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(remainingHypeCount);

            }

            //		//Call the RandomizeSfx function of SoundController and pass in two drinking sounds to choose between to play the drinking sound effect.
            //		SoundController.instance.RandomizeSfx(drinkSound1, drinkSound2);


            //add to energy
            playerData.energy += EnergyModel.GetEnergyByTag(collision.gameObject.tag);
            //Update food text display to reflect current Energy.
            GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("Energy + ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(EnergyModel.GetEnergyByTag(collision.gameObject.tag)), 3f, MeysamUtility.ColorYellowText, 40);

            GameController.instance.txtEnergy.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            GameController.instance.txtEnergy.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            GameController.instance.txtEnergy.text = MeysamLocalization.GetLocalizaStringByKey("Energy: ") + MeysamLocalization.GetLocalizaNumberStringByLanguage(playerData.energy);
            if (playerData.energy < 10)
                GameController.instance.txtEnergy.color = MeysamUtility.ColorBloodRed;
            else
                GameController.instance.txtEnergy.color = MeysamUtility.ColorYellowText;


            Destroy(collision.gameObject);
        }
        else if (EnemyModel.IsTagEnemy(collision.gameObject.tag))
        {

            if (playerData.equipedWeaponeTag != null && EnemyModel.IsEnemyKillable(collision.gameObject.tag, playerData.equipedWeaponeTag))
            {
                //player is moving so enemy layer must go one layer down because enemy will die
                //collision.gameObject.GetComponent<EnemyController>().spriteRenderer.sortingOrder = spriteRenderer.sortingOrder-1;
                spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

                UnEquipWeapon();
               

                //drop meat
                int index = 0;
                Vector3 emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);

                while (emptyTile == -1 * Vector3.one)
                {
                    index++;
                    emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);
                }
                GameObject itemGO = Instantiate(RefrenceRepository.GetPrefabByTag(EnemyModel.GetEnemyDropTag(collision.gameObject.tag)), emptyTile, Quaternion.identity) as GameObject;
                itemGO.transform.SetParent(GameObject.Find("Items").transform);
                Destroy(collision.gameObject);

            }
            else
            {
                //enemy is moving so player layer must go one layer down - player going to die

                spriteRenderer.sortingOrder = collision.gameObject.GetComponent<EnemyController>().spriteRenderer.sortingOrder - 2;
                weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

                DamageFlashController.dfc.ShowDamage();
                TraumaInducer tic = gameObject.GetComponent<TraumaInducer>();
                tic.StartInduceStress();

                int indexRandom = Random.Range(0, screamSounds.Length);
                audioSource.clip = screamSounds[indexRandom];
                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    
                    audioSource.Play();

                    GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name), audioSource.clip.length);

                    
                }

                //spriteRenderer.enabled = false;
                weaponSpriteRenderer.enabled = false;
                GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("You died!"), 5f, MeysamUtility.ColorBloodRed, 40);
                GameController.instance.GameOver(audioSource.clip.length + RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name).length);
            }

        }
        else if (collision.gameObject.tag == "Exit")
        {

            ec.StartHappiness();

            //increase every two level
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GameObject[] enemies = EnemyModel.GetAllEnemyNPCGOs();
            foreach (GameObject item in enemies)
            {
                item.GetComponent<EnemyController>().enabled = false;
            }

            GameController.instance.bc.columns = 5 + Mathf.FloorToInt(GameController.instance.level / 3);
            GameController.instance.bc.rows = 5 + Mathf.FloorToInt(GameController.instance.level / 3);
            //GameController.instance.bc.blockadeCount = new CountModel(GameController.instance.level, Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 4));
            //GameController.instance.bc.itemCount = new CountModel(GameController.instance.level, Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 8));
            //GameController.instance.bc.energyCount = new CountModel(GameController.instance.level, Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 16));

            //GameController.instance.bc.blockadeCount = new CountModel(Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) /3), Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 3));
            //GameController.instance.bc.itemCount = new CountModel(1 + Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) /16), Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 16));
            //GameController.instance.bc.energyCount = new CountModel(1 + Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 64), Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) / 64));

            GameController.instance.bc.blockadeCount = new CountModel(Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4))* 0.40f), Mathf.FloorToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) * 0.40f));
            GameController.instance.bc.itemCount = new CountModel(Mathf.CeilToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) * 0.10f), Mathf.CeilToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) * 0.10f));
            GameController.instance.bc.energyCount = new CountModel(Mathf.CeilToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) * 0.04f), Mathf.CeilToInt(((GameController.instance.bc.columns * GameController.instance.bc.rows) - ((4 * GameController.instance.bc.columns) - 4)) * 0.04f));


            //Disable the player object since level is over.
            enabled = false;


            remainingHypeCount = 0;

            //Invoke the NextLevel function to start the next level
            GameController.instance.NextLevel();


        }
        else if (MapModel.IsTagMap(collision.gameObject.tag))
        {
            //player is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

            playerData.collectedMapCount++;

            if(playerData.collectedMapCount == 1)
            {
                MessageDialogController dialog2 = MessageDialogController.Instance();
                dialog2.Title(MeysamLocalization.GetLocalizaStringByKey("Discovery!"));
                dialog2.Message(MeysamLocalization.GetLocalizaStringByKey("Wow! It's some sort of map! Maybe by collecting all the parts i can find a way to get out of here!"));
                dialog2.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
                { // define what happens when user clicks Yes:

                    dialog2.Hide();

                    //Show Map Animation
                    MapDialogController dialog = MapDialogController.Instance();
                    dialog.SetAnimationIndex(playerData.collectedMapCount);
                    dialog.OnOk(() =>
                    { // define what happens when user clicks Yes:

                        dialog.Hide();
                       
                    });

                    dialog.Show();

                });
                
                dialog2.Show();

            }
            else
            {
                //Show Map Animation
                MapDialogController dialog = MapDialogController.Instance();
                dialog.SetAnimationIndex(playerData.collectedMapCount);

                dialog.OnOk(() =>
                { // define what happens when user clicks Yes:

                    dialog.Hide();

                    if (playerData.collectedMapCount == 10)
                    {

                        if (PlayerPrefs.GetInt("MUSIC") == 1)
                        {
                            GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Winner_Sound_Name), 0f);
                        }

                        MessageDialogController dialog = MessageDialogController.Instance();
                        dialog.Title(MeysamLocalization.GetLocalizaStringByKey("Congratulations!"));
                        dialog.Message(MeysamLocalization.GetLocalizaStringByKey("You completed the map! Now you can get out of this cursed jungle easly!"));
                        dialog.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
                        { // define what happens when user clicks Yes:

                            dialog.Hide();

                            GameObject.Find("_Music").GetComponent<MusicController>().PlayCreditMusic();

                            MessageDialogController dialog2 = MessageDialogController.Instance();
                            dialog2.Title(MeysamLocalization.GetLocalizaStringByKey("Credits"));
                            dialog2.Message(MeysamLocalization.GetLocalizaStringByKey("Brought to you by Bored Alchemist indie game studio!\n Some of the musics and sounds by Eric Matyas:www.soundimage.org\n Take a look at our other games at: htttps://www.boredalchemist.com\n\n Programmer: Meysam Arab\n Art: Misagh Arab\n\n Thank you for playing the game!"));
                            dialog2.OnOk(MeysamLocalization.GetLocalizaStringByKey("Got It!"), () =>
                            { // define what happens when user clicks Yes:

                                dialog2.Hide();
                               
                                GameController.instance.GameWin();

                            });

                            dialog2.Show();

                        });

                        dialog.Show();
                    }
                });

                dialog.Show();
            }


   

            GameController.instance.txtMapCurrent.font = MeysamLocalization.GetCurrentAllowedFontAsset();
            GameController.instance.txtMapCurrent.isRightToLeftText = MeysamLocalization.IsCurrentLanguageRTL();
            GameController.instance.txtMapCurrent.text = MeysamLocalization.GetLocalizaNumberStringByLanguage(playerData.collectedMapCount);
 
            
            Destroy(collision.gameObject);

           
        }
        else if (BlockadeModel.IsTagBlockade(collision.gameObject.tag))
        {
            if (playerData.equipedWeaponeTag != null && BlockadeModel.IsBlockadeRemovable(collision.gameObject.tag, playerData.equipedWeaponeTag))
            {

                if(BlockadeModel.GetBlockadeDropTag(collision.gameObject.tag) != null)
                {
                    //drop item from blockade
                    int index = 0;
                    Vector3 emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);

                    while (emptyTile == -1 * Vector3.one)
                    {
                        index++;
                        emptyTile = ReturnRandomNeighborEmptyTile(lastPlayerPosition, index);
                    }
                    GameObject itemGO = Instantiate(RefrenceRepository.GetPrefabByTag(BlockadeModel.GetBlockadeDropTag(collision.gameObject.tag)), emptyTile, Quaternion.identity) as GameObject;
                    itemGO.transform.SetParent(GameObject.Find("Items").transform);

                }


                UnEquipWeapon();
                collision.gameObject.GetComponent<BlockadeController>().DelayedDestroy();

            }
            else if (BlockadeModel.IsBlockadeWalkableUpon(collision.transform.gameObject.tag))
            {
                //enemy is moving so player layer must go one layer down - player going to die

                spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
                weaponSpriteRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;

                DamageFlashController.dfc.ShowDamage();

               

                TraumaInducer tic = gameObject.GetComponent<TraumaInducer>();
                tic.StartInduceStress();

                int indexRandom = Random.Range(0, screamSounds.Length);
                audioSource.clip = screamSounds[indexRandom];
                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                   
                    audioSource.Play();

                    GameObject.Find("_Sound").GetComponent<SoundController>().PlaySound(false, RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name), audioSource.clip.length);

                    

                }
                if(BlockadeModel.TAG_BLOCKADE_PIT == collision.transform.gameObject.tag)
                {
                    audioSource.clip = screamSounds[indexRandom];
                    if (PlayerPrefs.GetInt("AUDIO") == 1)
                    {
                        collision.transform.gameObject.GetComponent<AudioSource>().Play();

                    }
                    spriteRenderer.enabled = false;
                    weaponSpriteRenderer.enabled = false;
                }

                transform.position = collision.transform.position;
                mc.StopMove();


                GameObject.Find("dlgCenterScreen").GetComponent<CenterScreenDialogController>().ShowMessage(MeysamLocalization.GetLocalizaStringByKey("You died!"), 5f, MeysamUtility.ColorBloodRed, 40);
                GameController.instance.GameOver(audioSource.clip.length + RefrenceRepository.GetAudioByName(RefrenceRepository.Game_Over_Lose_Sound_Name).length);
            }
                

        }
    }


    public bool CheckIfHavePlaceToMove()
    {
        return CanMove(1, 0) || CanMove(-1, 0) || CanMove(0, 1) || CanMove(0, -1);
    }

    public Vector3 ReturnRandomNeighborEmptyTile(Vector3 centerPosition, int limit)
    {
        
        List<Vector2> tiles = new List<Vector2>();
        if (IsTileEmptyToDrop(centerPosition + new Vector3(limit * 1, 0, centerPosition.z)))
            tiles.Add(centerPosition + new Vector3(limit * 1, 0));
        if (IsTileEmptyToDrop(centerPosition + new Vector3(limit * -1, 0, centerPosition.z)))
            tiles.Add(centerPosition + new Vector3(limit * -1, 0));
        if (IsTileEmptyToDrop(centerPosition + new Vector3(0, limit * 1, centerPosition.z)))
            tiles.Add(centerPosition + new Vector3(0, limit * 1));
        if (IsTileEmptyToDrop(centerPosition + new Vector3(0, limit * -1, centerPosition.z)))
            tiles.Add(centerPosition + new Vector3(0, limit * -1));

        if (tiles.Count > 0)
        {
            int randomIndex = Random.Range(0, tiles.Count);

            return tiles[randomIndex];
        }
        else
            return -1 * Vector3.one;
        
    }

    //Move returns true if it is empty and false if not. 
    //Move takes parameters for x direction, y direction and check collision.
    /// <summary>
    /// check if there is anything in specific position (it's not a line cast it just check the specific coordinates)
    /// </summary>
    /// <param name="xDir"></param>
    /// <param name="yDir"></param>
    /// <param name="startPosition"></param>
    /// <returns></returns>
    protected bool IsTileEmptyToDrop(Vector3 checkPosition)
    {

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(checkPosition, 0.1f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            if (BlockadeModel.IsTagBlockade(hitCollider.transform.gameObject.tag))
            {
                return false;

            }
            else if (EnergyModel.IsTagEnergy(hitCollider.transform.gameObject.tag))
            {
                return false;
            }
            else if (WeaponModel.IsTagWeapon(hitCollider.transform.gameObject.tag))
            {
                return false;
            }
            else if (hitCollider.transform.gameObject.tag == "Exit")
            {
                return false;
            }
            else if (EnemyModel.IsTagEnemy(hitCollider.transform.gameObject.tag))
            {
                return true;
            }
            else if (MapModel.IsTagMap(hitCollider.transform.gameObject.tag))
            {
                return false;
            }
            else if (hitCollider.transform.gameObject.tag == "Player")
            {
                return false;
            }
            else
                //If something was hit, but not in list.
                return false;
        }
        return true;


    }

    //private bool IsPointerOverUIObject()
    //{
    //    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    //    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    //    return results.Count > 0;
    //}

    ///// <returns>true if mouse or first touch is over any event system object ( usually gui elements )</returns>
    //public static bool IsPointerOverGameObject()
    //{
    //    //check mouse
    //    if (EventSystem.current.IsPointerOverGameObject())
    //        return true;

    //    //check touch
    //    if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
    //    {
    //        if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
    //            return true;
    //    }

    //    return false;
    //}
    //private bool IsPointerOverUIObject()
    //{

    //    PointerEventData eventData = new PointerEventData(EventSystem.current);
    //    eventData.position = Input.mousePosition;
    //    List<RaycastResult> results = new List<RaycastResult>();
    //    EventSystem.current.RaycastAll(eventData, results);
    //    return results.Count > 0;

    //    //// Check touches
    //    //for (int i = 0; i < Input.touchCount; i++)
    //    //{
    //    //    var touch = Input.GetTouch(i);

    //    //    var touchPosition = touch.position;
    //    //    var eventData = new PointerEventData(EventSystem.current) { position = touchPosition };
    //    //    var results = new List<RaycastResult>();
    //    //    EventSystem.current.RaycastAll(eventData, results);

    //    //    if (results.Count > 0)
    //    //    {
    //    //        return true;
    //    //    }
           

    //    //}

    //    //return false;

       
    //}

}
