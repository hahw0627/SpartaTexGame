using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// 아이템
struct Item
{
    public string name;
    public int price;
    public string effect;
    public string description;
    public bool equipped;

    // 생성
    public Item(string name, int price, string effect, string description, bool equipped = false)
    {
        this.name = name;
        this.price = price;
        this.effect = effect;
        this.description = description;
        this.equipped = equipped;
    }
}

class Program
{
    // 최대 인벤토리 크기
    const int MAX_INVENTORY_SIZE = 10;

    // 아이템 배열
    static Item[] inventory = new Item[MAX_INVENTORY_SIZE];

    // 캐릭터 정보
    static string name = "Chad";
    static string job = "전사";
    static int level = 1;
    static int attack = 10;
    static int defense = 5;
    static int health = 0;
    static int gold = 1500;

    // 상점 목록
    static Item[] shopInventory = new Item[]
    {
        new Item("수련자 갑옷", 1000, "방어력 +5", "수련에 도움을 주는 갑옷입니다."),
        new Item("무쇠갑옷", 1500, "방어력 +9", "무쇠로 만들어져 튼튼한 갑옷입니다."),
        new Item("스파르타의 갑옷", 3500, "방어력 +15", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다."),
        new Item("낡은 검", 600, "공격력 +2", "쉽게 볼 수 있는 낡은 검입니다."),
        new Item("청동 도끼", 1500, "공격력 +5", "어디선가 사용됐던 거 같은 도끼입니다."),
        new Item("스파르타의 창", 2000, "공격력 +7", "스파르타의 전사들이 사용했다는 전설의 창입니다.")
    };

    // 인벤토리 관리
    static void ManageInventory()
    {
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        while(true)
        {
            DisplayInventory();

            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    EquipItem();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    break;
            }
        }
    }

    // 장착 아이템 관리
    static void EquipItem()
    {
        Console.WriteLine("\n인벤토리 - 장착관리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        while(true)
        {
            DisplayInventory();

            Console.WriteLine("원하시는 아이템 번호를 입력해주세요. (0: 나가기)\n>> ");
            string input = Console.ReadLine();

            if (input == "0")
                return;

            if(!int.TryParse(input, out int index) || index < 1 || index > MAX_INVENTORY_SIZE)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                continue;
            }

            if (inventory[index -1].equipped)
            {
                inventory[index-1].equipped = false;
                Console.WriteLine($"\n{inventory[index - 1].name} 장착 해제\n");
            }
            else
            {
                inventory[index - 1].equipped = true;
                Console.WriteLine($"\n{inventory[index - 1].name} 장착 완료\n");
            }

            DisplayCharacterStatus();
        }
    }

    // 캐릭터 정보표시
    static void DisplayCharacterStatus()
    {
        Console.WriteLine("\n상태 보기");
        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

        Console.WriteLine($"Lv. {level:D2}\n{name} ( {job})");

        int totalAttack = attack;
        int totalDefense = defense;

        for (int i = 0; i < MAX_INVENTORY_SIZE; i++)
        {
            if (inventory[i].equipped)
            {
                if (inventory[i].effect.Contains("공격력"))
                    totalAttack += ExtractStatValue(inventory[i].effect);
                else if (inventory[i].effect.Contains("방어력"))
                    totalDefense += ExtractStatValue(inventory[i].effect);
            }
        }

        Console.WriteLine($"공격력 : {totalAttack}");
        Console.WriteLine($"방어력 : {totalDefense}");
        Console.WriteLine($"체 력 : {health}");
        Console.WriteLine($"Gold : {gold} G\n");

        Console.WriteLine("0. 나가기\n");
    }

    static int ExtractStatValue(string effect)
    {
        string[] words = effect.Split(' ');
        foreach (string word in words)
        {
            if (int.TryParse(word, out int value))
                return value;
        }
        return 0;
    }

    // 인벤토리 표시
    static void DisplayInventory()
    {
        Console.WriteLine("인벤토리");
        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < MAX_INVENTORY_SIZE; i++)
        {
            if (inventory[i].name != null)
            {
                string equipped = inventory[i].equipped ? "[E]" : "";
                Console.WriteLine($"{i + 1}. {equipped}{inventory[i].name} | {inventory[i].effect} | {inventory[i].description}");
            }
        }

        Console.WriteLine("\n0. 나가기\n");
    }

    // 상점 표시
    static void DisplayShop()
    {
        Console.WriteLine("상점");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

        Console.WriteLine("[보유 골드]");
        Console.WriteLine($"{gold} G\n");

        Console.WriteLine("[아이템 목록]");
        for (int i = 0; i < shopInventory.Length; i++)
        {
            string status = shopInventory[i].equipped ? "구매완료" : $"{shopInventory[i].price} G";
            Console.WriteLine($"- {shopInventory[i].name} | {shopInventory[i].effect} | {shopInventory[i].description} | {status}");
        }

        Console.WriteLine("\n1. 아이템 구매");
        Console.WriteLine("0. 나가기\n");
    }

    // 아이템 구매
    static void BuyItem()
    {
        Console.WriteLine("\n상점 - 아이템 구매");
        Console.WriteLine("필요한 아이템을 얻을 수 있는 상품입니다.\n");

        while (true)
        {
            DisplayShop();

            Console.WriteLine("원하시는 아이템 번호를 입력해주세요. (0: 나가기)\n>> ");
            string input = Console.ReadLine();

            if (input == "0")
                return;

            if (!int.TryParse(input, out int index) || index < 1 || index > shopInventory.Length)
            {
                Console.WriteLine("\n잘못된 입력입니다.\n");
                continue;
            }

            if (shopInventory[index - 1].equipped)
            {
                Console.WriteLine("\n이미 구매한 아이템입니다.\n");
                continue;
            }

            if (gold >= shopInventory[index - 1].price)
            {
                gold -= shopInventory[index - 1].price;
                shopInventory[index - 1].equipped = true;
                AddToInventory(shopInventory[index - 1]);
                Console.WriteLine("\n구매를 완료했습니다.\n");
            }
            else
            {
                Console.WriteLine("\nGold 가 부족합니다.\n");
            }
         }
    }

    // 인벤토리에 아이템 추가
    static void AddToInventory(Item item)
    {
        for(int i = 0; i < MAX_INVENTORY_SIZE; i++)
        {
            if (inventory[i].name == null)
            {
                inventory[i] = item;
                break;
            }
        }
    }

    // 메인 화면
    static void Main(string[] args)
    {
        Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
        Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
        
        while(true)
        {
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>> ");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    DisplayCharacterStatus();
                    Console.WriteLine("\n원하시는 행동을 입력해주세요.\n>> ");
                    break;
                case "2":
                    ManageInventory();
                    break;
                case "3":
                    BuyItem();
                    break;
                default:
                    Console.WriteLine("\n잘못된 입력입니다.\n");
                    break;
            }
        }
    }
}
