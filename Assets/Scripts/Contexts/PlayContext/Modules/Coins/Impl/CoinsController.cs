using System.Collections;
using System.Collections.Generic;

using strange.extensions.context.api;

using UnityEngine;

public class CoinsController : ICoinsController
{
    private const float CoinLifeDuration = 5f;
    private const float CoinSpawnCooldown = .5f;

    private readonly string WarningObjectLocation = "GUI/WarningObject";

    private int _coinsCollected;
    private float _coinSpawnTimer;
    private GameObject _coinPrefab;
    private GameObject _coinsSpawnRoot;
    private List<CoinModel> _coins = new List<CoinModel>(5);

    [Inject] public CollectedCoinsUpdateSignal CollectedCoinsUpdateSignal { get; set; }
    [Inject] public IAssetBundleProvider AssetBundleProvider { get; set; }
    [Inject(ContextKeys.CONTEXT_VIEW)] public GameObject ContextRoot { get; set; }

    [PostConstruct]
    private void Initialize()
    {
        _coinPrefab = AssetBundleProvider.GetAsset("gamelvl", "Coin");
        
        if (_coinPrefab == null)
        {
            _coinPrefab = Resources.Load<GameObject>(WarningObjectLocation);
        }

        _coinsSpawnRoot = new GameObject("CoinsRoot");
        _coinsSpawnRoot.transform.parent = ContextRoot.transform;
    }

    public void Update(float deltaTime)
    {
        UpdateCoinModels(deltaTime);

        if (_coinSpawnTimer >= CoinSpawnCooldown)
        {
            _coinSpawnTimer %= CoinSpawnCooldown;

            SpawnCoin();
        }

        _coinSpawnTimer += deltaTime;
    }

    public void CollectCoin(CoinModel coin)
    {
        if (_coins.Contains(coin) == false)
            return;
        
        coin.IsDead = true;

        _coins.Remove(coin);

        _coinsCollected++;
        CollectedCoinsUpdateSignal.Dispatch(_coinsCollected);
    }

    private void UpdateCoinModels(float deltaTime)
    {
        for (int i = _coins.Count - 1; i >= 0; i--)
        {
            var coin = _coins[i];

            if (coin.IsLanded)
            {
                coin.LifeTime += deltaTime;
            }

            if (coin.LifeTime >= CoinLifeDuration)
            {
                coin.IsDead = true;
                _coins.RemoveAt(i);
            }
        }
    }

    private void SpawnCoin()
    {
        var coinGO = GameObject.Instantiate(_coinPrefab, _coinsSpawnRoot.transform);
        coinGO.transform.position = new Vector3(Random.Range(50, -50), 50, Random.Range(30, -30));

        var coinMv = coinGO.AddComponent<CoinMV>();
        var coinModel = new CoinModel();
        
        coinMv.Initialize(coinModel);

        _coins.Add(coinModel);
    }
}