// 
// RemoveBracesTests.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc.
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using NUnit.Framework;
using ICSharpCode.NRefactory.CSharp.Refactoring;

namespace ICSharpCode.NRefactory.CSharp.CodeActions
{
	[TestFixture]
	public class RemoveBracesTests : ContextActionTestBase
	{
		[Test()]
		public void TestSimpleBraces()
		{
			string result = RunContextAction(
				new RemoveBracesAction(),
				"class TestClass" + Environment.NewLine +
					"{" + Environment.NewLine +
					"	void Test ()" + Environment.NewLine +
					"	{" + Environment.NewLine +
					"		if (true) ${" + Environment.NewLine +
					"			;" + Environment.NewLine +
					"		}" + Environment.NewLine +
					"	}" + Environment.NewLine +
					"}"
			);
			
			Assert.AreEqual(
				"class TestClass" + Environment.NewLine +
				"{" + Environment.NewLine +
				"	void Test ()" + Environment.NewLine +
				"	{" + Environment.NewLine +
				"		if (true) " + Environment.NewLine +
				"			;" + Environment.NewLine +
				"	}" + Environment.NewLine +
				"}", result);
		}

		[Test()]
		public void TestTryCatch()
		{
			TestWrongContext<RemoveBracesAction>(@"class TestClass
{
	void Test ()
	{
		try ${ ; } catch (Exception) { ; }
	}
}");
		}
		
		[Test()]
		public void TestTryCatchCatch()
		{
			TestWrongContext<RemoveBracesAction>(@"class TestClass
{
	void Test ()
	{
		try { ; } catch (Exception) ${ ; }
	}
}");
		}
		
		[Test()]
		public void TestTryCatchFinally()
		{
			TestWrongContext<RemoveBracesAction>(@"class TestClass
{
	void Test ()
	{
		try { ; } catch (Exception) { ; } finally ${ ; }
	}
}");
		}


		[Test()]
		public void TestSwitchCatch()
		{
			TestWrongContext<RemoveBracesAction>(@"class TestClass
{
	void Test ()
	{
		switch (foo	) ${ default: break;}
	}
}");
		}

		[Test()]
		public void TestMethodDeclaration()
		{
			TestWrongContext<RemoveBracesAction>(@"class TestClass
{
	void Test ()
	${
		;
	}
}");
		}
		
	}
}

