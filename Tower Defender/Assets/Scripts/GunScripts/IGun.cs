using System;

public interface IGun
{    

    float NextShotTime{get; set;}
    bool CanShoot { get; set; }

    void Shoot();

}
