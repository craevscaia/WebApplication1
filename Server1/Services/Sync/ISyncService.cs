﻿namespace ClientServer.Services.Sync;

public interface ISyncService
{
    public void SyncData(CancellationToken cancellationToken);
}