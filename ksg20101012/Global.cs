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
    /// <summary>
    /// 奥の手のグローバル変数
    /// </summary>
    public static class Global {
        public static Game1 game;
        public static void Dispose() {
            game = null;
        }
    }
}