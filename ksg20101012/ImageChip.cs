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

    public struct ImageChip {
        public static Dictionary<string, Rectangle> chipDatas;

        public string asset;
        public int x;
        public int y;
        public int width;
        public int height;

        public ImageChip(string asset, int x, int y) {
            if (ImageChip.chipDatas.ContainsKey(asset)) {
                this.asset = asset;
                this.width = ImageChip.chipDatas[asset].Width;
                this.height = ImageChip.chipDatas[asset].Height;
                this.x = x * this.width;
                this.y = y * this.height;
            } else {
                throw new Exception("このコンストラクタを使うためには予めImageChip.Bind(\""+asset+"\")しておく必要があります。");
            }
        }
        public ImageChip(string asset, int x, int y, int width, int height) {
            this.asset = asset;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Texture2D Texture2D {
            get { return CachedContent.Get<Texture2D>(this.asset); }
        }
        public Rectangle Rectangle {
            get { return new Rectangle(this.x, this.y, this.width, this.height); }
        }

        public void Bind(string asset, int width, int height) {
            ImageChip.chipDatas[asset] = new Rectangle(0, 0, width, height);
        }
    }
}