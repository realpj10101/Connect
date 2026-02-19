using api.DTOs;
using api.DTOs.Helpers;
using api.Extensions;
using api.Interfaces;
using Microsoft.AspNetCore.SignalR;
using MongoDB.Bson;

namespace api.SignalR;

public class ChatHub : Hub
{
    private IRoomRepository _roomRepository;
    private ITokenService _tokenService;

    public ChatHub(IRoomRepository roomRepository, ITokenService tokenService)
    {
        _roomRepository = roomRepository;
        _tokenService = tokenService;
    }

    public async Task SendMessageAsync(MessageRequest req, ObjectId roomId, CancellationToken cancellationToken)
    {
        string? hashedUserId = Context?.User?.GetHashedUserId();

        if (hashedUserId is null)
        {
            throw new HubException("User is not authenticated.");
        }

        ObjectId? userId =  await _tokenService.GetActualUserIdAsync(hashedUserId, cancellationToken);

        if (userId is null)
        {
            throw new HubException("User ID could not be retrieved.");
        }

        OperationResult<MessageResponseDto> res = await _roomRepository.SavedMessageAsync(req, userId.Value, roomId, cancellationToken);

        await Clients.All.SendAsync("ReceiveMessage", res.Result, cancellationToken);
    }
}