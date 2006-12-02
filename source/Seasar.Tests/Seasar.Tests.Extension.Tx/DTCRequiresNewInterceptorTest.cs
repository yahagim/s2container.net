#region Copyright
/*
 * Copyright 2005-2006 the Seasar Foundation and the Others.
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
using System.IO;
using System.EnterpriseServices;
using System.Reflection;

using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;

using log4net;
using log4net.Config;
using log4net.Util;

using MbUnit.Framework;

namespace Seasar.Tests.Extension.Tx
{
	[TestFixture]
	[Transaction(TransactionOption.RequiresNew)]
	public class DTCRequiresNewInterceptorTest : ServicedComponent
	{
		private const string path = "Seasar/Tests/Extension/Tx/DTCRequiresNewInterceptorTest.dicon";
		static DTCRequiresNewInterceptorTest()
		{
			FileInfo info = new FileInfo(SystemInfo.AssemblyFileName(
				Assembly.GetExecutingAssembly()) + ".config");
			XmlConfigurator.Configure(LogManager.GetRepository(), info);
		}

		private IS2Container container = null;
		private TxTest tester = null;
		[SetUp]
		public void SetUp()
		{
			container = S2ContainerFactory.Create(path);
			container.Init();
			tester = container.GetComponent(typeof(TxTest)) as TxTest;
		}
		
		[Test]
		[AutoComplete]
		public void TestProceed()
		{
			Guid txid = ContextUtil.TransactionId;
			Assert.IsFalse(txid.Equals(tester.GetTransactionId()));
			Assert.IsTrue(tester.IsInTransaction());
		}
	}
}