using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    //Player's Movement Speed
    [SerializeField]
    private float speed;

    protected Animator myAnimator;

    //Player's Direction 
    protected Vector2 direction;

    private Rigidbody2D myRigidBody;

    protected bool isAttacking = false;

    protected Coroutine attackRoutine;

    public bool IsMoving
    {
        get
        {
            //Ciao
            return direction.x != 0 || direction.y != 0;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        //animator = GameObject.Find("Animator").GetComponent<Animator>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void HandleLayers()
    {
        if (IsMoving)
        {
            ActivateLayer("WalkLayer");

            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if (isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {
            ActivateLayer("IdleLayer");
        }
    }

    //Moves the Player
    public void Move()
    {
        //Make sure that the player moves
        myRigidBody.velocity = direction.normalized * speed;

        //x = 2, Y = 2 /// Normalize = 1, 1

    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < myAnimator.layerCount; i ++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        if( attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            Debug.Log("Attack Stopped");
            isAttacking = false;
            myAnimator.SetBool("attack", isAttacking);
        }
    }
}
