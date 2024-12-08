using UnityEngine;
using TMPro;  // TextMeshPro kütüphanesini ekliyoruz

public class elmaa : MonoBehaviour
{
    public float hareketHizi = 5f;
    public float yukariHiz = 5f;
    public float asagiHiz = 5f;
    public GameObject elmaPrefab;   // Elma prefab'ý
    public TextMeshProUGUI canText;            // Can (TextMeshPro)
    public TextMeshProUGUI skorText;           // Skor (TextMeshPro)
    public Transform[] sepetler;    // Sepetlerin yerleri
    public int can = 3;
    public int skor = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // WASD
        transform.Translate(new Vector3(horizontal, 0, vertical) * hareketHizi * Time.deltaTime);

        // Sol CTRL
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate(Vector3.down * asagiHiz * Time.deltaTime);
        }

        // Space tuþu
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * yukariHiz * Time.deltaTime);
        }

        // E tuþu ile elma oluþturma
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnElma();
        }
    }

    private void SpawnElma()
    {
        // Elma oluþtur
        GameObject yeniElma = Instantiate(elmaPrefab, transform.position, Quaternion.identity);

        // Elma için yerçekimi
        Rigidbody2D rb = yeniElma.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1;
        }

        // Elma'nýn temas edeceði event için Collider eklenmeli (örneðin trigger olarak)
        yeniElma.GetComponent<Collider2D>().isTrigger = true;
    }

    private void UpdateUI()
    {
        // Can ve skor deðerlerini güncelle
        canText.text = "Can: " + can.ToString();
        skorText.text = "Skor: " + skor.ToString();
    }

    public void SepeteTemasEtti()
    {
        skor++;
        UpdateUI();

        // Sepeti random yere taþý
        Transform rastgeleSepet = sepetler[Random.Range(0, sepetler.Length)];
        transform.position = rastgeleSepet.position;
    }

    public void YereTemasEtti()
    {
        // Yere temasta caný azalt
        can--;
        UpdateUI();

        if (can <= 0)
        {
            Debug.Log("Oyun Bitti!");
        }
    }

    // Karakter ile Elma Temasý
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Elma")) // Elma tag'ine sahip objelerle temas
        {
            SkorArtir();
            Destroy(other.gameObject); // Elma yok edilsin
        }
    }

    void SkorArtir()
    {
        skor++;   // Skoru arttýr
        UpdateUI(); // UI'yi güncelle
    }
}