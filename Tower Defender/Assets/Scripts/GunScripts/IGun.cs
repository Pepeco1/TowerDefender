using System;

public interface IGun
{    

    float ShootingDelay{get; set;}
    float NextShotTime{get; set;}
    bool CanShoot { get; set; }

    void Shoot();

}
