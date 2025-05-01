using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;
    
    [SerializeField] private float followRange = 15f;

    private float targetUpdateTimer = 0;
    private float targetUpdateCooldown = 1f;
    
    
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        targetUpdateTimer -= Time.deltaTime;
        if (targetUpdateTimer <= 0f)
        {
            UpdateTarget();
            targetUpdateTimer = targetUpdateCooldown;
        }

        if (weaponHandler == null || target == null)
        {
            if(!movementDirection.Equals(Vector2.zero)) movementDirection = Vector2.zero;            
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;
        if (distance <= followRange)
        {
            lookDirection = direction;
            
            if (distance <= weaponHandler.AttackRange)
            {
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                
                movementDirection = Vector2.zero;
                return;
            }
            
            movementDirection = direction;
        }

    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    protected void UpdateTarget()
    {
        
        Vector2 center = transform.position;
        float radius = weaponHandler.AttackRange;
        LayerMask playerMask = LayerMask.GetMask("Player");
        
        Collider2D[] playerList = Physics2D.OverlapCircleAll(center, radius, playerMask);
            
        float closestDistance = Mathf.Infinity;
        Collider2D closestPlayer = null;

        foreach (var targetObj in playerList)
        {
            float targetDistance = Vector2.Distance(transform.position, targetObj.transform.position);
            if (targetDistance < closestDistance)
            {
                closestDistance = targetDistance;
                closestPlayer = targetObj;
            }
        }

        if (closestPlayer != null)
        {
            target = closestPlayer.gameObject.transform;    
        }
    }
    
    public override void Death()
    {
        base.Death();
        if (enemyManager != null)
        {
            enemyManager.RemoveEnemyOnDeath(this);
        }
    }
}
