using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;


namespace MyLibrary {
    public static class MyMath {
        public static double Center(double upperLeft, int length) {
            return MyMath.Center(upperLeft, length);
        }
        public static double Center(double upperLeft, double length) {
            return upperLeft + length / 2;
        }
        public static int Center(int upperLeft, int length) {
            if (length % 2 == 1) { //奇数
                return upperLeft + (length - 1) / 2;
            } else { //偶数
                return upperLeft + (length / 2 - 1);
            }
        }
        public static double UpperLeft(double center, int length) {
            return MyMath.UpperLeft(center, length);
        }
        public static double UpperLeft(double center, double length) {
            return center - length / 2;
        }
        public static int UpperLeft(int center, int length) {
            if (length % 2 == 1) { //奇数
                return center - (length - 1) / 2;
            } else { //偶数
                return center - (length / 2 - 1);
            }
        }
    }
}