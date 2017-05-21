using Gamelogic.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Extensions.Editor
{
	/// <summary>
	/// A property drawer for fields marked with the NonNegative Attribute.
	/// </summary>
	[Version(1, 2)]
	[CustomPropertyDrawer(typeof (NonNegativeAttribute))]
	public class NonNegativePropertyDrawer : PropertyDrawer
	{
		public override void OnGUI(Rect position,
			SerializedProperty prop,
			GUIContent label)
		{
			switch (prop.propertyType)
			{
				case SerializedPropertyType.Integer:
				{
					EditorGUI.BeginChangeCheck();

					int n = EditorGUI.IntField(position, label, prop.intValue);

					if (EditorGUI.EndChangeCheck() && n >= 0)
						prop.intValue = n;
				}
				break;

				case SerializedPropertyType.Float:
				{
					EditorGUI.BeginChangeCheck();

					float x = EditorGUI.FloatField(position, label, prop.floatValue);

					if (EditorGUI.EndChangeCheck() && x >= 0)
						prop.floatValue = x;

				}
				break;

				default:
					EditorGUI.LabelField(position, label.text, "Use NonNegative with float or int");
				break;
			}
		}
	}
}