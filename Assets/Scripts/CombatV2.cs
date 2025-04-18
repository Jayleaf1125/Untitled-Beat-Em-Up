using UnityEngine;
using System.Collections;
using System.Globalization;

public class CombatV2 : MonoBehaviour
{
    Animator _animator;

    // Timer Properties
    private bool timerActive = false;
    private float timer = 0f;
    public float timeLimit;

    // Damage
    public float punchDamage = 10f;
    public float kickDamage = 5f;
    public Transform punchAttackPoint;
    public Transform kickAttackPoint;
    public float weaponRange = 1f;
    public LayerMask enemyLayer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    /*
    void Update()
    {
        PunchComboIntiate();
        KickComboIntiate();
    }
    */

    private void FixedUpdate()
    {
        //PunchComboIntiate();
        KickComboIntiate();
    }

    void KickComboIntiate()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(KickOne());
        }
    }

    IEnumerator KickOne()
    {
        _animator.SetBool("isPunchOne", true);
        yield return new WaitForSeconds(0.75f);
        _animator.SetBool("isPunchOne", false);
    }


    void PunchComboIntiate()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(!timerActive)
            {
                StartCoroutine(PunchOne());
            } else
            {
                StartCoroutine(PunchTwo());
            }
        }

        Timer();
    }

    IEnumerator PunchOne()
    {
        _animator.SetBool("isPunchOne", true);
        timerActive = true;
        timer = 0f;
        yield return new WaitForSeconds(0.5f);
        _animator.SetBool("isPunchOne", false);
    }

    IEnumerator PunchTwo()
    {
        _animator.SetBool("isPunchOne", false);
        _animator.SetBool("isPunchTwo", true);
        timerActive = false;
        yield return new WaitForSeconds(0.75f);
        _animator.SetBool("isPunchTwo", false);
    }

    void Timer()
    {
        if (!timerActive) return;

        timer += Time.fixedDeltaTime;
        if (timer >= timeLimit)
        {
            Debug.Log("Punch Combo Over");
            timerActive = false;
        }
    }

    public void DealPunchDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(punchAttackPoint.position, weaponRange, enemyLayer);
        Debug.Log(enemies[0].tag ?? "None");
        if (enemies.Length > 0) enemies[0].GetComponent<HealthSystem>().DecereaseHealth(punchDamage);
    }

    public void DealKickDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(kickAttackPoint.position, weaponRange, enemyLayer);
        Debug.Log(enemies[0].tag ?? "None");
        if (enemies.Length > 0) enemies[0].GetComponent<HealthSystem>().DecereaseHealth(kickDamage);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(punchAttackPoint.position, weaponRange);
    }

}