using UnityEngine;

class BazaScript: MonoBehaviour
{
    public static BazaScript instance;
    public float live = 100f;



    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
        }

        instance = this;
    }
    public void Damage(float amount)
    {
       live -= amount;

        if (live <= 0f)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Debug.LogWarning("You died!");
    }
}

