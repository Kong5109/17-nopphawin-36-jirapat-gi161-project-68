using UnityEngine;

public class Asteroid : Character
{
    [Header("Asteroid Specifics")]
    [SerializeField] private float _rotationSpeed = 100f; 
    [SerializeField] private int _crashDamage = 30;      

    private void Start()
    {
        _rotationSpeed = Random.Range(_rotationSpeed - 20, _rotationSpeed + 20);
        
        if (Random.value > 0.5f) _rotationSpeed *= -1;
    }

    private void Update()
    {
        Move();
    }


    public void Move()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);

       
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime, Space.World);

        if (transform.position.y < -8f)
        {
            Destroy(gameObject);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
           
            Player player = other.GetComponent<Player>();
            
            if (player != null)
            {
                OnCollidePlayer(player);
            }
        }
    }

    
    public void OnCollidePlayer(Player player)
    {
        player.TakeDamage(_crashDamage);
        Die(); 
    }

    
    public override void Die()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(ScoreValue);
        }
        Destroy(gameObject);
    }
}