namespace Api.DTOs.Requests
{
    public record RegisterUserRequest(
        string Nickname,
        string Email,
        int Age,
        string Password
    );
}
