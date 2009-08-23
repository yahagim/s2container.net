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

using System;

namespace Seasar.Dao.Examples.AutoUpdate
{
    public interface IAutoUpdateLogic
    {
        void TestAutoUpdate();
    }

    public class AutoUpdateLogicImpl : IAutoUpdateLogic
    {
        private readonly IEmployeeDao _employeeDao;

        public AutoUpdateLogicImpl(IEmployeeDao employeeDao)
        {
            _employeeDao = employeeDao;
        }

        #region IAutoUpdateLogic �����o

        public void TestAutoUpdate()
        {
            // �]�ƈ��ԍ�7499�̏]�ƈ����m�F
            int empno = 7499;
            Employee emp1 = _employeeDao.GetEmployeeByEmpno(empno);
            Console.WriteLine("�]�ƈ��ԍ�[" + empno + "]�̏]�ƈ��F" + emp1.ToString());

            // �]�ƈ��ԍ�7499�̏]�ƈ����X�V
            emp1.Ename = "Sugimoto";
            emp1.Deptnum = 99;
            int ret = _employeeDao.UpdateEmployee(emp1);
            Console.WriteLine("UpdateEmployee���\�b�h�̖߂�l:" + ret);

            // �]�ƈ��ԍ�7499�̏]�ƈ����m�F
            Employee emp2 = _employeeDao.GetEmployeeByEmpno(empno);
            Console.WriteLine("�]�ƈ��ԍ�[" + empno + "]�̏]�ƈ��F" + emp2.ToString());

            throw new ForCleanupException();
        }

        #endregion
    }
}