using NUnit.Framework;
using RefactoringEssentials.CSharp.Diagnostics;

namespace RefactoringEssentials.Tests.CSharp.Diagnostics
{
    [TestFixture]
    public class CompareNonConstrainedGenericWithNullTests : CSharpDiagnosticTestBase
    {
        [Test]
        public void TestLocal()
        {
            Analyze<CompareNonConstrainedGenericWithNullAnalyzer>(@"public class Bar
{
	public void Foo<T> (T t)
	{
		if (t == $null$) {
		}
	}
}"
/*, @"public class Bar
{
	public void Foo<T> (T t)
	{
		if (t == default(T)) {
		}
	}
}"*/);
        }

        [Test]
        public void TestField()
        {
            Analyze<CompareNonConstrainedGenericWithNullAnalyzer>(@"public class Bar<T>
{
	T t;
	public void Foo ()
	{
		if (t == $null$) {
		}
	}
}"
/*, @"public class Bar<T>
{
	T t;
	public void Foo ()
	{
		if (t == default(T)) {
		}
	}
}"*/);
        }

        [Test]
        public void TestInvalid()
        {
            Analyze<CompareNonConstrainedGenericWithNullAnalyzer>(@"public class Bar
{
	public void Foo<T> (T t) where T : class
	{
		if (t == null) {
		}
	}
}");
        }

        [Test]
        public void TestDisable()
        {
            Analyze<CompareNonConstrainedGenericWithNullAnalyzer>(@"public class Bar
{
	public void Foo<T> (T t)
	{
#pragma warning disable " + CSharpDiagnosticIDs.CompareNonConstrainedGenericWithNullAnalyzerID + @"
		if (t == null) {
		}
	}
}");
        }
    }
}

