using UnityEngine;
using UnityEngine.Events;
using System;

namespace Xivol.Input
{
    [RequireComponent(typeof(Collider))]
    public class Clickable3D: Core<Clickable3D>
    {
        [Serializable]
        public class SerializedEvent : UnityEvent<Vector3> { }

        public SerializedEvent MouseClickEvent;

        protected void OnMouseDown()
        {
            Debug.Log(name + " Mouse Down");
        }

        protected void OnMouseUp()
        {
            Debug.Log(name + " Mouse Up");
        }

        protected void OnMouseUpAsButton()
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(ray, out hit) && 
                hit.collider == GetComponent<Collider>())
            {
                Debug.Log(name + "Mouse Click");
                MouseClickEvent.Invoke(hit.point);
            }
        }

    }
}
