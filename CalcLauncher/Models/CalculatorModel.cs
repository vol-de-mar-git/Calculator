using System.Text;
using CalcHost;
using Grpc.Net.Client;

namespace CalcLauncher.Models
{
    public class CalculatorModel
    {
        private static readonly GrpcChannel _channel = GrpcChannel.ForAddress("https://localhost:5001/");

        public Greeter.GreeterClient CalcClient { get; } = new Greeter.GreeterClient(_channel);
    }
}