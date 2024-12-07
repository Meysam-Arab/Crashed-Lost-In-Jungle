using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{

    protected Vector3 destination;
    protected int direction;

    [SerializeField] private float moveSpeed;

    private float speed;

    [HideInInspector]public bool onMove;
    private EntityController entityController;

    void Awake()
    {
        destination = Vector3.zero;
        direction = 0;
        speed = 0;

        onMove = false;

        entityController = gameObject.GetComponent<EntityController>();
    }



    // Update is called once per frame
    void Update()
    {

        if (onMove)
        {
            if (Mathf.Abs(destination.x - entityController.gameObject.transform.position.x) < 0.05 &&
                Mathf.Abs(destination.y - entityController.gameObject.transform.position.y) < 0.05)
            {
                StopMove();
            }
            else
                Move();
        }

    }

    /// <summary>
    /// start moving
    /// </summary>
    /// <param name="newDes">destination to move to</param>
    public void StartMove(Vector3 newDes)
    {
      
        destination = newDes;
        speed = moveSpeed;
        onMove = true;
    }

    /// <summary>
    /// for stopping all movements
    /// </summary>
    public void StopMove()
    {
        entityController.gameObject.transform.position = destination;
        onMove = false;
    }

    public void Move()
    {

        Vector3 direction = (new Vector3(destination.x, destination.y, 0f) - entityController.transform.position).normalized;
        direction.z = 0f;


        entityController.transform.position = new Vector3(entityController.transform.position.x + (direction.x * speed * Time.deltaTime), entityController.transform.position.y + (direction.y * speed * Time.deltaTime), 0f);

    }

    public Vector3 GetDestination()
    {
        return destination;
    }
}
