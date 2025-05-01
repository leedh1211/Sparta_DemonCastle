using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] public WeaponHandler WeaponPrefab;
    
    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection{get{return movementDirection;}} 
    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection{get{return lookDirection;}} 

    private Vector2 knockback = Vector2.zero; // 
    private float knockbackDuration = 0.0f;
    
    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;
    protected bool isInMainGame = false;
    protected int MaxWave;
    protected int MaxStack;
    protected int ProjectileCount;
    protected int AttackSpeed;
    protected int Power;

    
    protected AnimationHandler animationHandler;
    protected statHandler statHandler;
    protected WeaponHandler weaponHandler;

    protected virtual void Awake()
    {
        MaxWave = PlayerPrefs.GetInt("MaxWave", 0);
        MaxStack = PlayerPrefs.GetInt("MaxStack", 0);
        ProjectileCount = PlayerPrefs.GetInt("ProjectileCount", 0);
        AttackSpeed = PlayerPrefs.GetInt("AttackSpeed", 0);
        Power = PlayerPrefs.GetInt("Power", 0);
        isInMainGame = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainGameScene";
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        if (isInMainGame)
        {
            statHandler = GetComponent<statHandler>();
            if(WeaponPrefab != null)
                weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            else
                weaponHandler = GetComponentInChildren<WeaponHandler>();    
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsDialogueActive())
        {
            movementDirection = Vector2.zero;
            return;
        }
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();
    }

    protected virtual void FixedUpdate()
    {
        Movment(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }
    


    protected virtual void HandleAction()
    {
        
    }
    

    private void Movment(Vector2 direction)
    {
        direction = direction * 5;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler?.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;
        
        
        if (isLeft)
        {
            characterRenderer.flipX = true;
            weaponPivot.localPosition = new Vector3(-0.3f, 0f, 0f);
        }
        else
        {
            weaponPivot.localPosition = new Vector3(0.3f, 0f, 0f);
        }
        
        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        
        weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized * power;
    }
    
    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;

        float attackSpeed = PlayerPrefs.GetFloat("AttackSpeed", 1f); // 기본값 1

        float speedMultiplier = 1f + (attackSpeed - 1f) * 0.2f; // (0.2f는 보정값)

        float actualDelay = weaponHandler.Delay / speedMultiplier;

        if (timeSinceLastAttack <= actualDelay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > actualDelay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }
    
    protected virtual void Attack()
    {
        if(lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }
    
    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
    
    protected void UpdateMaxWave(int currentWave)
    {
        if (currentWave > MaxWave)
        {
            PlayerPrefs.SetInt("MaxWave", currentWave);
            PlayerPrefs.Save();
        }
    }

    protected void UpdateMaxStack(int currentStack)
    {
        if (currentStack > MaxStack)
        {
            PlayerPrefs.SetInt("MaxStack", currentStack);
            PlayerPrefs.Save();
        }
    }
    
}