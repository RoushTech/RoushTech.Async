namespace RoushTech.Async.Tests.Tasks
{
    using System.Threading.Tasks;
    using Xunit;

    public class ThenExtension
    {

        [Fact]
        public void ThenChain()
        {
            var i = 0;
            Task.Factory
                .StartNew(() => { i++; })
                .Then((t) => { i++; })
                .Wait();
            Assert.Equal(2, i);
        }
    }
}
