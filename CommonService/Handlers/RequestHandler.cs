using System;
using System.Collections.Generic;
using System.Linq;
using CommonService.Contracts;

namespace CommonService.Handlers
{
    public abstract class RequestHandler<TResponse, TRequest>
        where TRequest : IRequest, new()
        where TResponse : IResponse, new()
    {
        /// <summary>
        /// Abstract method to execute requests
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TResponse Execute(TRequest request)
        {
            // Initialize
            Initialize();

            // Validate
            List<ValidationError> ValidationErrors = Validate(request);

            if (ValidationErrors.Any())
                return new TResponse() { ValidationErrors = ValidationErrors };

            // Process
            TResponse response = Process(request);

            return response;
        }

        //Virtual method to initialize handler
        protected virtual void Initialize()
        {
            // If applicable, override specific initialization in the derived class
        }

        /// <summary>
        /// Virtual method to validate request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual List<ValidationError> Validate(TRequest request)
        {
            return new List<ValidationError>();
        }

        /// <summary>
        /// Virtual method to process request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual TResponse Process(TRequest request)
        {
            return new TResponse();
        }
    }
}