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

namespace Seasar.Framework.Container.Util
{
	/// <summary>
	/// InstanceModeUtil �̊T�v�̐����ł��B
	/// </summary>
	public sealed class InstanceModeUtil
	{
		private InstanceModeUtil()
		{
		}

		public static bool IsSingleton(string mode)
		{
			return ContainerConstants.INSTANCE_SINGLETON.ToLower().Equals(mode.ToLower());
		}

		public static bool IsPrototype(string mode)
		{
			return ContainerConstants.INSTANCE_PROTOTYPE.ToLower().Equals(mode.ToLower());
		}

		public static bool IsRequest(string mode)
		{
			return ContainerConstants.INSTANCE_REQUEST.ToLower().Equals(mode.ToLower());
		}

		public static bool IsSession(string mode)
		{
			return ContainerConstants.INSTANCE_SESSION.ToLower().Equals(mode.ToLower());
		}

		public static bool IsOuter(string mode)
		{
			return ContainerConstants.INSTANCE_OUTER.ToLower().Equals(mode.ToLower());
		}
	}
}