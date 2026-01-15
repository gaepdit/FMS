using JetBrains.Annotations;

namespace FMS.App.Tests.Helpers
{
    public class ExportHelperTests
    {
        private class MyClass
        {
            public MyClass(int i, decimal m, double d, float f, string s)
            {
                MyInt = i;
                MyDecimal = m;
                MyDouble = d;
                MyFloat = f;
                MyString = s;
            }

            [UsedImplicitly]
            public int MyInt { get; }

            [UsedImplicitly]
            public decimal MyDecimal { get; }

            [UsedImplicitly]
            public double MyDouble { get; }

            [UsedImplicitly]
            public float MyFloat { get; }

            [UsedImplicitly]
            public string MyString { get; }
        }

        private static readonly List<MyClass> MyItems = new()
        {
            new MyClass(1, 2, 3, 4, "abc"),
            new MyClass(1, 2.0m, 3.0d, 4.0f, "🐛👍😜"),
            new MyClass(0, 0, 0, 0, ""),
            new MyClass(0, 0, 0, 0, null),
        };

        [Test]
        public void Export_Unicode()
        {
            Action act = () => MyItems.ExportExcelAsByteArray(ExportHelper.ReportType.Normal);
            // verify that no exceptions were thrown
            act.Should().NotThrow();
            
        }

        private static readonly List<MyClass> MyMultilineItems = new()
        {
            new MyClass(1, 0, 0, 0, "abc"),
            new MyClass(2, 0, 0, 0, "abc" + Environment.NewLine + "def"),
            new MyClass(3, 0, 0, 0, "abc"),
        };

        [Test]
        public void Export_Multiline()
        {
            Action act = () => MyMultilineItems.ExportExcelAsByteArray(ExportHelper.ReportType.Normal);
            // verify that no exceptions were thrown
            act.Should().NotThrow();
        }
    }
}