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
using System.Data.SqlTypes;
using System.Reflection;
using Nullables;

namespace Seasar.Framework.Util
{
    /// <summary>
    /// �^�̕ϊ����s���N���X
    /// </summary>
    public sealed class ConversionUtil
    {
        private ConversionUtil()
        {
        }

        /// <summary>
        /// �n���ꂽ�I�u�W�F�N�g��ړI��Type�ɕϊ�����
        /// </summary>
        /// <param name="o">�ϊ�����I�u�W�F�N�g</param>
        /// <param name="targetType">�ړI��Type</param>
        /// <returns>�ϊ����ꂽ�I�u�W�F�N�g</returns>
        public static object ConvertTargetType(object o, Type targetType)
        {
            object ret = null;

            if(typeof(IConvertible).IsAssignableFrom(targetType))
            {
                ret = ConvertConvertible(o, targetType);
            }
            else if(typeof(INullable).IsAssignableFrom(targetType))
            {
                ret = ConvertNullable(o, targetType);
            }
            else if(typeof(INullableType).IsAssignableFrom(targetType))
            {
                ret = ConvertNullableType(o, targetType);
            }
            else if(o == DBNull.Value)
            {
                ret = null;
            }
            else
            {
                ret = o;
            }

            return ret;
        }

        /// <summary>
        /// �I�u�W�F�N�g��INullable����������Type�ɕϊ�����
        /// </summary>
        /// <param name="o">�ϊ�����I�u�W�F�N�g</param>
        /// <param name="targetType">INullable����������Type</param>
        /// <returns>�ϊ����ꂽ�I�u�W�F�N�g</returns>
        public static object ConvertNullable(object o, Type targetType)
        {
            INullable ret = null;

            if(o == null || o == DBNull.Value)
            {
                ret = (INullable) targetType.InvokeMember("Null", BindingFlags.GetField,
                    null, null, null);
            }
            else
            {
                Type paramType = GetValueType(targetType);
                ret = (INullable) Activator.CreateInstance(targetType, 
                    new object[] { Convert.ChangeType(o, paramType) });
            }

            return ret;
        }

        /// <summary>
        /// �I�u�W�F�N�g��INullableType����������Type�ɕϊ�����
        /// </summary>
        /// <param name="o">�ϊ�����I�u�W�F�N�g</param>
        /// <param name="targetType">INullableType����������Type</param>
        /// <returns>�ϊ����ꂽ�I�u�W�F�N�g</returns>
        public static object ConvertNullableType(object o, Type targetType)
        {
            INullableType ret = null;

            if(o == null || o == DBNull.Value)
            {
                ret = (INullableType) targetType.InvokeMember("Default", BindingFlags.GetField,
                    null, null, null);
            }
            else
            {
                Type paramType = GetValueType(targetType);
                ret = (INullableType) Activator.CreateInstance(targetType, 
                    new object[] { Convert.ChangeType(o, paramType) });
            }

            return ret;
        }

        /// <summary>
        /// �I�u�W�F�N�g��IConvertible����������Type�ɕϊ�����
        /// </summary>
        /// <param name="o">�ϊ�����I�u�W�F�N�g</param>
        /// <param name="targetType">IConvertible����������Type</param>
        /// <returns>�ϊ����ꂽ�I�u�W�F�N�g</returns>
        public static object ConvertConvertible(object o, Type targetType)
        {
            object ret = null;

            if(o == null || o == DBNull.Value)
            {
                if(!targetType.Equals(typeof(string)))
                {
                    ret = Convert.ChangeType(decimal.Zero, targetType);
                }
            }
            else
            {
                ret = Convert.ChangeType(o, targetType);
            }

            return ret;
        }

        /// <summary>
        /// �v���p�e�B����"Value"�ł���v���p�e�B��Type��Ԃ�
        /// </summary>
        /// <param name="targetType">Type</param>
        /// <returns></returns>
        private static Type GetValueType(Type targetType)
        {
            PropertyInfo pi = targetType.GetProperty("Value");
            return pi.PropertyType;
        }
    }
}