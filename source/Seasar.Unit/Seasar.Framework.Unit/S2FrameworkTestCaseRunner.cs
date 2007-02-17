#region Copyright
/*
 * Copyright 2005-2007 the Seasar Foundation and the Others.
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
using MbUnit.Core.Invokers;
using MbUnit.Framework;
using Seasar.Extension.Unit;
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using Seasar.Framework.Log;
using Seasar.Framework.Message;
using Seasar.Framework.Util;

namespace Seasar.Framework.Unit
{
	public class S2FrameworkTestCaseRunner
	{
		private static Logger logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private S2FrameworkTestCaseBase fixture;
		private MethodInfo method;
		private IS2Container container;
		private IList bindedFields;
		private Hashtable errors;

		protected IS2Container Container
		{
			get { return container; }
		}

		public virtual object Run(IRunInvoker invoker, object o, IList args)
		{
			fixture = o as S2FrameworkTestCaseBase;
			method = fixture.GetType().GetMethod(invoker.Name);
			SetUpContainer();
			fixture.Container = container;
			try
			{
				try 
				{
					SetUpForEachTestMethod();
					container.Init();
					try
					{
						SetUpAfterContainerInit();
						try
						{
							BindFields();
							SetUpAfterBindFields();
							try
							{
								BeginTransactionContext();
								return invoker.Execute(o, args);
							}
							catch(Exception e)
							{
								ExceptionHandler(e);
								throw e;
							}
							finally
							{
								EndTransactionContext();
								TearDownBeforeUnbindFields();
								UnbindFields();
							}
						}
						catch(Exception e)
						{
							ExceptionHandler(e);
							throw e;
						}
						finally
						{
							TearDownBeforeContainerDestroy();
						}
					}
					catch(Exception e)
					{
						ExceptionHandler(e);
						throw e;
					}
					finally
					{
						container.Destroy();
					}
				}
				catch(Exception e)
				{
					ExceptionHandler(e);
					throw e;
				}
				finally 
				{
					TearDownForEachTestMethod();
				}
			}
			catch(Exception e)
			{
				ExceptionHandler(e);
				throw e;
			}
			finally
			{
				for (int i = 0; i < 3; ++i)
				{
					GC.WaitForPendingFinalizers();
					GC.Collect();
				}
				TearDownContainer();
			}
		}

		protected virtual void SetUpContainer()
		{
			SingletonS2ContainerFactory.Init();
			container = SingletonS2ContainerFactory.Container;
		}

		protected virtual void TearDownContainer()
		{
            SingletonS2ContainerFactory.Destroy();
			container = null;
		}
		
		protected virtual void SetUpForEachTestMethod()
		{
			string targetName = GetTargetName();
			if (targetName.Length > 0) 
			{
				MethodInfo setupMethod = fixture.GetType().GetMethod("SetUp" + targetName);
				if (setupMethod != null)
					MethodUtil.Invoke(setupMethod, fixture, null);
			}
		}

		protected virtual void TearDownForEachTestMethod()
		{
			String targetName = GetTargetName();
			if (targetName.Length > 0) 
			{
				MethodInfo tearDownMethod = fixture.GetType().GetMethod("TearDown" + targetName);
				if (tearDownMethod != null)
					MethodUtil.Invoke(tearDownMethod, fixture, null);
			}
		}

		protected virtual void BeginTransactionContext()
		{
			
		}

		protected virtual void EndTransactionContext()
		{
			
		}

		protected virtual void SetUpAfterContainerInit()
		{
		}

		protected virtual void SetUpAfterBindFields()
		{
		}

		protected virtual void TearDownBeforeUnbindFields()
		{
		}

		protected virtual void TearDownBeforeContainerDestroy()
		{
		}

		protected string GetTargetName()
		{
			string name = method.Name;
			
			if (name.ToLower().StartsWith("test"))
				name = name.Substring(4);
			
			if (name.ToLower().EndsWith("test"))
				name = name.Substring(0, name.Length - 4);
			
			return name;
		}

		protected void BindFields()
		{
			bindedFields = new ArrayList();
			for (Type type = fixture.GetType(); 
				(type != typeof(S2FrameworkTestCaseBase) && type != typeof(S2TestCase) && type != null);
				type = type.BaseType) {
				
				FieldInfo[] fields = type.GetFields(
							BindingFlags.DeclaredOnly | 
							BindingFlags.Public| 
							BindingFlags.NonPublic | 
							BindingFlags.Instance | 
							BindingFlags.Static);
				
				for (int i = 0; i < fields.Length; ++i) {
					BindField(fields[i]);
				}
			}
		}

		protected void BindField(FieldInfo fieldInfo)
		{
			if (IsAutoBindable(fieldInfo))
			{
				if (fieldInfo.FieldType.ToString() == "System.DateTime")
				{
					DateTime dateValue = (DateTime) fieldInfo.GetValue(fixture);
					if (DateTime.MinValue != dateValue)
						return;
				}
				else if (fieldInfo.GetValue(fixture) != null)
				{
					return;
				}
				string name = NormalizeName(fieldInfo.Name);
				object component = null;
				if (this.container.HasComponentDef(name))
				{
					Type componentType = this.container.GetComponentDef(name).ComponentType;
					if (componentType == null)
					{
						component = this.container.GetComponent(name);
						if (component != null)
						{
							componentType = component.GetType();
						}
					}

					if (componentType != null 
								&& fieldInfo.FieldType.IsAssignableFrom(componentType))
					{
						if (component == null)
						{
							component = this.container.GetComponent(name);
						}
					}
					else
					{
						component = null;
					}
				}
				if (component == null
					&& this.container.HasComponentDef(fieldInfo.FieldType))
				{
					component = this.container.GetComponent(fieldInfo.FieldType);
				}
				if (component != null) {
					/// TODO 例外ラップとユーティリティにまとめる？
					fieldInfo.SetValue(fixture, component);
					bindedFields.Add(fieldInfo);
				}
			}
		}

		protected String NormalizeName(String name)
		{
			return name.TrimEnd('_').TrimStart('_');
		}

		protected bool IsAutoBindable(FieldInfo fieldInfo)
		{
			return !fieldInfo.IsStatic && !fieldInfo.IsLiteral 
						&& !fieldInfo.IsInitOnly; // && !fieldInfo.FieldType.IsValueType;
		}

		protected void UnbindFields()
		{
			for (int i = 0; i < bindedFields.Count; ++i) {
				FieldInfo fieldInfo = (FieldInfo) bindedFields[i];
				try
				{
					if (!fieldInfo.FieldType.IsValueType)
					{
						fieldInfo.SetValue(fixture, null);
					}
				}
				catch (ArgumentException e)
				{
					Console.Error.WriteLine(e);
				}
				catch (FieldAccessException e)
				{
					Console.Error.WriteLine(e);
				}
			}
		}

		private void ExceptionHandler(Exception e)
		{
			if (errors == null)
				errors = new Hashtable();
			
			if (!errors.ContainsKey(e.GetHashCode()))
			{
				object[] attrs = method.GetCustomAttributes(typeof(ExpectedExceptionAttribute), false);
				foreach(ExpectedExceptionAttribute attribute in attrs)
				{
					if (IsMatchExpectedException(attribute.ExceptionType, e))
						return;
				}

				if (logger.IsDebugEnabled)
					logger.Debug(MessageFormatter.GetSimpleMessage("ESSR0017", new object[] { e }), e);
				else
					Console.Error.WriteLine(e);
				
				errors.Add(e.GetHashCode(), e);
			}
		}

		private bool IsMatchExpectedException(Type ExpectedExceptionType, Exception e)
		{
			if (ExpectedExceptionType == e.GetType())
				return true;
			else if (e.InnerException != null)
				return IsMatchExpectedException(ExpectedExceptionType, e.InnerException);
			else
				return false;
		}
	}
}