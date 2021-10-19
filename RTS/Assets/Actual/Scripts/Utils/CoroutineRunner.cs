using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class CoroutineRunner : MonoBehaviour
    {
        public Coroutine StartCor(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }

        public void StopCor(Coroutine cor)
        {
            if (cor != null)
            {
                StopCoroutine(cor);
            }
        }

        private List<Action> actions = new List<Action>();

        public void AddFunction(Action action)
        {
            actions.Add(action);
        }

        public void RemoveFunction(Action action)
        {
            if (actions.Contains(action))
            {
                actions.Remove(action);
            }
        }

        private void Update()
        {
            for (var i = 0; i < actions.Count; i++)
            {
                actions[i].Invoke();
            }
        }
    }
}