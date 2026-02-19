namespace api.DTOs;

public record MessageRequest(
    string Message
);

public record MessageResponseDto(
    string Message,
    string SenderUserName,
    DateTime TimeStamp
);