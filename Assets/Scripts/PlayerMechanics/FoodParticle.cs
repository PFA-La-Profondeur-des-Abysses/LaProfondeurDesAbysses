using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodParticle : MonoBehaviour
{
    ParticleSystem ps;

    // these lists are used to contain the particles which match
    // the trigger conditions each frame.
    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleTrigger()
    {
        // get the particles which matched the trigger conditions this frame
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter, out var other);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        
        var collider = ps.trigger.GetCollider(0);
        //var obj = other.GetCollider(0, 0);

        if (!collider) return;
        if (!collider.gameObject.CompareTag("Fish")) return;
        //collider.transform.parent.GetComponent<DosBleu>().LoseFood(exit);
        //collider.transform.parent.GetComponent<DosBleu>().SpotFood(enter);



        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}
