using UnityEngine;
using TMPro;  // TextMeshPro k�t�phanesini ekliyoruz

public class elmaa : MonoBehaviour
{
    public float hareketHizi = 5f;
    public float yukariHiz = 5f;
    public float asagiHiz = 5f;
    public GameObject elmaPrefab;   // Elma prefab'�
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

        // Space tu�u
        if (Input.GetKey(KeyCode.Space))
        {
            transform.Translate(Vector3.up * yukariHiz * Time.deltaTime);
        }

        // E tu�u ile elma olu�turma
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnElma();
        }
    }

    private void SpawnElma()
    {
        // Elma olu�tur
        GameObject yeniElma = Instantiate(elmaPrefab, transform.position, Quaternion.identity);

        // Elma i�in yer�ekimi
        Rigidbody2D rb = yeniElma.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 1;
        }

        // Elma'n�n temas edece�i event i�in Collider eklenmeli (�rne�in trigger olarak)
        yeniElma.GetComponent<Collider2D>().isTrigger = true;
    }

    private void UpdateUI()
    {
        // Can ve skor de�erlerini g�ncelle
        canText.text = "Can: " + can.ToString();
        skorText.text = "Skor: " + skor.ToString();
    }

    public void SepeteTemasEtti()
    {
        skor++;
        UpdateUI();

        // Sepeti random yere ta��
        Transform rastgeleSepet = sepetler[Random.Range(0, sepetler.Length)];
        transform.position = rastgeleSepet.position;
    }

    public void YereTemasEtti()
    {
        // Yere temasta can� azalt
        can--;
        UpdateUI();

        if (can <= 0)
        {
            Debug.Log("Oyun Bitti!");
        }
    }

    // Karakter ile Elma Temas�
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
        skor++;   // Skoru artt�r
        UpdateUI(); // UI'yi g�ncelle
    }
}