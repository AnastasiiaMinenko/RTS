using UnityEngine.Events;

[System.Serializable]
public class VoidEvent : UnityEvent
{


}

[System.Serializable]
public class BoolEvent : UnityEvent<bool>
{


}
[System.Serializable]
public class IntEvent : UnityEvent<int>
{

}
[System.Serializable]
public class StringEvent : UnityEvent<string>
{

}