using UnityEngine;

public enum EnemyType
{
    BasicWarShip,
    FastScout,
    HeavyDrone,
    BossCommandShip
}

public abstract class Enemy : Character
{
    [Header("Enemy Stats")]
    [SerializeField] protected EnemyType _enemyType;
    [SerializeField] protected float _shootInterval = 2f; 

    [Header("Shooting Setup")]
    [SerializeField] protected GameObject _bulletPrefab;
    [SerializeField] protected Transform _firePoint;

    protected float _nextShootTime;

    protected virtual void Start()
    {
        _nextShootTime = Time.time + Random.Range(0f, 1f);
    }

    protected virtual void Update()
    {
        AttackPattern();
    }

    
    public abstract void AttackPattern();


   
    public void Shoot()
    {
        if (Time.time > _nextShootTime)
        {
            if (_bulletPrefab != null && _firePoint != null)
            {
                GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.Euler(0, 0, 180)); 

                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.Init("Enemy", 10); 
                }
            }
            _nextShootTime = Time.time + _shootInterval;
        }
    }
    
    public override void Die()
    {
      
        Debug.Log("Score + " + _scoreValue);
        
        //เอฟเฟคเมื่อตาย อาจจะทำ
        // Instantiate(explosionPrefab, transform.position, Quaternion.identity);

       
        Destroy(gameObject);
    }
}