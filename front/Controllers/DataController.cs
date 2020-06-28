using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Data.Data;

namespace front.Controllers
{

  [Route("[controller]")]
  [ApiController]
  public class DataController : ControllerBase
  {
    private readonly ILogger<DataController> _logger;
    private readonly DataClient _client;

    public DataController(ILogger<DataController> logger, DataClient client)
    {
      _logger = logger;
      _client = client;
    }

    [HttpGet("")]
    public async IAsyncEnumerable<DataPoint> GetData()
    {
      var response = await _client.GetDataAsync(new DataRequest
      {
      });

      foreach (var point in response.Result)
        yield return point;

    }

  }
}
