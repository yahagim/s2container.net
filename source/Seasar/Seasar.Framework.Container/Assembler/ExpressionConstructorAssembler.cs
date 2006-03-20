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
using Seasar.Framework.Util;

namespace Seasar.Framework.Container.Assembler
{
	/// <summary>
	/// ExpressionConstructorAssembler �̊T�v�̐����ł��B
	/// </summary>
	public sealed class ExpressionConstructorAssembler : AbstractConstructorAssembler
	{
		public ExpressionConstructorAssembler(IComponentDef componentDef)
			: base(componentDef)
		{
		}

		public override object Assemble()
		{
			IComponentDef cd = this.ComponentDef;
			IS2Container container = cd.Container;
			string expression = cd.Expression;
			Type componentType = cd.ComponentType;
			object component = JScriptUtil.Evaluate(expression,container);
			if(componentType != null)
			{
				if(component is DBNull || 
					!componentType.IsAssignableFrom(component.GetType()))
				{
					throw new TypeUnmatchRuntimeException(componentType,
						component != null ? component.GetType() : null);
				}
			}
			return component;
		}

	}
}