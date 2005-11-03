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
using System.Reflection;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using Seasar.Framework.Util;
using Seasar.Framework.Xml.Impl;
using Seasar.Framework.Container;

namespace Seasar.Framework.Xml
{
	/// <summary>
	/// XmlHandlerParser の概要の説明です。
	/// </summary>
	public sealed class XmlHandlerParser
	{
		private XmlHandler xmlHandler_;

		public XmlHandlerParser(XmlHandler xmlHandler)
		{
			xmlHandler_ = xmlHandler;
		}

		public XmlHandler XmlHandler
		{
			get { return xmlHandler_; }
		}

		public object Parse(string path)
		{
			StreamReader reader = null;
			string pathWithoutExt = Path.Combine(
					Path.GetDirectoryName(path), Path.GetFileNameWithoutExtension(path));
			string extension = ResourceUtil.GetExtension(path);

			try
			{
				if(File.Exists(path))
				{
					reader = new StreamReader(path);
				}
				else
				{
					reader = ResourceUtil.GetResourceAsStreamReader(pathWithoutExt, extension);
				}
			}
			catch(ResourceNotFoundRuntimeException)
			{
				Assembly[] assemblys = AppDomain.CurrentDomain.GetAssemblies();
				foreach(Assembly assembly in assemblys)
				{
					try
					{
						reader = ResourceUtil.GetResourceAsStreamReader(pathWithoutExt,
							extension,assembly);
						break;
					}
					catch(ResourceNotFoundRuntimeException)
					{
						continue;
					}
				}
			}
			if(reader == null) throw new ResourceNotFoundRuntimeException(path);
			return this.Parse(reader);
		}

		public object Parse(StreamReader input)
		{
			XmlValidatingReader reader = new XmlValidatingReader(
				new XmlTextReader(input));
			reader.ValidationType = ValidationType.DTD;
			reader.XmlResolver = new S2XmlResolver();
			reader.ValidationEventHandler += 
				new ValidationEventHandler(ValidationHandler);
			
			try
			{
				while(reader.Read())
				{
					switch(reader.NodeType)
					{
						case XmlNodeType.Element :
							string elementName = reader.Name;
							bool isEmptyElement = reader.IsEmptyElement;
							AttributesImpl attributes = new AttributesImpl();
							if(reader.MoveToFirstAttribute())
							{
								do
								{
									attributes.AddAttribute(reader.Name, reader.Value);
								} while(reader.MoveToNextAttribute());
							}
							xmlHandler_.StartElement(elementName, attributes);
							if(isEmptyElement) xmlHandler_.EndElement(reader.Name);
							break;
						case XmlNodeType.Text :
							xmlHandler_.Characters(reader.Value);
							break;
						case XmlNodeType.EndElement :
							xmlHandler_.EndElement(reader.Name);
							break;
					}
				}
			}
			finally
			{
				reader.Close();
				reader = null;
				input.Close();
				input = null;
			}

			return xmlHandler_.Result;
		}

		private void ValidationHandler(object sender, ValidationEventArgs args)
		{
			Console.Error.WriteLine("***Validation error");
			Console.Error.WriteLine("\tSeverity:{0}", args.Severity);
			Console.Error.WriteLine("\tMessage  :{0}", args.Message);
		}
	}
}
