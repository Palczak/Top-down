using Assets.Scripts;
using System.Linq;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private GameObject _target;
    private Movement _movement;
    private Combat _combat;
    private int _layerMask;
    private PathFinding _pathFinding;
    
    private void Start()
    {
        _layerMask = ~LayerMask.GetMask("Enemy");
        //_target = GameObject.Find("Player");
        _target = GameObject.FindGameObjectWithTag("Player");
        _movement = GetComponent<Movement>();
        _combat = GetComponent<Combat>();
        _pathFinding = new PathFinding(FindObjectOfType<GridNodes>().Nodes);
    }

    private void Update()
    {
        if (_target != null)
        {
            MoveToTarget();
            var rayHit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, Mathf.Infinity, _layerMask);
            bool isTargetObstructed = rayHit.rigidbody == _target.GetComponent<Rigidbody2D>();

            if (Config.HardMode)
            {
                var predictedPosition = PredictTargetMovement();

                rayHit = Physics2D.Raycast(transform.position, predictedPosition - transform.position, Vector3.Distance(transform.position, predictedPosition), _layerMask);
                if (rayHit.collider == null)
                {
                    rayHit = Physics2D.Raycast(predictedPosition, _target.transform.position - predictedPosition, float.PositiveInfinity, _layerMask);
                    if (rayHit.rigidbody == _target.GetComponent<Rigidbody2D>())
                    {
                        //targetedPosition = predictedPosition;
                        if(Random.value > 0.5f )
                            LockOnPositionAndShoot(predictedPosition);   
                        else   
                            LockOnPositionAndShoot(_target.transform.position, isTargetObstructed);   
                    }
                    else
                        LockOnPositionAndShoot(_target.transform.position, isTargetObstructed);
                }
                else
                    LockOnPositionAndShoot(_target.transform.position, isTargetObstructed);
            }
            else
            {
                LockOnPositionAndShoot(_target.transform.position, isTargetObstructed);
            }
            //_movement.LookAt(_target.transform.position);
            //var rayHit = Physics2D.Raycast(transform.position, _target.transform.position - transform.position, Mathf.Infinity, _layerMask);
            //rayHit.rigidbody == _target.GetComponent<Rigidbody2D>()
        }
    }

    private void LockOnPositionAndShoot(Vector3 target, bool shoot = true)
    {
        _movement.LookAt(target);
        if (shoot)
        {
            _combat.Shoot(gameObject);
        }
    }

    private void MoveToTarget()
    {
        var path = _pathFinding.FindPath(transform.position, _target.transform.position);
        if(Config.DrawPath)
        {
            var debugPath = path.ToArray();
            for (int i = 0; i < path.Count()-1; i++)
            {
                Debug.DrawRay(debugPath[i].Position, debugPath[i].Position - debugPath[i + 1].Position, Color.red, Time.deltaTime);
            }
        }
        if (path.Count() != 0)
        {
            var moveTo = new Vector2(path.Last().X, path.Last().Y);
            moveTo -= (Vector2)transform.position;
            _movement.Move(moveTo);
        }
    }

    private Vector3 PredictTargetMovement()
    {
        Vector2 targetVelocity = _target.GetComponent<Rigidbody2D>().velocity;
        float missileSpeed = _combat.Projectile.GetComponent<Movement>().Speed * 0.75f;
        float a = (targetVelocity.x * targetVelocity.x) + (targetVelocity.y * targetVelocity.y) - (missileSpeed * missileSpeed);
        float b = 2 * (targetVelocity.x * (_target.transform.position.x - gameObject.transform.position.x)
            + targetVelocity.y * (_target.transform.position.y - gameObject.transform.position.y));
        float c = ((_target.transform.position.x - gameObject.transform.position.x) * (_target.transform.position.x - gameObject.transform.position.x)) +
            ((_target.transform.position.y - gameObject.transform.position.y) * (_target.transform.position.y - gameObject.transform.position.y));
        float disc = b * b - (4 * a * c);
        float t1 = (-1 * b + Mathf.Sqrt(disc)) / (2 * a);
        float t2 = (-1 * b - Mathf.Sqrt(disc)) / (2 * a);
        float t = Mathf.Max(t1, t2);// let us take the larger time value 
        float aimX = (targetVelocity.x * t) + _target.transform.position.x;
        float aimY = _target.transform.position.y + (targetVelocity.y * t);

        var predictedPosition = new Vector3(aimX, aimY);
        return predictedPosition;
    }
}