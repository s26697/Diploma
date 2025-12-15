using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(IAttackStrategy), true)]
public class WeaponAttackStrategyDrawer : PropertyDrawer
{
    private static Dictionary<string, Type> _typeMap;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_typeMap == null)
            BuildTypeMap();

        EditorGUI.BeginProperty(position, label, property);

        float lineHeight = EditorGUIUtility.singleLineHeight;
        float spacing = EditorGUIUtility.standardVerticalSpacing;

        Rect typeRect = new Rect(
            position.x,
            position.y,
            position.width,
            lineHeight
        );

        Rect contentRect = new Rect(
            position.x,
            position.y + lineHeight + spacing,
            position.width,
            position.height - lineHeight - spacing
        );

        string fullTypeName = property.managedReferenceFullTypename;
        string displayName = GetDisplayTypeName(fullTypeName);

        // ============================
        // TYPE DROPDOWN
        // ============================
        if (EditorGUI.DropdownButton(
                typeRect,
                new GUIContent(displayName ?? "Select Attack Strategy"),
                FocusType.Keyboard))
        {
            var menu = new GenericMenu();

            if (_typeMap.Count == 0)
            {
                menu.AddDisabledItem(new GUIContent("No Strategies Found"));
            }
            else
            {
                foreach (var kvp in _typeMap)
                {
                    string menuName = kvp.Key;
                    Type type = kvp.Value;

                    bool selected =
                        property.managedReferenceValue != null &&
                        property.managedReferenceValue.GetType() == type;

                    menu.AddItem(
                        new GUIContent(menuName),
                        selected,
                        () =>
                        {
                            property.managedReferenceValue =
                                Activator.CreateInstance(type);

                            property.serializedObject.ApplyModifiedProperties();
                        }
                    );
                }
            }

            menu.ShowAsContext();
        }

        // ============================
        // STRATEGY FIELDS
        // ============================
        if (property.managedReferenceValue != null)
        {
            EditorGUI.indentLevel++;
            EditorGUI.PropertyField(contentRect, property, GUIContent.none, true);
            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float height = EditorGUIUtility.singleLineHeight;

        if (property.managedReferenceValue != null)
        {
            height += EditorGUIUtility.standardVerticalSpacing;
            height += EditorGUI.GetPropertyHeight(property, true);
        }

        return height;
    }

    // ===================================
    // Type Discovery
    // ===================================

    private static void BuildTypeMap()
    {
        var baseType = typeof(IAttackStrategy);

        _typeMap = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(asm =>
            {
                try { return asm.GetTypes(); }
                catch { return Type.EmptyTypes; }
            })
            .Where(t =>
                !t.IsAbstract &&
                baseType.IsAssignableFrom(t))
            .ToDictionary(
                t => ObjectNames.NicifyVariableName(t.Name),
                t => t
            );
    }

    private static string GetDisplayTypeName(string fullTypeName)
    {
        if (string.IsNullOrEmpty(fullTypeName))
            return null;

        // Unity format: "AssemblyName Namespace.TypeName"
        var parts = fullTypeName.Split(' ');
        return parts.Length > 1
            ? ObjectNames.NicifyVariableName(parts[1].Split('.').Last())
            : fullTypeName;
    }
}
