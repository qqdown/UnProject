﻿using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace UnityStandardAssets.Utility
{
    public class ActivateTrigger : MonoBehaviour
    {
        // A multi-purpose script which causes an action to occur when
        // a trigger collider is entered.
        public enum Mode
        {
            Trigger = 0,    // Just broadcast the action on to the target
            Replace = 1,    // replace target with source
            Activate = 2,   // Activate the target GameObject
            Enable = 3,     // Enable a component
            Animate = 4,    // Start animation on target
            Deactivate = 5  // Decativate target GameObject
        }

        public Mode action = Mode.Activate;         // The action to accomplish
        public Object target;                       // The game object to affect. If none, the trigger work on this game object
        public GameObject source;
        public int triggerCount = 1;
        public bool repeatTrigger = false;
        public UnityEvent triggerEvent = new UnityEvent();
        public UnityEvent exitEvent = new UnityEvent();

        private BoxCollider boxCollider;

        private void DoActivateTrigger()
        {
            triggerCount--;

            if (triggerCount == 0 || repeatTrigger)
            {
                Object currentTarget = target ?? gameObject;
                Behaviour targetBehaviour = currentTarget as Behaviour;
                GameObject targetGameObject = currentTarget as GameObject;
                if (targetBehaviour != null)
                {
                    targetGameObject = targetBehaviour.gameObject;
                }

                switch (action)
                {
                    case Mode.Trigger:
                        triggerEvent.Invoke();
                        break;
                    case Mode.Replace:
                        if (source != null)
                        {
                            if (targetGameObject != null)
                            {
                                Instantiate(source, targetGameObject.transform.position,
                                            targetGameObject.transform.rotation);
                                DestroyObject(targetGameObject);
                            }
                        }
                        break;
                    case Mode.Activate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(true);
                        }
                        break;
                    case Mode.Enable:
                        if (targetBehaviour != null)
                        {
                            targetBehaviour.enabled = true;
                        }
                        break;
                    case Mode.Animate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.GetComponent<Animation>().Play();
                        }
                        break;
                    case Mode.Deactivate:
                        if (targetGameObject != null)
                        {
                            targetGameObject.SetActive(false);
                        }
                        break;
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            DoActivateTrigger();
        }

        private void OnTriggerExit(Collider other)
        {
            exitEvent.Invoke();
        }

        private void OnDrawGizmos()
        {
            if (boxCollider == null)
                boxCollider = GetComponent<BoxCollider>();
            Gizmos.color = new Color(1, 0, 0, 0.5F);
            Gizmos.DrawCube(transform.position, boxCollider.size);
        }
    }
}
