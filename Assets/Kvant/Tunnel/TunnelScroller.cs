//
// Scroller script for Tunnel
//
using UnityEngine;

namespace Kvant
{
    [RequireComponent(typeof(Tunnel))]
    [AddComponentMenu("Kvant/Tunnel Scroller")]
    public class TunnelScroller : MonoBehaviour
    {
        [SerializeField]
        float _speed;

        bool forwards = true;

        public float speed {
            get { return _speed; }
            set { _speed = value; }
        }

        void Update()
        {
            if (!forwards)
                GetComponent<Tunnel>().offset += _speed * Time.deltaTime;
            else
                GetComponent<Tunnel>().offset -= _speed * Time.deltaTime;
        }
    }
}
