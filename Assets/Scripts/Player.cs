using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : Character
{

    [SerializeField]
    private Stat health;

    [SerializeField]
    private Stat energy;

    [SerializeField]
    private float healthValue;

    [SerializeField]
    private float initHealth;

    private float initEnergy = 100;

    // Start is called before the first frame update
    protected override void Start()
    {
        health.Initialize(healthValue, initHealth);
        energy.Initialize(initEnergy, initEnergy);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        GetInput();

        base.Update();
    }

    //Listen to the Player's input
    private void GetInput()
    {

        direction = Vector2.zero;

        ///THIS IS USED FOR DEBUGGING ONLY
        ///
        if(Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            energy.MyCurrentValue -= 10;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            energy.MyCurrentValue += 10;
        }


        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            attackRoutine = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {

        if (!isAttacking)
        {
            isAttacking = true;
            myAnimator.SetBool("attack", isAttacking);

            yield return new WaitForSeconds(2); // This is a hardcoded cast time, for debugging

            Debug.Log("Attack Done");
            StopAttack();
        }
    }
}
