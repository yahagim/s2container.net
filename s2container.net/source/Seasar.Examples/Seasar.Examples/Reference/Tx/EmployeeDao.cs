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
using System.Data;
using Seasar.Extension.ADO;

namespace Seasar.Examples.Reference.Tx
{
    public interface IEmployeeDao
    {
        void Insert();
    }

	public class EmployeeDaoImpl : IEmployeeDao
    {
        private IDataSource dataSource;

        public IDataSource DataSource
        {
            set { dataSource = value; }
        }

        #region IEmployeeDao �����o

        public void Insert()
        {
            using(IDbConnection cn = dataSource.GetConnection())
            {
                cn.Open();
                string sql = "insert into emp2 values(@empno, @ename, @deptnum)";
                using(IDbCommand cmd = dataSource.GetCommand(sql,cn))
                {
                    cmd.Parameters.Add(dataSource.GetParameter("@empno", 99));
                    cmd.Parameters.Add(dataSource.GetParameter("@ename", "Sugimoto"));
                    cmd.Parameters.Add(dataSource.GetParameter("@deptnum", 31));
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("EMP��ǉ����܂����B");
                }
            }

            // ���[���o�b�N�̂��߂�Exception�𔭐�������
            throw new ApplicationException();
        }

        #endregion
    }
}