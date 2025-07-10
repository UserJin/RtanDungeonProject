using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtanDungeonProject
{
    /*
     * 구현 필수 사항
     * 1) 게임 시작 화면
     * 2) 상태 보기
     * 3) 인벤토리
     * 4) 장착 관리
     * 5) 상점
     * 6) 아이템 구매
     */

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


        public void GameStart()
        {
            // 저장된 데이터 확인?
            // 캐릭터 설정?
            MakeCharacter();
            ShowMainmenu();
        }

        void MakeCharacter()
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
        }

        void ShowMainmenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("르탄 마을에 오신것을 환영합니다.\n");
                Console.WriteLine("1) 상태 보기\n2) 인벤토리\n3) 상점\n4) 던전 가기\n5) 종료하기");
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
                            break;
                        case 3:
                            // 상점 열기
                            break;
                        case 4:
                            // 던전 입장
                            break;
                        case 5:
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

        void ShowStatus()
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

        void ShowInventory()
        {

        }

        void OpenShop()
        {

        }

        void EnterDungeon()
        {

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
        public int Hp { get { return hp; } }
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
        List<Equipment> equips = new List<Equipment>();

        public ChrClass GetChrClass() { return chrClass; }
        public int Gold { get { return gold; } set { gold = value; } }

        public Player(ChrClass chrClass, string name, int level, int hp, int attack, int defence) : base(name, level, hp, attack, defence)
        {
            this.chrClass = chrClass;
            this.gold = 0;
        }

        // 아이템으로 올라가는 능력치
        public int GetAttackOnlyItems()
        {
            int attackPower = 0;

            foreach (var item in equips)
            {
                if (item.Type == ItemType.Weapon)
                {
                    Weapon weapon = (Weapon)item;
                    attackPower += weapon.AttackPower;
                }
            }

            return attackPower;
        }

        public int GetDefenceOnlyItems()
        {
            int defencePower = 0;

            foreach (var item in equips)
            {
                if (item.Type == ItemType.Armor)
                {
                    Armor armor = (Armor)item;
                    defencePower += armor.DefencePower;
                }
            }

            return defencePower;
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
            if (equip.IsEquip)
            {
                equips.Remove(equip);
                equip.IsEquip = false;
            }
            else
            {
                equips.Add(equip);
                equip.IsEquip = true;
            }
        }
    }

    abstract class Item
    {
        protected string name;
        protected string description;
        protected int price;
        protected ItemType type;

        public string Name { get { return name; } }
        public string Descrption { get { return description; } }
        public int Price { get { return price; } }
        public ItemType Type { get { return type; } }

        protected abstract void ShowItemInfo();
    }

    abstract class Equipment : Item
    {
        protected bool isEquip;

        public bool IsEquip { get { return isEquip; } set { IsEquip = value; } }
    }

    class Weapon : Equipment
    {
        int attackPower;

        public int AttackPower { get { return attackPower; } }

        protected override void ShowItemInfo()
        {
            if (isEquip) Console.Write("[E]");
            Console.WriteLine($"{name}\t | 방어력 : {attackPower}\t | {description}");
        }
    }

    class Armor : Equipment
    {
        int defencePower;

        public int DefencePower { get { return defencePower; } }

        protected override void ShowItemInfo()
        {
            if (isEquip) Console.Write("[E]");
            Console.WriteLine($"{name}\t | 방어력 : {defencePower}\t | {description}");
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
