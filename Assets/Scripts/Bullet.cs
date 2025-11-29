using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _lifeTime = 3f; 

    
    private string _ownerTag; 

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        Move();
    }


    public void Move()
    {
       
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
    }

    public void Init(string ownerTag, int damageValue)
    {
        _ownerTag = ownerTag;
        _damage = damageValue;
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
      
        if (other.CompareTag(_ownerTag)) return;

       
        Character target = other.GetComponent<Character>();

        
        if (target != null)
        {
            OnHit(target);
        }

       
        Destroy(gameObject); 
    }

    
    public void OnHit(Character target)
    {
        target.TakeDamage(_damage);
    }
}