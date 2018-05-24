using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModernNotes.Web;

namespace ModenNotes.IntegrationTests
{
    [TestClass]
    public class TestServerSetupTests
    {
	    private readonly TestServer _server;
	    private readonly HttpClient _client;

	    public TestServerSetupTests()
	    {
		    _server = new TestServer(new WebHostBuilder()
			    .UseStartup<Startup>());
		    _client = _server.CreateClient();
	    }

	    [TestMethod]
	    public async Task PingReturnsOk()
	    {
		    var response = await _client.GetAsync("/api/ping");
		    response.EnsureSuccessStatusCode();
		    var responseString = await response.Content.ReadAsStringAsync();
		    
			Assert.AreEqual("OK", responseString);
	    }
	}
}
