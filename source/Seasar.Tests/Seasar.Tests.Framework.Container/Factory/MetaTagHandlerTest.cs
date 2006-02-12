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
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;
using NUnit.Framework;

namespace TestSeasar.Framework.Container.Factory
{

	/// <summary>
	/// MetaTagHandlerTest �̊T�v�̐����ł��B
	/// </summary>
	[TestFixture]
	public class MetaTagHandlerTest
	{
		private const string PATH 
            = "Seasar/Tests/Framework/Container/Factory/MetaTagHandlerTest.dicon";

		[Test]
		public void TestMeta()
		{
			IS2Container container = S2ContainerFactory.Create(PATH);
			Assert.AreEqual(1, container.MetaDefSize);
			IMetaDef md = container.GetMetaDef("aaa");
			Assert.AreEqual("111", md.Value);
			Assert.IsNotNull(md.Container);
			Assert.AreEqual(1, md.MetaDefSize);
			IMetaDef md2 = md.GetMetaDef(0);
			Assert.AreEqual("222", md2.Value);
		}
	}
}