using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : EntityController
{

    [HideInInspector] public EnemyModel enemyData;

  
    public AudioClip[] roarSounds;                      //roar sounds


    [HideInInspector]public Transform target;                           //Transform to attempt to move toward each turn.


    private CapsuleCollider2D capsuleCollider; 		//The BoxCollider2D component attached to this object.
    private MovementController mc;
    private EmotionController ec;
    public LayerMask blockingLayer;         //Layer on which collision will be checked.
    public bool isDead;
    protected override void Awake()
    {
        base.Awake();

        mc = gameObject.GetComponent<MovementController>();
        ec = gameObject.GetComponent<EmotionController>();

        //Get a component reference to this object's BoxCollider2D
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
     
        isDead = false;
        enemyData = new EnemyModel();
        enemyData.Initialize(gameObject.tag);

     
        //Find the PlayerController GameObject using it's tag and store a reference to its transform component.
        SetTarget();

      

        //Register this enemy with our instance of GameController by adding it to a list of EnemyController objects. 
        //This allows the GameController to issue movement commands.
        GameController.instance.AddEnemyToList(this);

        ec.StartBlinking();
    }

    private void OnDestroy()
    {
        GameController.instance.RemoveEnemyFromList(this);
    }

    /// <summary>
    /// Call move animation
    /// </summary>
    public void Move()
    {
        if (isDead)
            return;
        //Boolean to determine whether or not enemy should skip a turn or move this turn.
        float prob = Random.value;                            
        if (prob < enemyData.moveProbability)
        {
            return;

        }

        //set the next target
        SetTarget();

        if(target == null)
        {
            //random movement
           

            int xDir = 0;
            int yDir = 0;

            float directionChance = Random.Range(0, 1);
            if (directionChance > 0.5)
            {
                //move horizontal
                xDir = Random.Range(0, 2);
                if (xDir == 0)
                    xDir = -1;
                yDir = 0;
            }
            else
            {
                //move vertical
                xDir = 0;
                yDir = Random.Range(0, 2);
                if (yDir == 0)
                    yDir = -1;
            }


            //Set canMove to true if Move was successful, false if failed.
            bool canMove = CanMove(xDir, yDir);

            if (canMove)
            {
                //check if destination is shared with other enemies and cancel the move if it does!
                Vector3 tmpDes = transform.position + new Vector3(xDir, yDir);
                GameObject[] enemies = EnemyModel.GetAllEnemyNPCGOs();
                //Loop through List of EnemyController objects.
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetInstanceID() != this.gameObject.GetInstanceID() && !enemy.GetComponent<EnemyController>().isDead)
                    {
                        //// meysam - coment for production
                        //if (enemy.GetComponent<EnemyController>().mc == null)
                        //{
                        //    throw new System.Exception("mc is null"+" enemy name:"+enemy.name);
                        //}
                        ///////////////////////////////////////////

                        if (enemy.GetComponent<EnemyController>().mc.GetDestination() == tmpDes)
                        {
                            if (EnemyModel.TAG_ENEMY_MONKEY != enemy.tag)
                                return;
                            if (EnemyModel.TAG_ENEMY_MONKEY == tag)
                                return;
                        }
                    }
                       

                }
                ////////////////////////////////////////////////////////////////////////////////////
                SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt((transform.position + new Vector3(xDir, yDir)).y), Mathf.RoundToInt((transform.position + new Vector3(xDir, yDir)).x));
                animator.SetBool("Moving", true);
                mc.StartMove(transform.position + new Vector3(xDir, yDir));

                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {

                    int indexRandom = Random.Range(0, moveSounds.Length);
                    audioSource.clip = moveSounds[indexRandom];
                    audioSource.Play();

                }
            }
        }
        else
        {
            //go toward target
            int limitStepHorizontal = enemyData.moveTileCounts;
            int limitStepVertical = enemyData.moveTileCounts;
            int tempHorizontal = Mathf.RoundToInt(Mathf.Abs(target.position.x - transform.position.x));
            int tempVertical = Mathf.RoundToInt(Mathf.Abs(target.position.y - transform.position.y));

            if (limitStepHorizontal > tempHorizontal)
            {
                limitStepHorizontal = tempHorizontal;
                if (limitStepHorizontal == 0)
                    limitStepHorizontal = 1;
            }
            if (limitStepVertical > tempVertical)
            {
                limitStepVertical = tempVertical;
                if (limitStepVertical == 0)
                    limitStepVertical = 1;
            }

            bool isLeap = true;
            prob = Random.value;//Boolean to determine whether or not enemy should skip a turn or move this turn.                            
            if (prob < enemyData.leapProbability)
            {
                //No leap move
                limitStepVertical = 1;
                limitStepHorizontal = 1;
                isLeap = false;
            }


            Vector3 emptyTileHorizontal = ReturnRandomNeighborEmptyTile(transform.position, limitStepHorizontal);

            while (emptyTileHorizontal == -1 * Vector3.one)
            {

                if (limitStepHorizontal == 0)
                {
                    limitStepHorizontal = 1;
                    break;
                }
                limitStepHorizontal--;
                emptyTileHorizontal = ReturnRandomNeighborEmptyTile(transform.position, limitStepHorizontal);
            }

            Vector3 emptyTileVertical = ReturnRandomNeighborEmptyTile(transform.position, limitStepVertical);

            while (emptyTileVertical == -1 * Vector3.one)
            {

                if (limitStepVertical == 0)
                {
                    limitStepVertical = 1;
                    break;
                }
                limitStepVertical--;
                emptyTileVertical = ReturnRandomNeighborEmptyTile(transform.position, limitStepVertical);
            }


            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
            int yDir = 0;

            //If the difference in positions is approximately zero (Epsilon) do the following:
            if (Mathf.Abs(target.position.x - transform.position.x) < 0.05)
            {
                //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
                yDir = target.position.y > transform.position.y ? limitStepVertical * 1 : limitStepVertical * -1;


            }
            //If the difference in positions is not approximately zero (Epsilon) do the following:
            else if (Mathf.Abs(target.position.y - transform.position.y) < 0.05)
            {
                //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
                xDir = target.position.x > transform.position.x ? limitStepHorizontal * 1 : limitStepHorizontal * -1;

            }
            else
            {
                prob = Random.value;//Boolean to determine whether enemy should go vertical or horizontal.                            
                if (prob < 0.5)
                {
                    //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
                    yDir = target.position.y > transform.position.y ? limitStepVertical * 1 : limitStepVertical * -1;

                }
                else
                {
                    //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
                    xDir = target.position.x > transform.position.x ? limitStepHorizontal * 1 : limitStepHorizontal * -1;
                }
            }

            //Set canMove to true if Move was successful, false if failed.
            bool canMove = CanMove(xDir, yDir);

            if (canMove)
            {
                //check if destination is shared with other enemies and cancel the move if it does!
                Vector3 tmpDes = transform.position + new Vector3(xDir, yDir);
                GameObject[] enemies = EnemyModel.GetAllEnemyNPCGOs();
                //Loop through List of EnemyController objects.
                foreach (GameObject enemy in enemies)
                {
                    if (enemy.GetInstanceID() != this.gameObject.GetInstanceID())
                    {
                        //// meysam - coment for production
                        //if (enemy.GetComponent<EnemyController>().mc == null)
                        //{
                        //    throw new System.Exception("mc is null" + " enemy name:" + enemy.name);
                        //}
                        ///////////////////////////////////////////
                        if (enemy.GetComponent<EnemyController>().mc.GetDestination() == tmpDes)
                        {
                            if (EnemyModel.TAG_ENEMY_MONKEY != enemy.tag)
                                return;
                            if (EnemyModel.TAG_ENEMY_MONKEY == tag)
                                return;
                        }
                    }
                   
                            
                }
                ////////////////////////////////////////////////////////////////////////////////////


                SortingLayerController.SetDynamicSortingLayer(spriteRenderer, Mathf.RoundToInt((transform.position + new Vector3(xDir, yDir)).y), Mathf.RoundToInt((transform.position + new Vector3(xDir, yDir)).x));
                animator.SetBool("Moving", true);
                mc.StartMove(transform.position + new Vector3(xDir, yDir));

                if (PlayerPrefs.GetInt("AUDIO") == 1 && spriteRenderer.isVisible)
                {
                    if (isLeap)
                    {

                        int indexRandom = Random.Range(0, roarSounds.Length);
                        audioSource.clip = roarSounds[indexRandom];
                        audioSource.Play();
                   
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ec.StartFearing();

                    }
                    else
                    {
                        int indexRandom = Random.Range(0, moveSounds.Length);
                        audioSource.clip = moveSounds[indexRandom];
                        audioSource.Play();
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
        capsuleCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        //ContactFilter2D cf2d = new ContactFilter2D();
        //cf2d.SetLayerMask(blockingLayer);
        hits = Physics2D.LinecastAll(start, end, blockingLayer);

        //Debug.DrawLine(start, end, MeysamUtility.ColorBloodRed, 30000);
        //Debug.DrawRay(start, new Vector2(xDir, yDir), MeysamUtility.ColorBloodRed, 30000);

        //Re-enable boxCollider after linecast
        capsuleCollider.enabled = true;


        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (BlockadeModel.IsTagBlockade(hit.transform.gameObject.tag))
                {
                    if(!BlockadeModel.IsTagAllowedToMoveOver(hit.transform.gameObject.tag, tag))
                    {
                        return false;
                    }

                }
                else if (EnemyModel.IsTagEnemy(hit.transform.gameObject.tag))
                {
                    if (EnemyModel.TAG_ENEMY_MONKEY == tag)
                        return false;
                    else if (EnemyModel.TAG_ENEMY_MONKEY != hit.transform.gameObject.tag)
                        return false;
                }
                else if (WallModel.IsTagWall(hit.transform.gameObject.tag))
                {
                    return false;
                }
                else if (!EnemyModel.IsEnemyKiller(tag) && hit.transform.gameObject.tag == "Player")
                {
                    return false;
                }
            }
            

            //If something was hit, return false, Move was unsuccesful.
            return true;
        }
        else
        {
           return true;
        }

    }

    public bool IsMoving()
    {
        if (mc.onMove)
            return true;
        else
            return false;
    }

    public Vector3 ReturnRandomNeighborEmptyTile(Vector3 centerPosition, int limit)
    {

        List<Vector2> tiles = new List<Vector2>();
        if (IsTileEmptyToMove(limit * 1, 0, centerPosition))
            tiles.Add(centerPosition + new Vector3(limit * 1, 0));
        if (IsTileEmptyToMove(limit * -1, 0, centerPosition))
            tiles.Add(centerPosition + new Vector3(limit * -1, 0));
        if (IsTileEmptyToMove(0, limit * 1, centerPosition))
            tiles.Add(centerPosition + new Vector3(0, limit * 1));
        if (IsTileEmptyToMove(0, limit * -1, centerPosition))
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
    protected bool IsTileEmptyToMove(int xDir, int yDir, Vector3 startPosition)
    {

        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit2D[] hits;

        //Store start position to move from, based on objects current transform position.
        Vector2 start = startPosition;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(xDir, yDir);



        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        capsuleCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        //ContactFilter2D cf2d = new ContactFilter2D();
        //cf2d.SetLayerMask(blockingLayer);
        hits = Physics2D.LinecastAll(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        capsuleCollider.enabled = true;



        if (hits.Length > 0)
        {
            
            foreach (RaycastHit2D hit in hits)
            {
                if (BlockadeModel.IsTagBlockade(hit.transform.gameObject.tag))
                {
                    if (!BlockadeModel.IsTagAllowedToMoveOver(hit.transform.gameObject.tag, tag))
                    {
                        return false;
                    }
                }
                else if (EnemyModel.IsTagEnemy(hit.transform.gameObject.tag))
                {
                  
                    if (EnemyModel.TAG_ENEMY_MONKEY == tag)
                        return false;
                    else if(EnemyModel.TAG_ENEMY_MONKEY != hit.transform.gameObject.tag)
                        return false;
                }
                else if (WallModel.IsTagWall(hit.transform.gameObject.tag))
                {
                    return false;
                }
                else if (!EnemyModel.IsEnemyKiller(tag) && hit.transform.gameObject.tag == "Player")
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (WeaponModel.IsTagWeapon(collision.gameObject.tag))
        {
            //enemy is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            if (tag == EnemyModel.TAG_ENEMY_CANNIBAL && spriteRenderer.isVisible)
            {
                animator.SetTrigger("Hype");
                Destroy(collision.gameObject);
            }

        }
        else if (EnergyModel.IsTagEnergy(collision.gameObject.tag))
        {

            //enemy is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            if (EnemyModel.GetAllowedEnergyTagsToEat(tag).Contains(collision.gameObject.tag) && spriteRenderer.isVisible)
            {
                animator.SetTrigger("Hype");
                Destroy(collision.gameObject);
            }
        }
        else if (EnemyModel.TAG_ENEMY_MONKEY == collision.gameObject.tag &&
            EnemyModel.TAG_ENEMY_MONKEY != tag)//attack monkeys by non monkeys
        {

            //enemy is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
            Destroy(collision.gameObject);
            
        }
        else if (MapModel.IsTagMap(collision.gameObject.tag))
        {

            //enemy is moving so item layer must go one layer down
            //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
            spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
        else if (BlockadeModel.IsTagBlockade(collision.gameObject.tag))
        {
            if(BlockadeModel.TAG_BLOCKADE_BUSH == collision.gameObject.tag)
                //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder + 1;
                spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder - 1;
            else
                //collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder - 1;
                spriteRenderer.sortingOrder = collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder + 1;

            if (BlockadeModel.TAG_BLOCKADE_SPIKE == collision.gameObject.tag && spriteRenderer.isVisible)
            {
                if (PlayerPrefs.GetInt("AUDIO") == 1)
                {
                    collision.transform.gameObject.GetComponent<AudioSource>().Play();

                }
                collision.gameObject.GetComponent<BlockadeController>().DelayedDestroyNoDisableSR();

                ec.StopBlinking();
                animator.enabled = false;
                isDead = true;
                //transform.localScale = transform.localScale / 3;
                //transform.position = mc.GetDestination();
                transform.position = collision.gameObject.transform.position;
                //spriteRenderer.sprite = RefrenceRepository.GetRandomBloodSprite();
               
                StartCoroutine(DelayedDestroyRoutine(collision.gameObject.GetComponent<BlockadeController>().GetDestroyClipDuration()));
            }
            
        }
    }

    protected bool IsTargetInLineOfSight(GameObject targetToCheck)
    {
        //Hit will store whatever our linecast hits when Move is called.
        RaycastHit2D[] hits;

        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = targetToCheck.transform.position;



        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        capsuleCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        //ContactFilter2D cf2d = new ContactFilter2D();
        //cf2d.SetLayerMask(blockingLayer);
        hits = Physics2D.LinecastAll(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        capsuleCollider.enabled = true;
        if (hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (BlockadeModel.IsTagBlockade(hit.transform.gameObject.tag) && !BlockadeModel.IsTagAllowedToMoveOver(hit.transform.gameObject.tag, tag))
                {
                    //If something was hit, return false, Move was unsuccesful.
                    return false;

                }
            }
        }
        return true;
    }

    private void SetTarget()
    {
        if (!spriteRenderer.isVisible)
            target = null;
        else if(EnemyModel.IsEnemyKiller(tag) && IsTargetInLineOfSight(GameObject.FindGameObjectWithTag("Player")))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
            //throw new System.Exception();

        }
        else
        {
            //check if there is allowed energy on line of sight
            List<string> energyTags = EnemyModel.GetAllowedEnergyTagsToEat(tag);
            GameObject[] items = EnergyModel.GetAllVisibleEnergyGOs(energyTags);

            if(tag == EnemyModel.TAG_ENEMY_CANNIBAL)
            {
                List<string> weaponTags = new List<string>();
                weaponTags.Add(WeaponModel.TAG_WEAPON_AXE);
                weaponTags.Add(WeaponModel.TAG_WEAPON_PICKAXE);
                weaponTags.Add(WeaponModel.TAG_WEAPON_SPEAR);


                items = items.Concat(WeaponModel.GetAllVisibleWeaponGOs(weaponTags)).ToArray();

            }

            foreach (GameObject item in items)
            {
                if (IsTargetInLineOfSight(item))
                {
                    target = item.transform;
                    break;
                }
                    
            }
        }
    }

    IEnumerator DelayedDestroyRoutine(float duration)
    {
     
        capsuleCollider.enabled = false;
      
        yield return new WaitForSeconds(duration);

        Destroy(this.gameObject);
        yield break;
    }
}