using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalcHost.Controllers;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace CalcHost
{
    public class GreeterService : Greeter.GreeterBase
    {
        public override Task<CalculateResponse> Calculate(CalculateRequest request, ServerCallContext context)
        {
            return Task.FromResult(new CalculateResponse
            {
                Response = new Calculator(new CalculationService()).Calculate(request.Expr)
            });
        }
    }
}