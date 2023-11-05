namespace Infrastructure.Random
{
    public static class Randomizer
    {
        public static int Range(int min, int max)
        {
            return new System.Random().Next(min, max);
        }

        public static float Range(float min, float max)
        {
            var random = new System.Random();
            return (float)random.NextDouble() * (max - min) + min;
        }
    }
}