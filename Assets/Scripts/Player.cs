using UnityEngine;
using UnityEngine.UI;


public class Player : Character
{
    [Header("Player Specific Stats")]
    [SerializeField] private Slider hpBar;
    [SerializeField] private Image[] LiveImages;
    [SerializeField] private int _lives = 3;
    [SerializeField] private float _fireRate = 0.5f;

    [Header("Shooting Setup")] 
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firePoint;

    [Header("Screen Bounds")]
    private float _nextFireTime = 0f;

    private void Start()
    {
        hpBar.maxValue = _hp;
        UpdateHPBarValue();
        UpdateLiveImageBaseOnLive();
        if (_characterName == "") _characterName = "Player One";
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        UpdateHPBarValue();
    }

    public override void Die()
    {
        _lives--;

        if (_lives > 0)
        {
            _hp = 100; 
        }
        else
        {
            Debug.Log("GAME OVER");
            
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }

            Destroy(gameObject);
        }
        UpdateLiveImageBaseOnLive();
        UpdateHPBarValue();
    }

    public void UpdateHPBarValue()
    {
        hpBar.value = _hp;
    }

    public void UpdateLiveImageBaseOnLive()
    {
        for (int i = 0; i < LiveImages.Length; i++)
        {
            if (i < _lives)
            {
                LiveImages[i].gameObject.SetActive(true);
            }
            else
            {
                LiveImages[i].gameObject.SetActive(false);
            }
        }
    }


    public void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(direction * _moveSpeed * Time.deltaTime);
        
        Vector3 minScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        
        float offset = 0.5f; 
        
        float clampedX = Mathf.Clamp(transform.position.x, minScreenBounds.x + offset, maxScreenBounds.x - offset);
        float clampedY = Mathf.Clamp(transform.position.y, minScreenBounds.y + offset, maxScreenBounds.y - offset);
        
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }


    public void Shoot()
    {

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;

            if (_bulletPrefab != null && _firePoint != null)
            {
                GameObject bulletObj = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.Init("Player", 10);
                }
            }
        }
    }
}
