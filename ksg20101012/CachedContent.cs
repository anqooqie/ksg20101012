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
    /// Content.Loadのキャッシングを行う静的クラス
    /// 使用例：Texture2D texture2D = CachedContent.Get&lt;Texture2D&gt;(@"Images\foobar");
    /// </summary>
    public static class CachedContent {
        private static Dictionary<string, object> contents = new Dictionary<string, object>();
        public static T Get<T>(string asset) {
            if (!CachedContent.contents.ContainsKey(asset)) {
                CachedContent.Load<T>(asset);
            }
            return (T)CachedContent.contents[asset];
        }
        public static void Load<T>(string asset) {
            CachedContent.contents[asset] = Global.game.Content.Load<T>(asset);
        }
    }
}