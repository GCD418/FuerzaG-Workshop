using FuerzaG.Domain.Entities;

namespace FuerzaG.Domain.Ports;

public interface ILoginRepository
{
    public UserAccount? GetByUserName(string userName);

    public bool IsUserNameUsed(string userName);
}