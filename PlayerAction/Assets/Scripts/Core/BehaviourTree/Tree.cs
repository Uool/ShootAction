using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node _root = null;
        protected bool isDead = false;
        protected void Start()
        {
            Init();
            _root = SetupTree();
        }

        private void Update()
        {
            if (null != _root && isDead)
                _root.Evaluate();
        }

        protected abstract void Init();
        protected abstract Node SetupTree();
    }

}
