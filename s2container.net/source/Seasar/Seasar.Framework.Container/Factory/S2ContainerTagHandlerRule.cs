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
using Seasar.Framework.Xml;

namespace Seasar.Framework.Container.Factory
{
	/// <summary>
	/// S2ContainerTagHandlerRule �̊T�v�̐����ł��B
	/// </summary>
	public class S2ContainerTagHandlerRule : TagHandlerRule
	{
		public S2ContainerTagHandlerRule()
		{
			this["/components"] = new ComponentsTagHandler();
			this["component"] = new ComponentTagHandler();
			this["arg"] = new ArgTagHandler();
			this["property"] = new PropertyTagHandler();
			this["meta"] = new MetaTagHandler();
			this["initMethod"] = new InitMethodTagHandler();
			this["destroyMethod"] = new DestroyMethodTagHandler();
			this["aspect"] = new AspectTagHandler();
			this["/components/include"] = new IncludeTagHandler();
		}
	}
}