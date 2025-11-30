using UnityEngine;


public abstract class Character : MonoBehaviour
{
    [Header("Base Character Stats")]
    
        
    [SerializeField] protected string _characterName;
    [SerializeField] protected int _hp;
    [SerializeField] protected float _moveSpeed; 
    [SerializeField] protected int _scoreValue;

   
    public string CharacterName { get { return _characterName; } }
    public int HP { get { return _hp; } }
    public int ScoreValue { get { return _scoreValue; } }
    
    public virtual void TakeDamage(int damage)
    {
        _hp -= damage;
        
        //Debug.Log(_characterName + " took " + damage + " damage. Current HP: " + _hp);

        if (_hp <= 0)
        {
            Die();
        }
    }

    
    public abstract void Die();
}