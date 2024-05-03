using UnityEngine;

namespace Codebase.Infrastructure
{
    public class RandomService
    {
        private readonly int _seed = 1;
        private readonly System.Random _random;

        public RandomService()
        {
            _random = new System.Random(_seed);
        }

        public float Range(float min, float max)
        {
            return min + (max - min) * (float)_random.NextDouble();
        }

        public Vector3 Range(Vector3 min, Vector3 max)
        {
            float x = Range(min.x, max.x);
            float y = Range(min.y, max.y);
            float z = Range(min.z, max.z);

            return new Vector3(x, y, z);
        }

        public Color GetColor()
        {
            float r = (float)_random.NextDouble();
            float g = (float)_random.NextDouble();
            float b = (float)_random.NextDouble(); 
            
            return new Color(r, g, b);
        }
    }
}
