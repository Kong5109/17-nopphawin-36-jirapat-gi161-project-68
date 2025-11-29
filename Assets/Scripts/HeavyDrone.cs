using UnityEngine;

public class HeavyDrone : Enemy
{
    [Header("Tracking Settings")]
    [SerializeField] private float _horizontalSpeed = 1.5f; // ความเร็วในการไหลตามซ้ายขวา (ควรช้ากว่าบินลง)
    
    [Header("Screen Bounds")]
    [SerializeField] private float _minX = -8f;
    [SerializeField] private float _maxX = 8f;

    private Transform _playerTransform;

    protected override void Start()
    {
        base.Start();
        
        // ค้นหาตัวผู้เล่นในฉากเพื่อเอาตำแหน่งมาใช้
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }

        // HeavyDrone ยิงช้าแต่หนักแน่น
        _shootInterval = 3.0f; 
    }

    public override void AttackPattern()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        if (_playerTransform != null)
        {
            float targetX = _playerTransform.position.x;
            
            float newX = Mathf.MoveTowards(transform.position.x, targetX, _horizontalSpeed * Time.deltaTime);
            
            float clampedX = Mathf.Clamp(newX, _minX, _maxX);

            transform.position = new Vector3(clampedX, transform.position.y, 0);
        }
        else
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null) _playerTransform = playerObj.transform;
        }

        Shoot();

        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}