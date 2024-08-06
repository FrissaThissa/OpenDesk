using Shared.Models;

namespace Frontend.Services;

public class StateService
{
    private UserDto? _user;

    public UserDto? User
    {
        get => _user;
        set
        {
            _user = value;
            NotifyStateChanged();
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
