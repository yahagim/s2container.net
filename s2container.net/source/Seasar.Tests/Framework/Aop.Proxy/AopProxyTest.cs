#region Copyright
/*
 * Copyright 2005-2008 the Seasar Foundation and the Others.
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
using System.Diagnostics;
using MbUnit.Framework;
using Seasar.Framework.Aop;
using Seasar.Framework.Aop.Impl;
using Seasar.Framework.Aop.Proxy;
using Seasar.Framework.Aop.Interceptors;
using Seasar.Extension.Unit;

namespace Seasar.Tests.Framework.Aop.Proxy
{
    [TestFixture]
    public class AopProxyTest : S2TestCase
    {
        [Test, S2]
        public void TestInterface()
        {
            IPointcut pointcut = new PointcutImpl(new string[] { "Greeting" });
            IAspect aspect = new AspectImpl(new HelloInterceptor(), pointcut);
            AopProxy aopProxy = new AopProxy(typeof(IHello), new IAspect[] { aspect });
            IHello proxy = (IHello) aopProxy.GetTransparentProxy();
            Assert.AreEqual("Hello", proxy.Greeting());
        }

        [Test, S2]
        public void TestCreateForArgs()
        {
            IAspect aspect = new AspectImpl(new TraceInterceptor());
            AopProxy aopProxy = new AopProxy(typeof(HelloImpl), new IAspect[] { aspect });
            IHello proxy = (IHello) aopProxy.Create(new Type[] { typeof(string) },
                new object[] { "Hello" });
            Assert.AreEqual("Hello", proxy.Greeting());
            Trace.WriteLine(proxy.GetHashCode());
        }

        [Test, S2]
        public void TestEquals()
        {
            IPointcut pointcut = new PointcutImpl(new string[] { "Greeting" });
            IAspect aspect = new AspectImpl(new HelloInterceptor(), pointcut);
            AopProxy aopProxy = new AopProxy(typeof(IHello), new IAspect[] { aspect });
            IHello proxy = (IHello) aopProxy.Create();

            //Assert.AreEqual(true,proxy.Equals(proxy));
            Assert.AreEqual(true, object.Equals(proxy, proxy));
            Assert.AreEqual(false, object.Equals(proxy, null));
            Assert.AreEqual(false, object.Equals(proxy, "hoge"));
        }

        public void SetUpPerformance1()
        {
            Include("Seasar.Tests.Framework.Aop.Proxy.AopProxy.dicon");
        }

        [Test, S2]
        public void TestPerformance1()
        {
            DateTime start = DateTime.Now;
            for (int i = 0; i < 100; ++i)
            {
                Container.GetComponent(typeof(IHello4));
            }
            TimeSpan span = DateTime.Now - start;
            Debug.WriteLine(span.TotalMilliseconds + "ms");
        }

        public void SetUpPerformance2()
        {
            Include("Seasar.Tests.Framework.Aop.Proxy.AopProxy.dicon");
        }

        [Test, S2]
        public void TestPerformance2()
        {
            IHello4 hello = (IHello4) Container.GetComponent(typeof(IHello4));
            DateTime start = DateTime.Now;
            for (int i = 0; i < 10000; ++i)
            {
                hello.Greeting();
            }
            TimeSpan span = DateTime.Now - start;
            Debug.WriteLine(span.TotalMilliseconds + "ms");
        }

        [Test, S2]
        public void TestArgs()
        {
            IAspect aspect = new AspectImpl(new TraceInterceptor());
            AopProxy aopProxy = new AopProxy(typeof(CalcImpl), new IAspect[] { aspect });
            ICalc proxy = (ICalc) aopProxy.Create();
            Assert.AreEqual(5, proxy.Add(2, 3));
            int ret;
            proxy.Add(4, 5, out ret);
            Assert.AreEqual(9, ret);
        }

        [Test, S2]
        public void TestArgsNoAspect()
        {
            AopProxy aopProxy = new AopProxy(typeof(CalcImpl), new IAspect[] { });
            ICalc proxy = (ICalc) aopProxy.Create();
            Assert.AreEqual(5, proxy.Add(2, 3));
            int ret;
            proxy.Add(4, 5, out ret);
            Assert.AreEqual(9, ret);
        }

        public class TestInvocation : IMethodInterceptor
        {
            internal bool invoked = false;

            public object Invoke(IMethodInvocation invocation)
            {
                invoked = true;
                return invocation.Proceed();
            }
        }

        public class MyInvocation : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return invocation.Proceed();
            }
        }

        public interface IHello
        {
            string Greeting();
        }

        [Serializable()]
        public class HelloImpl : MarshalByRefObject, IHello
        {
            private readonly string _str;

            public HelloImpl(string str)
            {
                _str = str;
            }

            public string Greeting()
            {
                return _str;
            }

        }

        public class Hello2Impl : IHello
        {
            public string Greeting()
            {
                return "Hello2";
            }
        }

        public class HelloImpl3 : IHello
        {
            public string Greeting()
            {
                return "hoge";
            }
        }

        public class HelloInterceptor : IMethodInterceptor
        {
            public object Invoke(IMethodInvocation invocation)
            {
                return "Hello";
            }
        }

        public interface IHello4
        {
            string Greeting();
        }

        public class HelloImpl4 : IHello4
        {
            private readonly string _str = "abc";

            public string Greeting()
            {
                return _str;
            }
        }

        public interface ICalc
        {
            int Add(int x, int y);
            void Add(int x, int y, out int ret);
        }

        public class CalcImpl : MarshalByRefObject, ICalc
        {
            public int Add(int a, int b)
            {
                return a + b;
            }

            public void Add(int a, int b, out int ret)
            {
                ret = a + b;
            }
        }
    }
}
