#region Copyright
/*
 * Copyright 2005 the Seasar Foundation and the Others.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
 * either express or implied. See the License for the specific language
 * governing permissions and limitations under the License.
 */
#endregion

using System;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Aop.Impl
{
	/// <summary>
	/// IPointcutインターフェイスの実装
	/// </summary>
	[Serializable]
	public sealed class PointcutImpl : IPointcut
	{
		/// <summary>
		/// コンパイル済みメソッド名の正規表現
		/// </summary>
		private Regex[] regularExpressions_;

		/// <summary>
		/// メソッド名の正規表現文字列
		/// </summary>
		private string[] methodNames_;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public PointcutImpl(Type targetType)
		{
			if(targetType == null) throw new EmptyRuntimeException("targetType");
			this.SetMethodNames(GetMethodNames(targetType));
		}

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="methodNames">メソッド名の正規表現文字列の配列</param>
        public PointcutImpl(string[] methodNames)
		{
			if(methodNames == null || methodNames.Length == 0)
				throw new EmptyRuntimeException("methodNames");
			this.SetMethodNames(methodNames);
		}

		#region IPointcut メンバ

		/// <summary>
		/// 引数で渡されたmethodにAdviceを挿入するか確認します
		/// </summary>
		/// <param name="method">MethodBase メソッドとコンストラクタに関する情報を持っています</param>
		/// <returns>TrueならAdviceを挿入する、FalseならAdviceは挿入されない</returns>
		public bool IsApplied(MethodBase method)
		{
			return this.IsApplied(method.Name);
		}

		/// <summary>
		/// 引数で渡されたメソッド名にAdviceを挿入するか確認します
		/// </summary>
		/// <param name="methodName">メソッド名</param>
		/// <returns>TrueならAdviceを挿入する、FalseならAdviceは挿入されない</returns>
		public bool IsApplied(string methodName)
		{
			foreach(Regex regex in regularExpressions_)
			{
				if(regex.Match(methodName).Success) return true;
				// if(m.Success && (string.Compare(methodName, m.Value) == 0)) return true;
			}
			return false;
		}

		#endregion

		/// <summary>
		/// メソッド名の正規表現文字列
		/// </summary>
		public string[] MethodNames
		{
			get { return methodNames_; }
		}

		private void SetMethodNames(string[] methodNames)
		{
			methodNames_        = methodNames;
			regularExpressions_ = new Regex[methodNames.Length];
			for(int i = 0; i < methodNames.Length; ++i)
			{
				regularExpressions_[i] = new Regex(methodNames[i].Trim());
			}
		}

		private static string[] GetMethodNames(Type targetType)
		{
			Hashtable methodNameList = new Hashtable();
			if(targetType.IsInterface) AddInterfaceMethodNames(methodNameList,targetType);
			for(Type type = targetType; type != typeof(object) && type != null; type = type.BaseType)
			{
				Type[] interfaces = type.GetInterfaces();
				foreach(Type interfaceTemp in interfaces)
				{
					AddInterfaceMethodNames(methodNameList,interfaceTemp);
				}
			}
			string[] methodNames = new string[methodNameList.Count];
			IEnumerator enu = methodNameList.Keys.GetEnumerator();
			int i = 0;
			while(enu.MoveNext())
			{
				methodNames[i++] = (string) enu.Current;
			}
            return methodNames;
		}

		private static void AddInterfaceMethodNames(Hashtable methodNameList, Type interfaceType)
		{
			MethodInfo[] methods = interfaceType.GetMethods();
			foreach(MethodInfo method in methods)
			{
				if(!methodNameList.ContainsKey(method.Name))
					methodNameList.Add(method.Name,null);
			}
			Type[] interfaces = interfaceType.GetInterfaces();
			foreach(Type interfaceTemp in interfaces)
			{
				AddInterfaceMethodNames(methodNameList,interfaceTemp);
			}
		}
		
	}
}
