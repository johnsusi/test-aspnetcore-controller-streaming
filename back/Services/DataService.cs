using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using InfluxDB.Client;
using Microsoft.Extensions.Logging;
using static Data.Data;

namespace back
{
  public class DataService : DataBase
  {
    private readonly ILogger<DataService> _logger;
    public DataService(ILogger<DataService> logger)
    {
      _logger = logger;
    }

    public override async Task<DataResponse> GetData(DataRequest request, ServerCallContext context)
    {

      using var influxDBClient = InfluxDBClientFactory.Create("http://influxdb:8086", "".ToCharArray());

      var flux = "from(bucket:\"test\") |> range(start: 0)";

      var queryApi = influxDBClient.GetQueryApi();

      var response = new DataResponse();

      await queryApi.QueryAsync(flux, (cancellable, record) =>
      {
        response.Result.Add(new DataPoint
        {
          Name = record.GetField(),
          Value = (double)record.GetValue(),
          Timestamp = Timestamp.FromDateTime(record.GetTimeInDateTime().Value)
        });
      }, exception =>
      {
        throw new RpcException(new Status(StatusCode.Internal, exception.Message));
      }, () =>
      {
      });

      return response;

    }
  }
}
