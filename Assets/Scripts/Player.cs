using UnityEngine;


public class Player : Character
{
    [Header("Player Specific Stats")] [SerializeField]
    private int _lives = 3;

    [SerializeField] private float _fireRate = 0.5f;

    [Header("Shooting Setup")] [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField] private Transform _firePoint;

    private float _nextFireTime = 0f;

    private void Start()
    {

        if (_characterName == "") _characterName = "Player One";
    }

    private void Update()
    {
        Move();
        Shoot();
    }


    public override void Die()
    {
        _lives--;
        Debug.Log(_characterName + " Died! Lives remaining: " + _lives);

        if (_lives > 0)
        {
            _hp = 100;
        }
        else
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }



    public void Move()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        transform.Translate(direction * _moveSpeed * Time.deltaTime);

    }


    public void Shoot()
    {

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;

            if (_bulletPrefab != null && _firePoint != null)
            {
                GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

                // --- ส่วนที่เพิ่มเข้ามา ---
                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    // ส่งข้อมูลไปบอกกระสุนว่า "Player เป็นคนยิงนะ" และ "ดาเมจเท่าไหร่นะ"
                    bulletScript.Init("Player", 10);
                }
            }
        }
    }
}
