using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtanDungeonProject
{
    /*
     * 구현 필수 사항
     * 1) 게임 시작 화면 V
     * 2) 상태 보기 V
     * 3) 인벤토리 V
     * 4) 장착 관리 V
     * 5) 상점 V
     * 6) 아이템 구매 V
     */

    /*
     * 도전 사항
     * 1) 휴식 기능 구현 V
     * 2) 판매 기능 구현 V
     * 3) 장착 개선 V
     * 4) 레벨업 기능 구현
     * 5) 던전 기능 구현
     * 6) 데이터 저장 구현
     */

    /// <summary>
    /// 고찰
    /// 1. 사용자 입력받는 부분을 반복문을 통해서 구현하였는데, 아무리 생각해봐도 생노가다같음. 이를 함수로 빼서 좀 더 효율적으로 바꾸는 방법이 있을까?
    /// 2. 현재 파일에 클래스를 여러개 선언하고 사용하고 있음, 파일을 나누고 일부 클래스는 옮겨두면 가독성이 좋을 거 같은데 익숙하지 않아서 어떤식으로 해야할지 감이 안잡힘
    /// 3. 데이터 저장 구현의 경우 txt파일이나 Json파일 등으로 외부에 파일을 저장하고 이를 시작할 때 불러오는 방식으로 구현가능할 듯하다. 다만 이쪽도 사용한 경험이 별로 없어서 찾아봐야할듯함
    /// </summary>

    internal class TxtGame
    {
        public static void Main(string[] args)
        {
            RtanDungeon rd = new RtanDungeon();
            rd.GameStart();
        }

    }

    class RtanDungeon
    {
        // 캐릭터 정보 인스턴스
        Player player;
        Shop shop;
        List<Dungeon> dungeons;

        public void GameStart()
        {
            // 저장된 데이터 확인?
            // 캐릭터 설정?
            MakeCharacter();
            InitGame();
            ShowMainmenu();
        }

        void MakeCharacter() // 캐릭터 생성
        {
            Console.Clear();
            Console.WriteLine("<<캐릭터 생성>>\n");

            // 이름 입력
            Console.WriteLine("이름을 입력하세요");
            string name;
            while (true)
            {
                Console.Write(">> ");
                name = Console.ReadLine();
                if (name == null || name.Length <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }
                break;
            }

            // 직업 선택
            Console.WriteLine("\n직업을 선택하세요.\n1) 전사\n2) 마법사\n3) 도적\n4) 궁수\n");
            ChrClass chrClass;
            while (true)
            {
                Console.Write(">> ");
                string choice = Console.ReadLine();
                int choiceNum = 0;
                try
                {
                    choiceNum = int.Parse(choice);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }

                if (choiceNum < 1 || choiceNum > 4)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }

                chrClass = (ChrClass)choiceNum;
                break;
            }

            player = new Player(chrClass, name, 1, 100, 10, 5);
            player.AddItem(new Weapon("나뭇가지", "단순한 나뭇가지다.", 10, ItemType.Weapon, 3));
            player.AddItem(new Armor("허름한 천옷", "허름한 천으로 만들어진 옷이다.", 15, ItemType.Armor, 5));
        }

        void ShowMainmenu() // 메인메뉴
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("1) 상태 보기\n2) 인벤토리\n3) 상점\n4) 던전 가기\n5) 휴식 하기\n6) 종료하기\n");
                while (true)
                {
                    Console.Write(">> ");
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    switch (choiceNum)
                    {
                        case 1:
                            // 상태 확인
                            ShowStatus();
                            break;
                        case 2:
                            // 인벤토리 열기
                            ShowInventory();
                            break;
                        case 3:
                            // 상점 열기
                            OpenShop();
                            break;
                        case 4:
                            // 던전 입장
                            ShowDungeons();
                            break;
                        case 5:
                            // 휴식하기
                            Rest();
                            break;
                        case 6:
                            // 게임 종료
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ResetColor();
                            continue;
                    }
                    break;
                }
            }

        }

        void ShowStatus() // 캐릭터 스테이터스 표시
        {
            Console.Clear();
            if (player != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상태 보기");
                Console.ResetColor();
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

                string chad = "";
                switch (player.GetChrClass())
                {
                    case ChrClass.Warrior:
                        chad = "전사";
                        break;
                    case ChrClass.Wizard:
                        chad = "마법사";
                        break;
                    case ChrClass.Thief:
                        chad = "도적";
                        break;
                    case ChrClass.Archer:
                        chad = "궁수";
                        break;
                    default:
                        chad = "무직";
                        break;
                }

                Console.WriteLine($"이름 : {player.Name}\nLv. {player.Level}\nChad ( {chad} )\n공격력 : {player.GetAttackByItems()} (+{player.GetAttackOnlyItems()})\n방어력 : {player.GetDefenceByItems()} (+{player.GetDefenceOnlyItems()})\n체 력 : {player.Hp}\nGold : {player.Gold}G\n");

                Console.WriteLine("\n0. 나가기\n");

                Console.Write("원하시는 행동을 입력해주세요.\n>> ");
                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                    if (choiceNum != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    return;
                }

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("플레이어 정보가 없습니다.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }

        void ShowInventory() // 캐릭터 인벤토리 표시
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("인벤토리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");

                // 아이템 목록 출력함
                foreach(Item item in player.GetItems())
                {
                    if(item is Equipment)
                    {
                        Equipment equipment = (Equipment)item;
                        if(equipment.IsEquip) Console.Write("[E]");
                    }
                    item.ShowItemInfo();
                    Console.WriteLine();
                }

                Console.WriteLine("\n1. 장착 관리\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력하세요.");
                Console.Write(">> ");
                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    switch (choiceNum)
                    {
                        case 1:
                            // 장착 관리 메뉴
                            EquipMenu();
                            break;
                        case 0:
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ResetColor();
                            continue;
                    }
                    break;
                }
                


            }
            
        }

        void EquipMenu() // 캐릭터 장비 장착 화면
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.ResetColor();
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n");

                Console.WriteLine("[아이템 목록]");

                // 아이템 목록 출력함
                List<Item> items = player.GetItems();

                for (int i = 0; i < items.Count; i++)
                {
                    Console.Write($"- {i+1} ");
                    if (items[i] is Equipment)
                    {
                        Equipment equipment = (Equipment)items[i];
                        if (equipment.IsEquip) Console.Write("[E]");
                    }
                    items[i].ShowItemInfo();
                    Console.WriteLine();
                }

                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력하세요.");
                Console.Write(">> ");
                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    if (choiceNum > 0 && choiceNum <= items.Count)
                    {
                        try
                        {
                            player.EqipItem((Equipment)items[choiceNum-1]);
                        }
                        catch
                        {
                            Console.WriteLine("장착 불가능한 아이템입니다.");
                            continue;
                        }
                    }
                    else if (choiceNum == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
            }
        }

        void OpenShop() // 상점 물건 전시
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 구매할 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");
                shop.ShowItemsList();

                Console.WriteLine("\n1. 아이템 구매\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력하세요.");
                Console.Write(">> ");

                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    switch (choiceNum)
                    {
                        case 1:
                            OpenBuyMenu();
                            break;
                        case 2:
                            OpenSellMenu();
                            break;
                        case 0:
                            return;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ResetColor();
                            continue;
                    }
                    break;
                }
            }
        }

        void OpenBuyMenu() // 상점 아이템 구매
        {
            while(true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점 - 아이템 구매");
                Console.ResetColor();
                Console.WriteLine("필요한 아이템을 구매할 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");
                shop.ShowSellingItems();
                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력하세요.");
                Console.Write(">> ");
                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    if (choiceNum > 0 && choiceNum <= shop.GetSellingItems().Count)
                    {
                        player.BuyItem(choiceNum, shop);
                    }
                    else if (choiceNum == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
            }
        }

        void OpenSellMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("상점 - 아이템 판매");
                Console.ResetColor();
                Console.WriteLine("불필요한 아이템을 판매할 수 있는 상점입니다.\n");

                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Gold} G\n");

                Console.WriteLine("[아이템 목록]");

                List<Item> items = player.GetItems();
                for(int i=0;i< items.Count;i++)
                {
                    Console.Write($"- {i + 1} ");
                    if (items[i] is Equipment)
                    {
                        Equipment equipment = (Equipment)items[i];
                        if (equipment.IsEquip) Console.Write("[E]");
                    }
                    items[i].ShowItemInfo();
                    Console.WriteLine($"\t| {items[i].Price} G");
                }

                Console.WriteLine("\n0. 나가기\n");
                Console.WriteLine("원하시는 행동을 입력하세요.");
                Console.Write(">> ");
                
                while (true)
                {
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    if (choiceNum > 0 && choiceNum <= player.GetItems().Count)
                    {
                        player.SellItem(choiceNum);
                    }
                    else if (choiceNum == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                    break;
                }
            }
        }

        void ShowDungeons() // 던전 메뉴 진입
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor= ConsoleColor.DarkYellow;
                Console.WriteLine("던전입장");
                Console.ResetColor();
                Console.WriteLine("이곳에서 던전으로 들어가기전 활등을 할 수 있습니다.\n");

                for(int i=0;i<dungeons.Count;i++)
                {
                    Console.Write($"{i + 1}. ");
                    dungeons[i].ShowDungeonInfo();
                }

                Console.WriteLine("0. 나가기\n\n원하시는 행동을 입력해주세요.");
                while (true)
                {
                    Console.Write(">> ");
                    string choice = Console.ReadLine();
                    int choiceNum = 0;
                    try
                    {
                        choiceNum = int.Parse(choice);
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }

                    if(choiceNum > 0 && choiceNum <= dungeons.Count)
                    {
                        dungeons[choiceNum-1].EnterTheDungeon(player);
                        break;
                    }
                    else if (choiceNum == 0)
                    {
                        return;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                    }
                }
            }
        }

        void Rest() // 휴식
        {
            Console.Clear();
            Console.ForegroundColor= ConsoleColor.DarkYellow;
            Console.WriteLine("휴식하기");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("500 ");
            Console.ResetColor();
            Console.Write("G 를 내면 체력을 바로 회복할 수 있습니다. (보유 골드 : ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{player.Gold}");
            Console.ResetColor();
            Console.WriteLine(" G)");

            Console.WriteLine("\n1. 휴식하기\n0. 나가기\n");
            Console.WriteLine("원하시는 행동을 입력하세요.");
            Console.Write(">> ");
            while (true)
            {
                string choice = Console.ReadLine();
                int choiceNum = 0;
                try
                {
                    choiceNum = int.Parse(choice);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }

                switch (choiceNum)
                {
                    case 1:
                        // 장착 관리 메뉴
                        if(player.Gold >= 500)
                        {
                            player.Hp = 100;
                            player.Gold -= 500;
                            Console.WriteLine("휴식을 완료했습니다.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Gold 가 부족합니다.");
                            Console.ResetColor();
                        }
                        Console.ReadKey();
                        break;
                    case 0:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("잘못된 입력입니다.");
                        Console.ResetColor();
                        continue;
                }
                break;
            }
        }

        void InitGame() // 게임 정보 초기화
        {
            shop = new Shop();
            shop.AddItem(new Weapon("부러진 직검", "날이 중간부터 부러져 없어진 직검이다.", 200, ItemType.Weapon, 5));
            shop.AddItem(new Weapon("의식검", "의식에 사용되는 초승달 모양의 검이다.", 500, ItemType.Weapon, 7));
            shop.AddItem(new Weapon("커다란 대검", "양손으로도 휘두르기 힘든 대검이다. -부웅쾅부우웅쾅부우웅콰앙-", 1000, ItemType.Weapon, 10));
            shop.AddItem(new Armor("철제 투구", "양 옆으로 박힌 뿔이 특징인 투구다. 이걸 쓴다고 용언을 사용할 수는 없다.", 200, ItemType.Armor, 7));
            shop.AddItem(new Armor("하일리아의 방패", "하이랄의 기사들에게 주어지는 방패다. 명심하자 이 방패로도 닭은 못막는다.", 500, ItemType.Armor, 10));

            dungeons = new List<Dungeon>();
            dungeons.Add(new Dungeon("쉬운", 5, 1000));
            dungeons.Add(new Dungeon("일반", 11, 1700));
            dungeons.Add(new Dungeon("어려운", 17, 2500));
        }
    }

    abstract class Unit
    {

        protected string name;
        protected int level;
        protected int hp;
        protected int attack;
        protected int defence;


        public string Name { get { return name; } }
        public int Level { get { return level; } }
        public int Hp { get { return hp; } set { hp = value; } }
        public int Attack { get { return attack; } }
        public int Defence { get { return defence; } }



        public Unit(string name, int level, int hp, int attack, int defence)
        {
            this.name = name;
            this.level = level;
            this.hp = hp;
            this.attack = attack;
            this.defence = defence;
        }

        public void AttackUnit(Unit unit)
        {
            if (unit.Hp > 0)
            {
                unit.OnDamaged(attack);
            }
        }

        public void OnDamaged(int damage)
        {
            hp -= (damage - defence);
        }
    }

    class Player : Unit
    {
        ChrClass chrClass;
        int gold;

        List<Item> items = new List<Item>();
        //List<Equipment> equips = new List<Equipment>();

        Weapon equipWeapon = null;
        Armor equipArmor = null;

        public ChrClass GetChrClass() { return chrClass; }
        public int Gold { get { return gold; } set { gold = value; } }

        public Player(ChrClass chrClass, string name, int level, int hp, int attack, int defence) : base(name, level, hp, attack, defence)
        {
            this.chrClass = chrClass;
            this.gold = 500;
        }

        // 아이템으로 올라가는 능력치
        public int GetAttackOnlyItems()
        {
            if (equipWeapon != null)
                return equipWeapon.AttackPower;
            else
                return 0;
        }

        public int GetDefenceOnlyItems()
        {
            if(equipArmor != null)
                return equipArmor.DefencePower;
            else
                return 0;
        }

        // 아이템을 포함한 최종 능력치
        public int GetAttackByItems()
        {
            return attack + GetAttackOnlyItems();
        }

        public int GetDefenceByItems()
        {
            return defence + GetDefenceOnlyItems();
        }


        public void EqipItem(Equipment equip)
        {
            if(equip.Type == ItemType.Weapon)
            {
                if(equipWeapon != null)
                {
                    equipWeapon.IsEquip = false;
                }
                equipWeapon = (Weapon)equip;
                equipWeapon.IsEquip = true;
            }
            else if(equip.Type == ItemType.Armor)
            {
                if (equipArmor != null)
                {
                    equipArmor.IsEquip = false;
                }
                equipArmor = (Armor)equip;
                equipArmor.IsEquip = true;
            }
            else
            {
                // 허용되지 않은 상태
                return;
            }
        }

        public List<Item> GetItems() { return items; }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void BuyItem(int itemIndex, Shop shop)
        {
            shop.SellItemTo(itemIndex, this);
        }

        public void SellItem(int itemIndex)
        {
            // 장착 중인 아이템이라면 장착해제
            if (items[itemIndex - 1].Type == ItemType.Weapon)
            {
                Weapon tempWeapon = (Weapon)items[itemIndex - 1];
                if (tempWeapon == equipWeapon)
                {
                    tempWeapon.IsEquip = false;
                    equipWeapon = null;
                }
            }
            else if (items[itemIndex - 1].Type == ItemType.Armor)
            {
                Armor tempArmor = (Armor)items[itemIndex - 1];
                if(tempArmor == equipArmor)
                {
                    tempArmor.IsEquip = false;
                    equipArmor = null;
                }
            }
            // 실제 판매
            Gold += (int)(items[itemIndex - 1].Price * 0.85f);
            items.Remove(items[itemIndex - 1]);
        }
    }

    abstract class Item
    {
        protected string name;
        protected string description;
        protected int price;
        protected ItemType type;

        public Item(string name, string description, int price, ItemType type)
        {
            this.name = name;
            this.description = description;
            this.price = price;
            this.type = type;
        }

        public string Name { get { return name; } }
        public string Descrption { get { return description; } }
        public int Price { get { return price; } }
        public ItemType Type { get { return type; } }

        public abstract void ShowItemInfo();
    }

    abstract class Equipment : Item
    {
        protected bool isEquip;

        public Equipment(string name, string desc, int price, ItemType type) : base(name, desc, price, type)
        {
            isEquip = false;
        }

        public bool IsEquip { get { return isEquip; } set { isEquip = value; } }
    }

    class Weapon : Equipment
    {
        int attackPower;

        public int AttackPower { get { return attackPower; } }

        public Weapon(string name, string desc, int price, ItemType type, int attackPower) : base(name, desc, price, type)
        {
            this.attackPower = attackPower;
        }

        public override void ShowItemInfo()
        {
            //if (isEquip) Console.Write("[E]");
            Console.Write($"{name}\t | 공격력 : +{attackPower}\t | {description}");
        }
    }

    class Armor : Equipment
    {
        int defencePower;

        public int DefencePower { get { return defencePower; } }

        public Armor(string name, string desc, int price, ItemType type, int defencePower) : base(name, desc, price, type)
        {
            this.defencePower = defencePower;
        }

        public override void ShowItemInfo()
        {
            //if (isEquip) Console.Write("[E]");
            Console.Write($"{name}\t | 방어력 : +{defencePower}\t | {description}");
        }
    }

    class Shop
    {
        List<Item> sellingItems =  new List<Item>();
        List<Item> curItems = new List<Item>();

        public void CopyItemList()
        {
            foreach (Item item in sellingItems)
            {
                curItems.Add(item);
            }
        }

        public void ShowItemsList()
        {
            if(curItems == null || sellingItems == null) return;
            foreach(Item item in sellingItems)
            {
                item.ShowItemInfo();
                if (curItems.Contains(item))
                    Console.WriteLine($"\t| {item.Price}G");
                else
                    Console.WriteLine($"\t| 구매 완료");
            }
        }

        public void ShowSellingItems()
        {
            if(curItems == null || sellingItems == null) return;
            for(int i=0;i<sellingItems.Count;i++)
            {
                Console.Write($"- {i+1} ");
                sellingItems[i].ShowItemInfo();
                if (curItems.Contains(sellingItems[i]))
                    Console.WriteLine($"\t| {sellingItems[i].Price}G");
                else
                    Console.WriteLine($"\t| 구매 완료");
            }
        }

        public List<Item> GetSellingItems()
        {
            return sellingItems;
        }

        public List<Item> GetCurItems()
        {
            return curItems;
        }

        public void AddItem(Item item)
        {
            sellingItems.Add(item);
            curItems.Add(item);
        }

        public void SellItemTo(int itemIndex, Player player)
        {
            if (curItems.Contains(sellingItems[itemIndex-1]))
            {
                if (sellingItems[itemIndex-1].Price <= player.Gold)
                {
                    //구매
                    curItems.Remove(sellingItems[itemIndex - 1]);
                    player.Gold -= sellingItems[itemIndex - 1].Price;
                    player.AddItem(sellingItems[itemIndex - 1]);
                }
                else
                {
                    // 돈 부족
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("보유 소지금이 부족합니다.");
                    Console.ResetColor();
                    Console.ReadKey();
                }
            }
            else
            {
                // 이미 구매한 물건
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("이미 구매한 물건입니다.");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }

    class Dungeon
    {
        string difficulty;
        int recDefence;
        int rewardGold;

        public Dungeon(string difficulty, int recDefence, int rewardGold)
        {
            this.difficulty = difficulty;
            this.recDefence = recDefence;
            this.rewardGold = rewardGold;
        }

        public void EnterTheDungeon(Player player)
        {
            Console.Clear();
            Random rand = new Random();
            int oriHp = player.Hp;
            // 권장 방어도보다 낮은 경우, 40%로 실패
            if (player.GetDefenceByItems() <  recDefence)
            {
                int s = rand.Next(0, 10);
                if (s < 4) // 실패
                {
                    player.Hp -= player.Hp/2;
                    Console.ForegroundColor= ConsoleColor.DarkRed;
                    Console.WriteLine("던전 실패");
                    Console.ResetColor();
                    Console.WriteLine($"{difficulty} 던전 클리어에 실패하였습니다.");
                    Console.WriteLine($"\n[탐험 결과]\n체력 : {oriHp} -> {player.Hp}");
                    while (true)
                    {
                        string choice = Console.ReadLine();
                        int choiceNum = 0;
                        try
                        {
                            choiceNum = int.Parse(choice);
                        }
                        catch
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ResetColor();
                            continue;
                        }

                        if (choiceNum == 0)
                        {
                            return;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.ResetColor();
                            continue;
                        }
                    }
                }
            }
            int decHp = rand.Next(20, 36) - (player.GetDefenceByItems() - recDefence);
            int sumGold = rewardGold + (int)(rewardGold * 0.01f * rand.Next(player.GetAttackByItems(), (player.GetAttackByItems() * 2) + 1));

            int oriGold = player.Gold;

            player.Hp -= decHp;
            player.Gold += sumGold;

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("던전 클리어");
            Console.ResetColor();
            Console.WriteLine($"{difficulty} 던전을 클리어 하였습니다.");
            Console.WriteLine($"\n[탐험 결과]\n체력 : {oriHp} -> {player.Hp}\nGold : {oriGold} G -> {player.Gold} G");
            Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            while (true)
            {
                string choice = Console.ReadLine();
                int choiceNum = 0;
                try
                {
                    choiceNum = int.Parse(choice);
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }

                if (choiceNum == 0)
                {
                    return;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.ResetColor();
                    continue;
                }
            }
        }

        public void ShowDungeonInfo()
        {
            Console.WriteLine($"{difficulty} 던전\t| 방어력 {recDefence}이상 권장");
        }
    }

    enum ChrClass
    {
        Warrior = 1,
        Wizard = 2,
        Thief = 3,
        Archer = 4
    }

    enum ItemType
    {
        Weapon,
        Armor
    }
}
