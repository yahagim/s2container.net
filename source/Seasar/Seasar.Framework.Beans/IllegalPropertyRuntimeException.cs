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
using Seasar.Framework.Exceptions;

namespace Seasar.Framework.Beans
{
	/// <summary>
	/// IllegalPropertyRuntimeException �̊T�v�̐����ł��B
	/// </summary>
	public class IllegalPropertyRuntimeException : SRuntimeException
	{
		private Type componentType_;
		private string propertyName_;

		public IllegalPropertyRuntimeException(
			Type componentType, string propertyName,Exception cause)
			: base("ESSR0059",new object[] {componentType.FullName,propertyName,cause},
			cause)
		{
			componentType_ = componentType;
			propertyName_ = propertyName;
		}

		public Type ComponentType
		{
			get { return componentType_; }
		}

		public string PropertyName
		{
			get { return propertyName_; }
		}
	}
}