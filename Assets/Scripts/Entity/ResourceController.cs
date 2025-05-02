using System;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private statHandler statHandler;
    private AnimationHandler animationHandler;
    private Action<float, float> OnChangeHealth;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.Health;

    protected virtual void Awake()
    {
        statHandler = GetComponent<statHandler>();
        animationHandler = GetComponentInChildren<AnimationHandler>();
        baseController = GetComponent<BaseController>();
    }

    protected virtual void Start()
    {
        CurrentHealth = statHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;

        float bonusMax = MaxHealth + PlayerPrefs.GetInt("MaxStack", 0) * 5;

        if (CurrentHealth > bonusMax)
        {
            CurrentHealth = bonusMax;
        }

        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        OnChangeHealth?.Invoke(CurrentHealth, MaxHealth);

        if (change < 0)
        {
            animationHandler.Damage();
        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death();
    }
    
    public void AddHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth += action;
    }

    public void RemoveHealthChangeEvent(Action<float, float> action)
    {
        OnChangeHealth -= action;
    }
}