using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsChanger : MonoBehaviour
{
    [Serializable]
    private class ScreenControlsMapping
    {
        public int screenNumber;
        public Key up = Key.W;
        public Key down = Key.S;
        public Key left = Key.A;
        public Key right = Key.D;
    }

    [SerializeField] private InputActionReference moveActionReference;
    [SerializeField] private ScreenControlsMapping[] screenMappings = new ScreenControlsMapping[]
    {
        new ScreenControlsMapping { screenNumber = 0 },
        new ScreenControlsMapping { screenNumber = 1 },
        new ScreenControlsMapping { screenNumber = 2 },
        new ScreenControlsMapping { screenNumber = 3 },
        new ScreenControlsMapping { screenNumber = 4 }
    };

    public void OnScreenEntered(int screenNumber)
    {
        if (moveActionReference == null || moveActionReference.action == null)
        {
            Debug.LogError("No se ha asignado la acción de movimiento.", this);
            return;
        }

        ScreenControlsMapping mapping = null;
        foreach (ScreenControlsMapping candidate in screenMappings)
        {
            if (candidate != null && candidate.screenNumber == screenNumber)
            {
                mapping = candidate;
                break;
            }
        }

        if (mapping == null)
        {
            Debug.LogError($"No hay controles configurados para la pantalla {screenNumber}.", this);
            return;
        }

        Key[] keys = { mapping.up, mapping.down, mapping.left, mapping.right };
        string[] directions = { "up", "down", "left", "right" };
        foreach (Key key in keys)
        {
            if (key == Key.None)
            {
                Debug.LogError($"La configuración de controles de la pantalla {screenNumber} contiene una tecla vacía.", this);
                return;
            }
        }

        InputAction moveAction = moveActionReference.action;
        var bindingIndices = new List<int>[directions.Length];
        for (int directionIndex = 0; directionIndex < directions.Length; directionIndex++)
            bindingIndices[directionIndex] = new List<int>();

        for (int bindingIndex = 0; bindingIndex < moveAction.bindings.Count; bindingIndex++)
        {
            InputBinding binding = moveAction.bindings[bindingIndex];
            if (!binding.isPartOfComposite)
                continue;

            for (int directionIndex = 0; directionIndex < directions.Length; directionIndex++)
            {
                if (binding.name == directions[directionIndex])
                    bindingIndices[directionIndex].Add(bindingIndex);
            }
        }

        for (int directionIndex = 0; directionIndex < directions.Length; directionIndex++)
        {
            if (bindingIndices[directionIndex].Count == 0)
            {
                Debug.LogError($"La acción '{moveAction.name}' no tiene un binding compuesto para '{directions[directionIndex]}'.", this);
                return;
            }
        }

        for (int directionIndex = 0; directionIndex < directions.Length; directionIndex++)
        {
            string path = GetKeyboardPath(keys[directionIndex]);
            foreach (int bindingIndex in bindingIndices[directionIndex])
                moveAction.ApplyBindingOverride(bindingIndex, path);
        }
    }

    private static string GetKeyboardPath(Key key)
    {
        string keyName = key.ToString();
        if (keyName.StartsWith("Digit", StringComparison.Ordinal))
            keyName = keyName.Substring("Digit".Length);

        return $"<Keyboard>/{keyName.ToLowerInvariant()}";
    }
}
