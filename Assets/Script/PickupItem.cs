using UnityEngine;

public class PickupItem : MonoBehaviour
{
    [Header("아이템 정보")]
    [SerializeField] private string itemName = "아이템";
    [SerializeField] private int amount = 1;

    // (주의: QuestManager가 없다면 주석 처리)
    [SerializeField] QuestManager questManager;

    [Header("화면 연출")]
    [SerializeField] private float rotationSpeed = 60f;
    [SerializeField] private float floatingHeight = 0.15f;
    [SerializeField] private float floatingSpeed = 2f;

    private Vector3 startPosition;
    private SimpleInventory inventory;

    public void Awake()
    {
        // questManager = FindAnyObjectByType<QuestManager>(); // 필요시 주석 해제
        inventory = FindAnyObjectByType<SimpleInventory>();
    }

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 아이템 회전 연출
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // 아이템 위아래 둥둥 떠다니는 연출
        float newY = startPosition.y + Mathf.Sin(Time.time * floatingSpeed) * floatingHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // 플레이어가 이 아이템을 주웠을 때 실행되는 함수
    public void Collect()
    {
        if (inventory == null)
        {
            Debug.Log("SimpleInventory를 찾을 수 없습니다.");
            return;
        }

        // 인벤토리가 꽉 차서 AddItem이 false를 반환하면 줍기 취소
        if (inventory.AddItem(itemName, amount) == false)
        {
            return;
        }

        Debug.Log(itemName + " " + amount + "개 획득");

        if (questManager != null)
        {
            // questManager.AddQuestItem(itemName, amount); // 퀘스트 기능이 필요하면 주석 해제
        }

        // 아이템을 인벤토리에 넣었으므로 맵에서 삭제
        Destroy(gameObject);
    }

    // 💡 [추가된 부분] PlayerPickup에서 아이템 이름을 가져갈 수 있게 해주는 함수
    public string GetItemName()
    {
        return itemName;
    }
}