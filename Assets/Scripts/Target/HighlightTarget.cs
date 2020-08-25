using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Target
    {

        public class HighlightTarget : TargetBehaviour
        {

            private List<Material> materials;
            bool isOn = false;

            private static int highlightId = Shader.PropertyToID("_HighlightEnabled");

            private void Awake()
            {
                materials = new List<Material>();
                foreach (Renderer r in GetComponentsInChildren<Renderer>())
                {
                    materials.AddRange(r.materials);
                }
            }

            public override void OnTargetGazed()
            {
                if (!isOn)
                {
                    foreach (Material m in materials)
                    {
                        m.SetFloat(highlightId, 1.0f);
                    }
                    isOn = true;
                }
            }

            public override void OnTargetUngazed()
            {
                if (isOn)
                {
                    foreach (Material m in materials)
                    {
                        m.SetFloat(highlightId, 0.0f);
                    }
                    isOn = false;
                }
            }

        }
    }
}
