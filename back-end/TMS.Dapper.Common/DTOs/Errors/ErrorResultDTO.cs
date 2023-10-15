﻿namespace TMS.Dapper.Common.DTOs.Errors
{
    public class ErrorResultDTO
    {
        public List<string> Messages { get; set; } = new();

        public string? Source { get; set; }
        public string? Exception { get; set; }
        public string? ErrorId { get; set; }
        public string? SupportMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
