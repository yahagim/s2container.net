#region Copyright
/*
 * Copyright 2005-2012 the Seasar Foundation and the Others.
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
using Seasar.Framework.Container;
using Seasar.Framework.Container.Factory;

namespace Seasar.Examples.Reference.AutoBinding
{
    public class AutoHelloPropertyInjection : IHello
    {
        private IDictionary _dictionary;

        public IDictionary Message
        {
            set { _dictionary = value; }
        }

        public void ShowMessage()
        {
            Console.WriteLine(_dictionary["hello"]);
        }
    }

    public class AutoHelloPropertyInjectionClient
    {
        private const string PATH = "Seasar.Examples/Reference/AutoBinding/AutoHelloPropertyInjection.dicon";

        public void Main()
        {
            IS2Container container = S2ContainerFactory.Create(PATH);
            IHello hello = (IHello) container.GetComponent(typeof(IHello));
            hello.ShowMessage();
        }
    }
}
