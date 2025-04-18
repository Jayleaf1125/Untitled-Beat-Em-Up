using System.Collections;
using UnityEngine;

public class PlayerAttackRange : MonoBehaviour
{
    public bool isInAttackRange = false;
    public float weaponRange = 1f;
    public Transform attackPoint;
    public LayerMask playerLayer;

     Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //DealEnemyPunchDamage();
    }

    private void FixedUpdate()
    {
        if(isInAttackRange)
        {
            StartCoroutine(DamageAnimation());
        }
    }

    IEnumerator DamageAnimation()
    {
        _animator.SetBool("isEnemyAttack", true);
        yield return new WaitForSeconds(0.30f);
        _animator.SetBool("isEnemyAttack", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { 
            GetComponent<FollowPlayer>().enabled = false; 
            isInAttackRange = true;
        } 
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<FollowPlayer>().enabled = true;
            isInAttackRange = false;
        } 
    }

    public void DealEnemyPunchDamage()
    {
        if (!isInAttackRange) return;

        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.position, weaponRange, playerLayer);
        Debug.Log(enemy.Length);
        
    }

    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }
    */

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.magenta;
    //    Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    //}
}
