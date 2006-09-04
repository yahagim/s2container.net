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

using System.Text.RegularExpressions;
using MbUnit.Framework;
using Seasar.Extension.ADO;
using Seasar.Extension.ADO.Impl;
using Seasar.Extension.Unit;

namespace Seasar.Tests.Extension.ADO.Impl
{
    [TestFixture]
    public class DbParameterParserTest : S2TestCase
    {
        private IDbParameterParser parser = new DbParameterParser();

        [Test]
        public void ParseAtMark()
        {
            string sql = "INSERT INTO emp ( empno , ename , job ) VALUES ( @empno , 'M@NAGER' , @job )";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual("@empno", ret[0].Value);
            Assert.AreEqual("@job", ret[1].Value);
        }

        [Test]
        public void ParseAtMark2()
        {
            string sql = "INSERT INTO emp(empno,ename,job)VALUES(@empno,'m@nager',@job)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual("@empno", ret[0].Value);
            Assert.AreEqual("@job", ret[1].Value);
        }

        [Test]
        public void ParseAtMark3()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (@$empno, @#manager, @dto.job)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseAtMark4()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (@$従業員番号, @かんりしゃ, @ショクギョウ)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseAtMark5()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (@1empno, @m2anager, @job3)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseAtMark6()
        {
            string sql = "INSERT INTO EMP (EMPNO, ENAME, JOB) VALUES (@EMP_NO, @MANAGER, @_JOB)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseAtMark7()
        {
            string sql = "INSERT INTO EMP (EMPNO, ENAME, JOB) VALUES (@$#, @@USERID, @A.B)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual("@$#", ret[0].Value);
            Assert.AreEqual("@A.B", ret[1].Value);
        }

        [Test]
        public void ParseColon()
        {
            string sql = "INSERT INTO emp ( empno , ename , job ) VALUES ( :empno , 'M:NAGER' , :job )";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(":empno", ret[0].Value);
            Assert.AreEqual(":job", ret[1].Value);
        }

        [Test]
        public void ParseColon2()
        {
            string sql = "INSERT INTO emp(empno,ename,job)VALUES(:empno,'m:nager',:job)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(":empno", ret[0].Value);
            Assert.AreEqual(":job", ret[1].Value);
        }

        [Test]
        public void ParseColon3()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (:$empno, :#manager, :dto.job)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseColon4()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (:$従業員番号, :かんりしゃ, :ショクギョウ)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseColon5()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (:1empno, :m2anager, :job3)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseColon6()
        {
            string sql = "INSERT INTO EMP (EMPNO, ENAME, JOB) VALUES (:EMP_NO, :MANAGER, :_JOB)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }

        [Test]
        public void ParseColon7()
        {
            string sql = "INSERT INTO EMP (EMPNO, ENAME, JOB) VALUES (:$#, ::USERID, :A.B)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(":$#", ret[0].Value);
            Assert.AreEqual(":A.B", ret[1].Value);
        }

        [Test]
        public void ParseQuestion()
        {
            string sql = "INSERT INTO emp ( empno , ename , job ) VALUES ( :empno , 'M:NAGER' , :job )";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual(":empno", ret[0].Value);
            Assert.AreEqual(":job", ret[1].Value);
        }

        [Test]
        public void ParseQuestion2()
        {
            string sql = "INSERT INTO emp(empno,ename,job)VALUES(?,'m?nager',?)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(2, ret.Count);
            Assert.AreEqual("?", ret[0].Value);
            Assert.AreEqual("?", ret[1].Value);
        }

        [Test]
        public void ParseQuestion3()
        {
            string sql = "INSERT INTO emp (empno, ename, job) VALUES (?, ?, ?)";
            MatchCollection ret = parser.Parse(sql);
            Assert.AreEqual(3, ret.Count);
        }
    }
}
