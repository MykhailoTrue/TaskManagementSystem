﻿using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Net;
using TMS.EF.NTier.Common.DTO.Errors;
using TMS.EF.NTier.Common.Exceptions;

namespace TMS.EF.NTier.Web.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate _next)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (SqlException sqlException)
            {
                await HandleExceptionAsync(context, sqlException, HttpStatusCode.BadRequest);
            }
            catch (CustomException ex)
            {
                await HandleExceptionAsync(context, ex, ex.StatusCode);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync
            (HttpContext context,
            Exception exception,
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            string errorId = Guid.NewGuid().ToString();
            _logger.LogError(exception, exception.Message, errorId);


            var errorResult = new ErrorResultDTO()
            {
                Source = exception.TargetSite?.DeclaringType?.FullName,
                Exception = exception.Message.Trim(),
                ErrorId = errorId,
                SupportMessage = $"Provide the Error Id: {errorId} to the support team for further analysis.",
                StatusCode = (int)statusCode,
            };

            var response = context.Response;

            if (!response.HasStarted)
            {
                response.ContentType = "application/json";
                response.StatusCode = errorResult.StatusCode;
                await response.WriteAsync(JsonConvert.SerializeObject(errorResult));
            }
        }
    }
}
