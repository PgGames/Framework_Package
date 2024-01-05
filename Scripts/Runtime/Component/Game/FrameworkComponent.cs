using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace PGFrammework
{
    public abstract class FrameworkComponent : MonoBehaviour
    {
        public abstract void Init();


        protected virtual void Awake()
        {
            GameComponent.Instance.RegComponent(this);
        }
    }

}