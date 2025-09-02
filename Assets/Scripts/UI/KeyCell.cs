using UnityEngine;

namespace UI
{
    public class KeyCell : MonoBehaviour
    {
        private Key _key;

        public void SetKey(Key key)
        {
            _key = key;
            _key.transform.SetParent(transform, false);
        }
    }
}
