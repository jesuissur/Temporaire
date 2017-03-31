using FluentAssertions;
using NUnit.Framework;

namespace CountingSheep
{
    [TestFixture]
    public class TheSheepTests
    {
        private const string WhateverLineCount = "2";

        [Test]
        public void Count1_Should_Compute10()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {WhateverLineCount, "1"});

            sleepNumbers.Should().HaveCount(1).And.Contain("Case #1: 10");
        }

        [Test]
        public void Count2_Should_Compute10()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {WhateverLineCount, "2"});

            sleepNumbers.Should().HaveCount(1).And.Contain("Case #1: 90");
        }

        [Test]
        public void Count11_Should_Compute110()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {WhateverLineCount, "11"});

            sleepNumbers.Should().HaveCount(1).And.Contain("Case #1: 110");
        }

        [Test]
        public void Count1692_Should_Compute5076()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {WhateverLineCount, "1692"});

            sleepNumbers.Should().HaveCount(1).And.Contain("Case #1: 5076");
        }

        [Test]
        public void CountZero_Should_ComputeInsomnia()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {WhateverLineCount, "0"});

            sleepNumbers.Should().HaveCount(1).And.Contain("Case #1: INSOMNIA");
        }

        [Test]
        public void CountMultiple_Should_ComputeAsMuchResultsAsSpecifiedOnFirstLine()
        {
            var subject = new TheSheep();

            var sleepNumbers = subject.ComputeSleepNumbers(new[] {"3", "1", "2", "3", "4"});

            sleepNumbers.Should().HaveCount(3);
        }
    }
}