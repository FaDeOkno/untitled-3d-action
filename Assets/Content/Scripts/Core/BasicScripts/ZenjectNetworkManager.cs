using Mirror;
using Zenject;
using UnityEngine;

public class ZenjectNetworkManager : NetworkManager
{
    [Inject] private DiContainer _container;

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (playerPrefab != null)
        {
            NetworkClient.UnregisterPrefab(playerPrefab);
            NetworkClient.RegisterPrefab(playerPrefab, SpawnPrefab, UnspawnPrefab);
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var startPos = GetStartPosition();
        var position = startPos != null ? startPos.position : Vector3.zero;
        var rotation = startPos != null ? startPos.rotation : Quaternion.identity;

        var player = _container.InstantiatePrefab(playerPrefab, position, rotation, null);
        player.name = $"{playerPrefab.name} [connId={conn.connectionId}]";

        NetworkServer.AddPlayerForConnection(conn, player);
    }

    private GameObject SpawnPrefab(SpawnMessage msg)
    {
        if (!NetworkClient.GetPrefab(msg.assetId, out var prefab))
        {
            Debug.LogError($"ZenjectNetworkManager: failed to get prefab for assetId {msg.assetId}");
            return null;
        }

        return _container.InstantiatePrefab(prefab.gameObject, msg.position, msg.rotation, null);
    }

    private void UnspawnPrefab(GameObject spawned)
    {
        Destroy(spawned);
    }
}
