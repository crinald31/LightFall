using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DamageHealth : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;

    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private float invincibilityTime = 0.25f;

    private float hitTime = 0f;
    private Animator animator;

    private Slider healthSlider;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            animator.SetBool("isAlive", value);
            Debug.Log("IsAlive set " + value);
        }
    }

    public bool Lock
    {
        get { return animator.GetBool("lockVelocity"); }
    }

    public void HealthBar()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Slider").GetComponent<Slider>();
        }
        if (gameObject.tag == "Player")
        {
            healthSlider.maxValue = MaxHealth;
            healthSlider.value = Health;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        HealthBar();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (hitTime > invincibilityTime)
            {
                isInvincible = false;
                hitTime = 0;
            }
            hitTime += Time.deltaTime;
        }
        Dead();
    }

    public void Heal(int restore)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            Health += Mathf.Min(maxHealth, restore);
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            HealthBar();
            isInvincible = true;
            animator.SetTrigger("hit");
            damageableHit.Invoke(damage, knockback);
            return true;
        }
        return false;
    }

    public void Dead()
    {
        if (!IsAlive && (gameObject.tag == "Player"))
        {
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("SampleScene");
    }
}
