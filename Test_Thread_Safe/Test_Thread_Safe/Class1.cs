using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Test_Thread_Safe
{
    public static class Class1
    {

        private static MySelf _mySelf;
        private static readonly Object _lockObject = new object();

        public static MySelf Instance
        {
            get { return _mySelf ?? (_mySelf = new MySelf()); } // NON Thread safe
            //get { return ThreadTools.AssignWhenNotInitialized(_lockObject, ref _mySelf, () => new MySelf()); } // Thread safe
        }
    }

    public static class ThreadTools
    {
        public static T AssignWhenNotInitialized<T>(object lockObject, ref T instance, Func<T> createNew) where T:class 
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = createNew();
                    }
                }
            }
            return instance;
        }

    }

    public class MySelf
    {
        public string SomePublicField;
    }

    [TestFixture]
    public class Class1Test
    {
        [Test]
        public void IsItThreadSafe()
        {
            var failed = false;

            var threadList = new List<Thread>();
            for (int i = 0; i < 300; i++)
            {
                var thread = new Thread(() => DoSomeNotThreadSafeOperation(ref failed));
                threadList.Add(thread);
            }
            failed = false;
            threadList.ForEach(x => x.Start());
            threadList.ForEach(x => x.Join());
            failed.Should().BeFalse();
        }

        private void DoSomeNotThreadSafeOperation(ref bool failed)
        {
            for (int i = 0; i < 300; i++)
            {
                var previousInstance = Class1.Instance;

                try
                {
                    previousInstance.Should().Be(Class1.Instance);

                }
                catch (Exception)
                {
                    failed = true;                    
                }
            }
        }
    }

}
