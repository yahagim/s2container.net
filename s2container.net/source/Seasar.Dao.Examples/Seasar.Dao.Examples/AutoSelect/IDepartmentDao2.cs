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

using System.Collections;
using System.Data.SqlTypes;
using Seasar.Dao.Attrs;

namespace Seasar.Dao.Examples.AutoSelect
{
    /// <summary>
    /// IDepartmentDao
    /// </summary>
    [Bean(typeof(Department2))]
    public interface IDepartmentDao2
    {
        IList GetAllList();

        [Sql("select active from dept2 where deptno=/*deptno*/")]
        SqlInt16 GetActiveByDeptno(int deptno);

        [Sql("select active from dept2 where deptno=/*deptno*/")]
        int? GetActiveByDeptno2(int deptno);

        [Sql("select dname from dept2 where deptno=/*deptno*/")]
        string GetDnameByDeptno(int deptno);
    }
}