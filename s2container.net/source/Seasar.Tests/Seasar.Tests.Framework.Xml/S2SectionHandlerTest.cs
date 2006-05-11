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
using NUnit.Framework;
using Seasar.Framework.Xml;

namespace Seasar.Tests.Framework.Xml
{
	/// <summary>
	/// S2SectionHandlerTest の概要の説明です。
	/// </summary>
	[TestFixture]
	public class S2SectionHandlerTest
	{
		[Test]
		public void Test()
		{
			S2Section section = S2SectionHandler.GetS2Section();

			Assert.AreEqual("test.dicon", section.ConfigPath);
			Assert.AreEqual(2, section.Assemblys.Count);
			Assert.AreEqual("Seasar.Tests", (string) section.Assemblys[0]);
			Assert.AreEqual("Seasar", (string) section.Assemblys[1]);
		}
	}
}
