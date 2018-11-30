using UnityEngine;

public class Entry<T> : MonoBehaviour
    where T : Named {
    public T Data;
}
