using UnityEngine;

public class HeavyDrone : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float _stopYPosition = 2.0f; 
    [SerializeField] private float _horizontalSpeed = 3.0f; 
    
    [Header("Weapon Settings")]
    [SerializeField] private float _gunOffset = 0.5f;

    [Header("Screen Bounds")]
    [SerializeField] private float _minX = -8f;
    [SerializeField] private float _maxX = 8f;

    private Transform _playerTransform;

    protected override void Start()
    {
        base.Start();
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }

        _shootInterval = 2.0f; 
    }

    public override void AttackPattern()
    {
        if (transform.position.y > _stopYPosition)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }


        if (_playerTransform != null)
        {
            float targetX = _playerTransform.position.x;
            
            float newX = Mathf.MoveTowards(transform.position.x, targetX, _horizontalSpeed * Time.deltaTime);
            
            float clampedX = Mathf.Clamp(newX, _minX, _maxX);

            transform.position = new Vector3(clampedX, transform.position.y, 0);
        }

        FireDualGuns();
    }

    private void FireDualGuns()
    {
        if (Time.time > _nextShootTime)
        {
            if (_bulletPrefab != null && _firePoint != null)
            {
                SpawnBullet(-_gunOffset); 
                SpawnBullet(_gunOffset);  
            }
            _nextShootTime = Time.time + _shootInterval;
        }
    }

    private void SpawnBullet(float xOffset)
    {
      
        Vector3 spawnPos = transform.position + new Vector3(xOffset, -0.5f, 0);

        GameObject bulletObj = Instantiate(_bulletPrefab, spawnPos, Quaternion.Euler(0, 0, 180));
        
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.Init("Enemy", 15);
        }
    
    }
}