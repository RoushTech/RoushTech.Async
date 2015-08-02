namespace RoushTech.Async.Tests.Tasks
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class CatchExtension
    {        
        [Fact]
        public void CatchSingle()
        {
            var caught = false;
            Task.Factory
                .StartNew(() => { throw new InvalidOperationException("test"); })
                .Catch((exception) =>
                {
                    Assert.Equal("test", exception.Message);
                    caught = true;
                })
                .Wait();
            Assert.True(caught, "Caught flag false");
        }

        [Fact]
        public void CatchChain()
        {
            var caught = false;
            Task.Factory
                .StartNew(() => { throw new InvalidOperationException("test"); })
                .Then((t) => Assert.True(false, "Then called after thrown task."))
                .Catch((exception) =>
                {
                    Assert.Equal("test", exception.Message);
                    caught = true;
                })
                .Wait();
            Assert.True(caught, "Caught flag false");
        }

        [Fact]
        public void CatchContinueChain()
        {
            bool caught = false,
                continued = false,
                faulted = true;
            Task.Factory
                .StartNew(() => { throw new InvalidOperationException("test"); })
                .Then((t) => Assert.True(false, "Then called after thrown task."))
                .Catch((exception) =>
                {
                    Assert.Equal("test", exception.Message);
                    caught = true;
                })
                .Then((t) => 
                {
                    faulted = t.IsFaulted;
                    continued = true;
                })
                .Wait();
            Assert.True(caught, "Caught flag false");
            Assert.True(continued, "Continued flag false");
            Assert.False(faulted, "Faulted flag true");
        }
    }
}
