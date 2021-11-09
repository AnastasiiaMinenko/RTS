using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineRunnerExt
{
    public static IEnumerator Move(Transform transform, Vector3 startPos, Vector3 endPos, float duration, object data = null, Action<object> onComplete = null)
    {
        var time = 0f;
        while (time < 1)
        {
            time += Time.deltaTime / duration;
            transform.position = Vector3.Lerp(startPos, endPos, time);
            yield return null;
        }

        onComplete?.Invoke(data);
    }
}