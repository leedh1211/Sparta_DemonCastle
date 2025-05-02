using UnityEngine;
using Random = UnityEngine.Random;

public class RangeWeaponHandler : WeaponHandler
{

    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    
    [SerializeField] private int bulletIndex;
    public int BulletIndex {get{return bulletIndex;}}
    
    [SerializeField] private float bulletSize = 1;
    public float BulletSize {get{return bulletSize;}}
    
    [SerializeField] private float duration;
    public float Duration {get{return duration;}}
    
    [SerializeField] private float spread;
    public float Spread {get{return spread;}}
    
    [SerializeField] private int numberofProjectilesPerShot;
    public int NumberofProjectilesPerShot {get{return numberofProjectilesPerShot;}}
    
    [SerializeField] private float multipleProjectilesAngle;
    public float MultipleProjectilesAngle {get{return multipleProjectilesAngle;}}
    
    [SerializeField] private Color projectileColor;
    public Color ProjectileColor {get{return projectileColor;}}
    public float Size { get; set; }

    private ProjectileManager projectileManager;

    public override void Attack()
    {
        base.Attack();
        
        float projectilesAngleSpace = multipleProjectilesAngle;

        float minAngle = -(NumberofProjectilesPerShot / 2f) * projectilesAngleSpace;


        for (int i = 0; i < NumberofProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }
    
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
        if (gameObject.layer == LayerMask.NameToLayer("PlayerWeapon"))
        {
            numberofProjectilesPerShot += PlayerPrefs.GetInt("ProjectileCount", 0);
            multipleProjectilesAngle += PlayerPrefs.GetInt("ProjectileCount", 0)/2;
        }
    }
    
    private void CreateProjectile(Vector2 _lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(_lookDirection, angle));
    }
    
    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}