using System;

namespace Common.Utils
{
    public static class EventExtentions
    {
        public static void Call(this Action action)
        {
            if(action != null)
                action.Invoke();
        }

        public static void Call<T>(this Action<T> action, T arg)
        {
            if(action != null)
                action.Invoke(arg);
        }

        public static void Call<T, K>(this Action<T, K> action, T arg1, K arg2)
        {
            if (action != null)
                action.Invoke(arg1, arg2);
        }

        public static void Call<T, K, F>(this Action<T, K, F> action, T arg1, K arg2, F arg3)
        {
            if (action != null)
                action.Invoke(arg1, arg2, arg3);
        }

        public static Action<object> ConvertToObject<T>(this Action<T> myActionT)
        {
            if (myActionT != null)
                return o => myActionT((T) o);

            return null;
        }
    }
}
