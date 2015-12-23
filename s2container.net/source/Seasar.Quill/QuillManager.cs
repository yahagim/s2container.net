﻿using System;
using Quill.Config;
using Quill.Config.Impl;
using Quill.Container;
using Quill.Container.Impl;
using Quill.Inject;
using Quill.Inject.Impl;
using Quill.Message;

namespace Quill {
    /// <summary>
    /// ログ出力デリゲート
    /// </summary>
    /// <param name="category"></param>
    /// <param name="log"></param>
    public delegate void OutputLogDelegate(string category, string log);

    /// <summary>
    /// Quill挙動管理クラス
    /// </summary>
    public static class QuillManager {
        /// <summary>
        /// 設定情報
        /// </summary>
        public static IQuillConfig Config { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static QuillContainer Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static ITypeMap TypeMap { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IQuillInjector Injector { get; set; }

        /// <summary>
        /// Injectionフィルター
        /// </summary>
        public static IInjectionFilter InjectionFilter { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static IComponentCreator ComponentCreator { get; set; }

        /// <summary>
        /// 出力メッセージ
        /// </summary>
        public static IQuillMessage Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static OutputLogDelegate OutputLog { get; set; }

        /// <summary>
        /// 既定の設定で初期化
        /// </summary>
        public static void InitializeDefault() {
            Config = new QuillAppConfig();
            Container = new QuillContainer();
            TypeMap = new TypeMapImpl();
            ComponentCreator = new CompornentCreatorBase();
            Injector = new QuillInjector();
            InjectionFilter = new InjectionFilterBase();
            OutputLog = OutputLogToConsole;
        }

        private static void OutputLogToConsole(string category, string log) {
            Console.WriteLine(string.Format("{0}:[{1}] {2}", DateTime.Now, category, log));
        }
    }

}
