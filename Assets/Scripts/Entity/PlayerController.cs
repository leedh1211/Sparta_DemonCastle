using UnityEngine;

public class PlayerController : BaseController
{
    private GameManager gameManager;
    private Camera camera;
    


    protected override void Awake()
    {
        base.Awake();
        
        if (camera == null)
        {
            camera = Camera.main;            
        }
    }

    public void Init(GameManager gameManager)
    {
        Debug.Log("playerInit시점"+isInMainGame);
        if (isInMainGame)
        {
            this.gameManager = gameManager;    
        }
        
    }
    

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        if (isInMainGame)
        {
            Vector2 mousePosition = Input.mousePosition;
            Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
            lookDirection = (worldPos - (Vector2)transform.position);

            if (lookDirection.magnitude < .9f)
            {
                lookDirection = Vector2.zero;
            }
            else
            {
                lookDirection = lookDirection.normalized;
            }
        
            isAttacking = Input.GetMouseButton(0);    
        }
    }
    
    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }
}