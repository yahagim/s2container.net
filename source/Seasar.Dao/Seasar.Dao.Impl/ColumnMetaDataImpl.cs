#region Copyright
/*
 * Copyright 2005-2009 the Seasar Foundation and the Others.
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

using System.Reflection;
using Seasar.Extension.ADO;

namespace Seasar.Dao.Impl
{
    /// <summary>
    /// Column�̃��^�f�[�^
    /// </summary>
    public class ColumnMetaDataImpl : IColumnMetaData
    {
        private readonly string _columnName;
        private readonly PropertyInfo _propertyInfo;
        private readonly IValueType _valueType;

        public ColumnMetaDataImpl(IPropertyType propertyType, string columnName)
        {
            _valueType = propertyType.ValueType;
            _propertyInfo = propertyType.PropertyInfo;
            _columnName = columnName;
        }

        #region IColumnMetaData �����o

        public string ColumnName
        {
            get { return _columnName; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return _propertyInfo; }
        }

        public IValueType ValueType
        {
            get { return _valueType; }
        }

        #endregion
    }
}