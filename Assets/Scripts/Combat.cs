using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Projectile Projectile;

    // Start is called before the first frame update
    private void Start()
    {
           
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void Shoot(GameObject shooter)
    {
        Projectile projectile = Instantiate(Projectile);
        projectile.Shooter = shooter;
    }
}
