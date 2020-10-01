using System.Collections.Generic;
using UnityEngine;

public class BulletHitPool : GenericObjectPool<ParticleSystem>
{

    private List<ParticleSystem> particleList = new List<ParticleSystem>();
    private List<ParticleSystem> stoppedParticlesList = new List<ParticleSystem>();

    public override ParticleSystem GetObject()
    {

        ParticleSystem particle = base.GetObject();
        particleList.Add(particle);

        return particle;

    }

    private void Update()
    {
        
        CheckForStoppedParticles();

    }

    private void CheckForStoppedParticles()
    {
        AddParticlesToStoppedList();
        ClearStoppedListAndReturnToPool();
    }

    private void AddParticlesToStoppedList()
    {
        foreach (ParticleSystem particle in particleList)
        {
            if (particle.isStopped)
            {
                stoppedParticlesList.Add(particle);
            }
        }
    }

    private void ClearStoppedListAndReturnToPool()
    {
        foreach (ParticleSystem particle in stoppedParticlesList)
        {
            particleList.Remove(particle);
            ReturnToPool(particle);
        }

        stoppedParticlesList.Clear();
    }
}
