using UnityEngine;
using Mirror;
using Zenject;

public class ZenjectNetworkSpawner : NetFancyBehaviour
{
    [SerializeField] private NetworkIdentity[] networkPrefabs;

    private IInstantiator _instantiator;

    [Inject]
    public void Construct(IInstantiator instantiator)
    {
        _instantiator = instantiator;
    }

    public override void OnStartClient()
    {
        foreach (var prefab in networkPrefabs)
        {
            if (prefab == null)
            {
                Debug.LogWarning("ZenjectNetworkSpawner: network prefab reference is null");
                continue;
            }

            if (prefab.assetId == 0)
            {
                Debug.LogError($"ZenjectNetworkSpawner: prefab '{prefab.name}' has no NetworkIdentity assetId. Assign an assetId or use a scene object spawn handler.");
                continue;
            }

            NetworkClient.RegisterPrefab(prefab.gameObject, SpawnPrefab, UnspawnPrefab);
        }
    }

    private GameObject SpawnPrefab(SpawnMessage msg)
    {
        if (!NetworkClient.GetPrefab(msg.assetId, out var prefab))
        {
            Debug.LogError($"ZenjectNetworkSpawner: failed to get prefab for assetId {msg.assetId}");
            return null;
        }

        if (prefab == null)
        {
            Debug.LogError($"ZenjectNetworkSpawner: registered prefab for assetId {msg.assetId} is null");
            return null;
        }

        GameObject instance = _instantiator.InstantiatePrefab(prefab.gameObject, msg.position, msg.rotation, null);
        return instance;
    }

    private void UnspawnPrefab(GameObject spawned)
    {
        Destroy(spawned);
    }

    private void OnDestroy()
    {
        if (!NetworkClient.active) return;

        foreach (var prefab in networkPrefabs)
        {
            if (prefab == null || prefab.assetId == 0) continue;
            NetworkClient.UnregisterPrefab(prefab.gameObject);
        }
    }
}
