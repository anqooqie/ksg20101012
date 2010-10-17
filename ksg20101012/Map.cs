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
using MyLibrary;

namespace ksg20101012 {
    public class Map {
        public class Back {
            public enum Status {
                Blank,
                Wall
            }

            public Status State { get; private set; }
            public ImageChip ImageChip { get; private set; }

            public Back(Status[,] status, int x, int y) {
                this.State = status[x, y];
                Point point;
                if (this.State == Status.Blank) {
                    point = new Point(0, 0);
                } else {
                    if (x == 0 && y == 0) {
                        point = new Point(2, 8);
                    } else if (x == status.GetLength(0) - 1 && y == 0) {
                        point = new Point(3, 8);
                    } else if (x == status.GetLength(0) - 1 && y == status.GetLength(1) - 1) {
                        point = new Point(0, 8);
                    } else if (x == 0 && y == status.GetLength(1) - 1) {
                        point = new Point(1, 8);
                    } else if (x == 0) {
                        if (status[x + 1, y] == Status.Wall) point = new Point(2, 9);
                        else point = new Point(2, 10);
                    } else if (y == 0) {
                        if (status[x, y + 1] == Status.Wall) point = new Point(3, 9);
                        else point = new Point(3, 10);
                    } else if (x == status.GetLength(0) - 1) {
                        if (status[x - 1, y] == Status.Wall) point = new Point(0, 9);
                        else point = new Point(0, 10);
                    } else if (y == status.GetLength(1) - 1) {
                        if (status[x, y - 1] == Status.Wall) point = new Point(1, 9);
                        else point = new Point(1, 10);
                    } else {
                        int walls = 0;
                        if (status[x - 1, y] == Status.Wall) walls++;
                        if (status[x, y - 1] == Status.Wall) walls++;
                        if (status[x + 1, y] == Status.Wall) walls++;
                        if (status[x, y + 1] == Status.Wall) walls++;
                        switch (walls) {
                            case 4:
                                point = new Point(0, 1);
                                break;
                            case 3:
                                if (status[x - 1, y] == Status.Blank) point = new Point(0, 2);
                                else if (status[x, y - 1] == Status.Blank) point = new Point(1, 2);
                                else if (status[x + 1, y] == Status.Blank) point = new Point(2, 2);
                                else point = new Point(3, 2);
                                break;
                            case 2:
                                if (status[x + 1, y] == Status.Wall && status[x, y + 1] == Status.Wall) point = new Point(0, 3);
                                else if (status[x, y + 1] == Status.Wall && status[x - 1, y] == Status.Wall) point = new Point(1, 3);
                                else if (status[x - 1, y] == Status.Wall && status[x, y - 1] == Status.Wall) point = new Point(2, 3);
                                else if (status[x, y - 1] == Status.Wall && status[x + 1, y] == Status.Wall) point = new Point(3, 3);
                                else if (status[x - 1, y] == Status.Wall && status[x + 1, y] == Status.Wall) point = new Point(0, 4);
                                else point = new Point(1, 4);
                                break;
                            case 1:
                                if (status[x + 1, y] == Status.Wall) point = new Point(0, 5);
                                else if (status[x, y + 1] == Status.Wall) point = new Point(1, 5);
                                else if (status[x - 1, y] == Status.Wall) point = new Point(2, 5);
                                else point = new Point(3, 5);
                                break;
                            default:
                                point = new Point(0, 6);
                                break;
                        }
                    }
                }
                this.ImageChip = new ImageChip(@"Images\back", point.X, point.Y);
            }
        }
        public class Enemy {
            /// <summary>
            /// 画像pxの中央
            /// </summary>
            public double RealX { get; private set; }
            /// <summary>
            /// 画像pxの中央
            /// </summary>
            public double RealY { get; private set; }
            public Direction Direction { get; private set; }
            public double Speed { get; private set; }

            public Enemy(int x, int y, Direction direction, double speed) {
                this.RealX = MyMath.Center((double)(x * Map.chipSize), Map.chipSize);
                this.RealY = MyMath.Center((double)(y * Map.chipSize), Map.chipSize);
                this.Direction = direction;
                this.Speed = speed;
            }

            public int X { get { return (int)Math.Floor(this.RealX); } }
            public int Y { get { return (int)Math.Floor(this.RealY); } }

            public void Update() {
                
            }
        }

        public const int chipSize = 24;
    }
}