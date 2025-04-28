using System;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
   protected Rigidbody2D rb;
   [HideInInspector] protected SpriteRenderer sr;
   [HideInInspector] protected Animator anim;

   protected virtual void Awake()
   {
      rb = GetComponent<Rigidbody2D>();
      sr = GetComponent<SpriteRenderer>();
      anim = GetComponent<Animator>();
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

   protected virtual void Dead()
   {
      
   }
}
