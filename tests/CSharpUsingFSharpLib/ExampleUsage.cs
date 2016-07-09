using System;
using FSharpExampleLib;
using NUnit.Framework;
using Microsoft.FSharp.Core;

namespace CSharpUsingFSharpLib
{
	public class ExampleUsage
	{
		public static ExampleDomainRecord BuildFSharpRecord()
		{
			return new ExampleDomainRecord("TEST", 1, 2);
		}

		[Test]
		public void TestExampleRecordWorksAsExpected()
		{
			var record = BuildFSharpRecord();
			Assert.AreEqual("TEST", record.Field1);
			Assert.AreEqual(1, record.Field2);
			Assert.AreEqual(2.0, record.Field3);
		}

		public static ExampleUnion BuildExampleFSharpUnion()
		{
            var oneCase = ExampleUnion.One;
            var twoCase = ExampleUnion.NewTwo("Test");
            var threeCase = ExampleUnion.NewThree(3);


            return threeCase;
		}

        [Test]
        public void TestExampleUnionUsage()
        {
            var unionValue = BuildExampleFSharpUnion();

            var result = 
                unionValue.Match(
                    () => "TEST1",
                    s => "TEST2 " + s,
                    i => "TEST3 " + i);

            Assert.AreEqual("TEST3 3", result);
        }

		[Test]
		public void UsingAFSharpModuleDirectly()
		{
			// Using two module functions (like a C# static class to build the list)
			var list = RecursiveList.cons("TEST", RecursiveList.EmptyList<string>());
			// Building a discriminated union the C# way to assert equality
			var expectedList = RecursiveList<string>.NewHead("TEST", RecursiveList.EmptyList<string>());

			Assert.AreEqual(expectedList, list);
		}

		[Test]
		public void UsingFSharpFuncIfYouHaveTo()
		{
            // These are all using the module functions.
            // Module is just a static class to C#.
            var list = RecursiveList.EmptyList<string>();
            var listWithAddedData = RecursiveList.cons("Test", list);

            Func<string, string> mapFunction = s => "TEST2" + s;
            var fsharpFunc = mapFunction.ToFSharpFunc();

			var mappedList = RecursiveList.map(fsharpFunc, listWithAddedData);

			var expectedList = RecursiveList<string>.NewHead("TEST2Test", RecursiveList<string>.Empty);

			Assert.AreEqual(expectedList, mappedList);
		}

		[Test]
		public void UsingCSharpFriendlyApiInsteadOfFSharpFunc()
		{
			// These are all using the module functions.
			// Module is just a static class to C#.
			var list = RecursiveList.EmptyList<string>();
			var listWithAddedData = RecursiveList.cons("Test", list);

			Func<string, string> mapFunction = s => "TEST3" + s;

			var mappedList = listWithAddedData.Select(mapFunction);

			var expectedList = RecursiveList<string>.NewHead("TEST3Test", RecursiveList<string>.Empty);

			Assert.AreEqual(expectedList, mappedList);
		}
    }
}

