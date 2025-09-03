using UnityEngine;

namespace UI
{
    public class KeyCell : MonoBehaviour
    {
        public Key Key { get; private set; }

        public void SetKey(Key key)
        {
            Key = key;
            Key.transform.SetParent(transform, false);
        }
    }
}
