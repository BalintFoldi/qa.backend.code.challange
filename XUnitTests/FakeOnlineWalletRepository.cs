using Betsson.OnlineWallets.Data.Models;
using Betsson.OnlineWallets.Data.Repositories;

public class FakeOnlineWalletRepository : IOnlineWalletRepository
{
    // This property is used to simulate the "last entry" that GetLastOnlineWalletEntryAsync will return
    public OnlineWalletEntry? LastEntry { get; set; }

    // This list will store all entries added via InsertOnlineWalletEntryAsync
    public List<OnlineWalletEntry> Entries { get; } = new List<OnlineWalletEntry>();

    public Task<OnlineWalletEntry?> GetLastOnlineWalletEntryAsync()
    {
        // Return the simulated "last entry"
        return Task.FromResult(LastEntry);
    }

    public Task InsertOnlineWalletEntryAsync(OnlineWalletEntry onlineWalletEntry)
    {
        // Simulate inserting the entry into a repository
        Entries.Add(onlineWalletEntry);
        LastEntry = onlineWalletEntry; // Assume the inserted entry is the last entry
        return Task.CompletedTask;
    }
}
