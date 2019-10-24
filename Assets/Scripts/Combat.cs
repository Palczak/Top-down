using UnityEngine;

public class Combat : MonoBehaviour
{
    public Projectile Projectile;
    public uint FireRateRPM;
    public int MaxHitPoints;
    [SerializeField]
    private int _currentHitPoints;
    [SerializeField]
    private float _pauseBetweenShots;
    [SerializeField]
    private float _shotTimer;

    private void Start()
    {
        _pauseBetweenShots = 60f / FireRateRPM;
        _shotTimer = 0;

        _currentHitPoints = MaxHitPoints;
    }

    private void Update()
    {
        if (_shotTimer <= _pauseBetweenShots)
        {
            _shotTimer += Time.deltaTime;
        }
    }

    public void TakeHit(int damage)
    {
        _currentHitPoints = _currentHitPoints - damage;
        if(_currentHitPoints == 0)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(GameObject shooter)
    {   
        if(_shotTimer >= _pauseBetweenShots)
        {
            Projectile projectile = Instantiate(Projectile);
            projectile.Shooter = shooter;
            _shotTimer = 0;
        }
    }
}
