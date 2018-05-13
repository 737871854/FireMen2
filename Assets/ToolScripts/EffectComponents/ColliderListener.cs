using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Qtz.Q5.Battle
{
    public class ColliderListener : MonoBehaviour
    {
        public delegate void OnTriggerEnterListener(Collider c);
        public delegate void OnTriggerExitListener(Collider c);
        public delegate void OnTriggerStayListener(Collider c);

        public void Start()
        {
        }

        void OnTriggerEnter(Collider c)
        {
            if (_onTriggerEnterListener != null)
            {
                _onTriggerEnterListener(c);
            }
        }

        void OnTriggerExit(Collider c)
        {
            if (_onTriggerExitListener != null)
            {
                _onTriggerExitListener(c);
            }
        }

        void OnTriggerStay(Collider c)
        {
            if (_onTriggerStayListener != null)
            {
                _onTriggerStayListener(c);
            }
        }

        public void EnrollBulletEnter(OnTriggerEnterListener onTriggerEnter)
        {
            _onTriggerEnterListener +=  onTriggerEnter;
        }

        public void EnrollBulletExit(OnTriggerExitListener onTriggerExit)
        {
            _onTriggerExitListener += onTriggerExit;
        }

        public void EnrollBulletStay(OnTriggerStayListener onTriggerStay)
        {
            _onTriggerStayListener += onTriggerStay;
        }

        private OnTriggerEnterListener _onTriggerEnterListener = null;
        private OnTriggerExitListener _onTriggerExitListener = null;
        private OnTriggerStayListener _onTriggerStayListener = null;
    }
}
