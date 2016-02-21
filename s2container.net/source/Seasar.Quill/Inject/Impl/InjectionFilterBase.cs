﻿using System;
using System.Collections.Generic;
using System.Reflection;
using QM = Quill.QuillManager;

namespace Quill.Inject.Impl {
    /// <summary>
    /// 既定のInjectionフィルタークラス
    /// </summary>
    public class InjectionFilterBase : IInjectionFilter {
        private const string SOURCE_IS_TARGET_TYPE = "InjectionFilterBase#IsTargetType";

        /// <summary>
        /// Injection除外対象型セット
        /// </summary>
        public virtual ISet<Type> NotInjectionTargetTypes { get; protected set; }

        /// <summary>
        /// Injection対象型セット
        /// </summary>
        public virtual ISet<Type> InjectionTargetTypes { get; protected set; }

        /// <summary>
        /// デフォルトでInjection対象に含めるか？（true（既定値）:含める, false:含めない)
        /// </summary>
        public virtual bool IsTargetTypeDefault { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InjectionFilterBase() {
            NotInjectionTargetTypes = new HashSet<Type>();
            InjectionTargetTypes = new HashSet<Type>();
            IsTargetTypeDefault = true;
        }

        /// <summary>
        /// Injection対象となるフィールド紐づけフラグの取得
        /// </summary>
        /// <returns>フィールド紐づけフラグ</returns>
        public virtual BindingFlags GetTargetFieldBindinFlags() {
            return BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        }

        /// <summary>
        /// Injection対象のフィールドか判定
        /// </summary>
        /// <param name="componentType">コンポーネント型</param>
        /// <param name="fieldInfo">判定対象のフィールド情報</param>
        /// <returns>true;Injection対象, false:Injection対象外</returns>
        public virtual bool IsTargetField(Type componentType, FieldInfo fieldInfo) {
            return IsTargetType(fieldInfo.FieldType);
        }

        /// <summary>
        /// Injection対象の型か判定
        /// </summary>
        /// <param name="componentType">コンポーネント型</param>
        /// <returns>true;Injection対象, false:Injection対象外</returns>
        public virtual bool IsTargetType(Type componentType) {
            // 必ずInjection対象とする型か？
            if(InjectionTargetTypes.Contains(componentType)) {
                QM.OutputLog(GetType(), Message.EnumMsgCategory.DEBUG,
                    string.Format("[{0}] is injection target.", 
                    componentType == null ? "null" : componentType.Name));
                return true;
            }

            // 必ずInjection非対象とする型か？
            if(NotInjectionTargetTypes.Contains(componentType)) {
                QM.OutputLog(GetType(), Message.EnumMsgCategory.DEBUG,
                    string.Format("[{0}] is not injection target.",
                    componentType == null ? "null" : componentType.Name));
                return false;
            }

            // 上記どちらでもない場合はデフォルトの設定を適用
            QM.OutputLog(GetType(), Message.EnumMsgCategory.DEBUG,
                    string.Format("[{0}]:isInjectionTarget:{1}", 
                    componentType == null ? "null" : componentType.Name, 
                    IsTargetTypeDefault));
            return IsTargetTypeDefault;
        }

        /// <summary>
        /// リソースの解放
        /// </summary>
        public virtual void Dispose() {
            NotInjectionTargetTypes.Clear();
            InjectionTargetTypes.Clear();
        }
    }
}
