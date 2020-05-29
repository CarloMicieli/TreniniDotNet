using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Grpc.Core;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.GrpcServices.Infrastructure
{
    public abstract class DefaultGrpcPresenter<TOutput, TResponse> : IOutputPortStandard<TOutput>
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

        public void InvalidRequest(IList<ValidationFailure> failures)
        {
            string details = string.Join(", ", failures.Select(it => it.ToString()));
            throw new RpcException(new Status(StatusCode.InvalidArgument,
                $"Invalid request [Details: {details}]"));
        }

        public void Error(string? message)
        {
            throw new RpcException(new Status(StatusCode.Internal, message));
        }

        protected void AlreadyExists(string message) =>
            throw new RpcException(new Status(StatusCode.AlreadyExists, message));
    }
}
