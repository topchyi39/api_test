using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Api
{
    [Serializable]
    public class ApiButton
    {
        public int id;
        public string color;
        public bool animationType;
        public string text;

        public static string GetRandomColor()
        {
            var r = Random.Range(0f, 1f);
            var g = Random.Range(0f, 1f);
            var b = Random.Range(0f, 1f);
            return $"#{ColorUtility.ToHtmlStringRGB(new Color(r, g, b)).ToLower()}";
        }
    }
}