namespace Eskort.Services.AuthAPI.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; } = "none";
        public string? SuccessMessage { get; set; } = "Not Successful";
    }
}
