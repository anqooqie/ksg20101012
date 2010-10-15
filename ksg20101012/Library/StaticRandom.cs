using System;
using System.Collections.Generic;
using System.Linq;

namespace MyLibrary {
    /// <summary>
    /// プログラム全体で一つのシードを使う乱数
    /// </summary>
    public static class StaticRandom {
        private static Random random = new Random();
        public static int Next() {
            return random.Next();
        }
        public static int Next(int maxValue) {
            return random.Next(maxValue);
        }
        public static int Next(int minValue, int maxValue) {
            return random.Next(minValue, maxValue);
        }
        public static void NextBytes(byte[] buffer) {
            random.NextBytes(buffer);
        }
        public static double NextDouble() {
            return random.NextDouble();
        }
    }
}