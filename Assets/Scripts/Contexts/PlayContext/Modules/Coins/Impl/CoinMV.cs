using strange.extensions.mediation.impl;
using UnityEngine;

public class CoinMV : View, ICoin
{
    private CoinModel _coinModel;

    public CoinModel Model => _coinModel;

    public void Initialize(CoinModel coinModel)
    {
        _coinModel = coinModel;
    }

    private void Update()
    {
        if (_coinModel.IsDead)
        {
            Destroy(gameObject);
        }

        transform.Rotate(0f, 1f, 0f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _coinModel.IsLanded = true;
        }
    }
}
