using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
   protected Rigidbody2D rb;
   [HideInInspector] protected SpriteRenderer sr;

   protected virtual void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
   }

   protected virtual void Update()
   {
      
   }

   protected virtual void FixedUpdate()
   {
      
   }
   
   protected  virtual void LateUpdate()
   {
      
   }
}
