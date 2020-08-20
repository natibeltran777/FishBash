using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace FishBash
{
    public class OffsetPropertyDrawer : OdinValueDrawer<Offset>
    {

        protected override void DrawPropertyLayout(GUIContent label)
        {
			Rect rect = EditorGUILayout.GetControlRect();

			if (label != null)
			{
				rect = EditorGUI.PrefixLabel(rect, label);
			}

			Offset value = this.ValueEntry.SmartValue;
			Transform t = BatHandler.Instance.transform.GetChild(0); // Gets transform of the bat
			GUIHelper.PushLabelWidth(20);
			value.label = EditorGUI.TextField(rect.AlignLeft(rect.width * 0.25f), value.label);
			if(GUI.Button(rect.AlignCenter(rect.width * 0.25f), new GUIContent("Apply Offset")))
            {
				value.ApplyTransform(t);
            }
			if (GUI.Button(rect.AlignRight(rect.width * 0.25f), new GUIContent("Save Offset")))
			{
				value = value.GetTransform(t);
			}
			GUIHelper.PopLabelWidth();

			this.ValueEntry.SmartValue = value;
		}

    }
}