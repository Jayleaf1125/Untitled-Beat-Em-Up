using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;


public class HealthSystem : MonoBehaviour
{
    private float currentHealth;
    [SerializeField]
    private float maxHealth = 100.0f;
    public Slider healthbarUI;
    Animator _animator;
    SpriteRenderer _sr;

    public string hitAniParameter;
    public string deathAniParameter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;

        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //Debug.Log(healthbarUI);
        healthbarUI.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
        //if(Input.GetKeyUp(KeyCode.B))
        //{
        //    DecereaseHealth(10);
        //    return;
        //}
        
    }

    IEnumerator HitAnimationChange()
    {
        _sr.color = Color.red;
        _animator.SetBool(hitAniParameter, true);
        yield return new WaitForSeconds(0.20f);
        _animator.SetBool(hitAniParameter, false);
        _sr.color = Color.white;
    }

    IEnumerator DeathAnimationChange()
    {
        _animator.SetBool(deathAniParameter, true);
        SelectingObjectComponents();
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
        _animator.SetBool(deathAniParameter, false);
    }

    void SelectingObjectComponents()
    {
        string tag = this.gameObject.tag;

        switch(tag)
        {
            case "Player":
                GetComponent<PlayerMovement>().enabled = false;
                break;
            case "Enemy":
                GetComponent<EnemyMovement>().enabled = false;
                break;
        }
    }

    public void DecereaseHealth(float damage)
    {
        float remainingHealth = currentHealth - damage;
        StartCoroutine(HitAnimationChange());

        if (remainingHealth <= 0)
        {
            //StartCoroutine(DeathAnimationChange());
            healthbarUI.value = 0;
            Destroy(this.gameObject);
            return;
        }
        Debug.Log(string.Format("{0} health remaining", remainingHealth));
        currentHealth = remainingHealth;
        healthbarUI.value = currentHealth;
    }

    public void IncreaseHealth(float amount)
    {
        float healedHealth = currentHealth + amount;

        if (healedHealth > maxHealth)
        {
            currentHealth = maxHealth;
            healthbarUI.value = currentHealth;
            return;
        }

        currentHealth = healedHealth;
        healthbarUI.value = currentHealth;
    }
}
