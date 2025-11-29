using UnityEngine;

public class BossCommandShip : Enemy
{
    [Header("Boss Position Settings")]
    [SerializeField] private float _entranceSpeed = 2f; 
    [SerializeField] private Vector3 _targetPos = new Vector3(0, 3.0f, 0); 

    protected override void Start()
    {
        base.Start();
        _shootInterval = 1.5f; 
    }

    public override void AttackPattern()
    {
        
        if (Vector3.Distance(transform.position, _targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _entranceSpeed * Time.deltaTime);
        }
        else
        {
            BossShoot();
        }
    }

    
    private void BossShoot()
    {
        if (Time.time > _nextShootTime)
        {
            if (_bulletPrefab != null && _firePoint != null)
            {
                SpawnBullet(0);   
                SpawnBullet(20);  
                SpawnBullet(-20); 
            }
            _nextShootTime = Time.time + _shootInterval;
        }
    }

    private void SpawnBullet(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, 180 + angle);
        GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, rotation);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Init("Enemy", 20); 
        }
    }
}