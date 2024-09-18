using System.Collections.Generic;
using UnityEngine;

    public class ClippingPlane : MonoBehaviour
    {
        public List<Material> mats;
        public float triggerPlaneClipping = 1f;
        public bool clipAbovePlane = true;

        private Plane plane;
        private Vector4 planeRepresentation;

        void Start()
        {
            plane = new Plane(transform.up, transform.position);
            planeRepresentation = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);

            refresh();
        }

        private void refresh()
        {
            // refresh for clipping
            foreach (Material mat in mats)
            {
                mat.SetVector("_Plane", planeRepresentation);
                mat.SetFloat("_TriggerPlaneClipping", triggerPlaneClipping);
                mat.SetFloat("_ClipAbovePlane", (clipAbovePlane ? 1f : 0f));
            }
        }

    }

