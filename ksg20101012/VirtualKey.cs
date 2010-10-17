﻿using System;
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
    /// 仮想キーを扱う静的クラス
    /// </summary>
    public static class VirtualKey {
        /// <summary>
        /// 仮想キーの状態
        /// </summary>
        public struct Status {
            public bool isPressed;
            public int pressFrame;

            public Status(bool isPressed, int pressFrame) {
                this.isPressed = isPressed;
                this.pressFrame = pressFrame;
            }

            public TimeSpan PressTime {
                get { return new TimeSpan((long)((double)(this.pressFrame * 1000 * 10000) / 60d)); }
            }
        }
        /// <summary>
        /// 仮想キーの種類
        /// </summary>
        public enum Keys {
            Left,
            Up,
            Right,
            Down,
            Decide,
            Cancel,
            Quit
        }

        private static Dictionary<Keys, Status> currentStatus = new Func<Dictionary<Keys, Status>>(() => {
            var result = new Dictionary<Keys, Status>();
            foreach (Keys key in Keys.GetValues(typeof(Keys))) {
                result[key] = new Status(false, 0);
            }
            return result;
        })();
        private static Dictionary<Keys, Status> previousStatus = new Func<Dictionary<Keys, Status>>(() => {
            var result = new Dictionary<Keys, Status>();
            foreach (Keys key in Keys.GetValues(typeof(Keys))) {
                result[key] = new Status(false, 0);
            }
            return result;
        })();
        private static Dictionary<Keys, List<int>> reserves = new Func<Dictionary<Keys, List<int>>>(() => {
            var result = new Dictionary<Keys, List<int>>();
            foreach (Keys key in Keys.GetValues(typeof(Keys))) {
                result[key] = new List<int>();
            }
            return result;
        })();

        //シンタックスシュガーたち
        public static Status Left { get { return VirtualKey.GetState(Keys.Left); } }
        public static Status Up { get { return VirtualKey.GetState(Keys.Up); } }
        public static Status Right { get { return VirtualKey.GetState(Keys.Right); } }
        public static Status Down { get { return VirtualKey.GetState(Keys.Down); } }
        public static Status Decide { get { return VirtualKey.GetState(Keys.Decide); } }
        public static Status Cancel { get { return VirtualKey.GetState(Keys.Cancel); } }
        public static Status Quit { get { return VirtualKey.GetState(Keys.Quit); } }

        /// <summary>
        /// 仮想キーの状態を取得する。
        /// </summary>
        /// <param name="key">仮想キーの種類</param>
        /// <returns>仮想キーの状態</returns>
        public static Status GetState(Keys key) {
            return VirtualKey.currentStatus[key];
        }
        public static Status GetState(Direction direction) {
            return VirtualKey.GetState((Keys)direction);
        }
        private static void UpdateChild(Keys key, bool isPressed) {
            //プログラムからの入力命令があったらisPressedをtrue
            List<int> reserves = VirtualKey.reserves[key];
            for (int i = 0; i < reserves.Count; i++) {
                reserves[i]--;
            }
            if (reserves.Count > 0 && reserves[0] == 0) {
                reserves.RemoveAt(0);
                isPressed = true;
            }

            //previousStatesにstatesをバックアップ
            VirtualKey.previousStatus[key] = VirtualKey.currentStatus[key];

            //isPressedを元にstatesの更新処理
            if (VirtualKey.previousStatus[key].isPressed) {
                if (isPressed) {
                    VirtualKey.currentStatus[key] = new Status(VirtualKey.currentStatus[key].isPressed, VirtualKey.currentStatus[key].pressFrame + 1);
                } else {
                    VirtualKey.currentStatus[key] = new Status(false, 0);
                }
            } else {
                if (isPressed) {
                    VirtualKey.currentStatus[key] = new Status(true, 1);
                }
            }
        }
        /// <summary>
        /// 毎フレーム更新するべし。
        /// </summary>
        public static void Update() {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            VirtualKey.UpdateChild(Keys.Left, gamePadState.DPad.Left == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left));
            VirtualKey.UpdateChild(Keys.Up, gamePadState.DPad.Up == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up));
            VirtualKey.UpdateChild(Keys.Right, gamePadState.DPad.Right == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right));
            VirtualKey.UpdateChild(Keys.Down, gamePadState.DPad.Down == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down));
            VirtualKey.UpdateChild(Keys.Decide, gamePadState.Buttons.A == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z) || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter));
            VirtualKey.UpdateChild(Keys.Cancel, gamePadState.Buttons.B == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.X) || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift) || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightShift));
            VirtualKey.UpdateChild(Keys.Quit, gamePadState.Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape));
        }
        /// <summary>
        /// reserveフレーム後にキー入力の予約をする。
        /// </summary>
        /// <param name="key">予約するキー</param>
        /// <param name="reserve">予約するフレーム</param>
        public static void Reserve(Keys key, int reserve) {
            List<int> reserves = VirtualKey.reserves[key];

            var range = new { start = 0, end = reserves.Count };
            while (range.end - range.start > 0) {
                int targetIndex = range.start + (range.end - range.start) / 2;
                if (reserve < reserves[targetIndex]) {
                    range = new { start = range.start, end = targetIndex };
                } else if (reserves[targetIndex] < reserve) {
                    range = new { start = targetIndex + 1, end = range.end };
                } else {
                    return;
                }
            }
            reserves.Insert(range.start, reserve);
        }
        public static void Reserve(Direction direction, int reserve) {
            VirtualKey.Reserve((Keys)direction, reserve);
        }
        /// <summary>
        /// 仮想キーの入力を行う。
        /// 通常、入力は次のフレームで有効になるが、値の変更を直ちに行いたい場合immediatelyをtrueにする。
        /// </summary>
        /// <param name="key">入力するキー</param>
        /// <param name="immediately">値を直ちに変更する</param>
        public static void Input(Keys key, bool immediately) {
            if (immediately) {
                if (!VirtualKey.currentStatus[key].isPressed) {
                    List<int> reserves = VirtualKey.reserves[key];
                    for (int i = 0; i < reserves.Count; i++) {
                        reserves[i]++;
                    }
                    reserves.Insert(0, 1);
                    VirtualKey.currentStatus[key] = VirtualKey.previousStatus[key];
                    VirtualKey.UpdateChild(key, true);
                }
            } else {
                VirtualKey.Reserve(key, 1);
            }
        }
        public static void Input(Keys key) {
            VirtualKey.Input(key, false);
        }
        public static void Input(Direction direction, bool immediately) {
            VirtualKey.Input((Keys)direction, immediately);
        }
        public static void Input(Direction direction) {
            VirtualKey.Input((Keys)direction);
        }
    }
}