using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] GameObject deathVFX;
   [SerializeField] GameObject hitVFX;
   [SerializeField] Transform parent; 
   [SerializeField] int scorePerHit = 15;
   [SerializeField] int hitPoints = 4;
   ScoreBoard scoreBoard;

   void Start() 
   {
       scoreBoard=FindObjectOfType<ScoreBoard>();
       AddRigidBody();
   }
   void AddRigidBody()
   {
       Rigidbody rb = gameObject.AddComponent<Rigidbody>();
       rb.useGravity = false;
   }
   void ProcessHit()
   {
      GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
      vfx.transform.parent = parent;
      hitPoints--;
      scoreBoard.IncreaseScore(scorePerHit);
   }
   void KilEnemy()
   {
       GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
       vfx.transform.parent = parent;
       Destroy(gameObject);
   }
   void OnParticleCollision(GameObject other) 
       {
           if (hitPoints < 1)
           {
                KilEnemy();

           }
           ProcessHit(); 
       }
   
}
 