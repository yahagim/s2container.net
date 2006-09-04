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
using Nullables;

namespace Seasar.Extension.ADO.Types
{
	public abstract class NHibernateNullableBaseType : BaseValueType
	{
		public NHibernateNullableBaseType()
		{
        }

		protected override object GetBindValue(object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			INullableType ret = (INullableType) value as INullableType;
            if (ret == null) 
            {
                return value;
            }
			if (!ret.HasValue)
			{
				return DBNull.Value;
			}
			return ret.Value;
		}
    }
}
