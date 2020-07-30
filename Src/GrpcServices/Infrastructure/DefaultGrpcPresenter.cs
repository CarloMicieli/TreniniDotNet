using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.GrpcServices.Infrastructure
{
    public abstract class DefaultGrpcPresenter<TOutput, TResponse> : IStandardOutputPort<TOutput>
        where TOutput : IUseCaseOutput
        where TResponse : class
    {
        private readonly Func<TOutput, TResponse> _mapping;

        public TResponse Response { get; private set; } = null!;

        protected DefaultGrpcPresenter(Func<TOutput, TResponse> mapping)
        {
            _mapping = mapping;
        }

        public void Standard(TOutput output)
        {
            this.Response = _mapping(output);
        }

        public void InvalidRequest(IEnumerable<ValidationError> validationErrors)
        {
            string details = string.Join(", ", validationErrors.Select(it => it.ToString()));
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                $"Invalid request [Details: {details}]"));
        }

        public void Error(string? message)
        {
            throw new RpcException(new Status(StatusCode.Internal, message));
        }

        protected void AlreadyExists(string message) =>
            throw new RpcException(new Status(StatusCode.AlreadyExists, message));

        protected void NotFound(string message) =>
            throw new RpcException(new Status(StatusCode.NotFound, message));
    }
}
