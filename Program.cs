using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace TextRPG_KangEonDeok
{
    internal class Program
    {
        public void Process()
        {
            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
            }
        }

        public void ProcessStoreSell()
        {
            Console.WriteLine("아이템 판매를 선택하셨습니다.");
            Console.WriteLine("판매할 아이템 번호를 입력하세요. ([0] 돌아가기)");

            for (int i = 0; i < Inventory.Count; i++)

                do
                {
                    if (int.TryParse(Console.ReadLine(), out int output) && output >= 0 && output <= StoreItem.Count)
                    {
                        if (output == 0)
                        {
                            ProcessStore();
                            break;
                        }
                        else
                        {
                            Console.WriteLine($"{Inventory[i].ItemName}를 판매하셨습니다.");
                            StoreItem.Add(ax);
                            Inventory.Remove(ax);
                            player.Money -= (StoreItem[i].Price);
                            ProcessTown();
                            //SellItem();
                        }
                    }
                    Console.WriteLine("올바른 값을 입력해주세요.");

                } while (true);
        }
        public void ProcessStoreBuy()
        {
            Console.WriteLine("아이템 구매를 선택하셨습니다.");
            Console.WriteLine("구매할 아이템 번호를 입력하세요. ([0] 돌아가기)");

            for (int i = 0; i < StoreItem.Count; i++)

                do
                {
                    if (int.TryParse(Console.ReadLine(), out int output) && output >= 0 && output <= StoreItem.Count)
                    {
                        if (output == 0)
                        {
                            ProcessStore();
                            break;
                        }
                        //StoreItem
                        //Inventory
                        else
                        {
                            Console.WriteLine($"{StoreItem[i].ItemName}를 구매하셨습니다.");
                            Inventory.Add(StoreItem[i]);
                            StoreItem.Remove(StoreItem[i]);
                            player.Money = 0;//(StoreItem[i].Price);
                            ProcessTown();
                            //BuyItem();

                        }
                    }
                    Console.WriteLine("올바른 값을 입력해주세요.");

                } while (true);
        }
        public void ProcessStore()
        {
            CreateItem_PutInStore();

            Console.WriteLine($"[아이템 목록]");
            for (int i = 0; i < StoreItem.Count; i++)
            {
                Console.WriteLine("[보유 골드]");
                Console.WriteLine($"{player.Money} G ");

                Console.WriteLine($" {i + 1}. {StoreItem[i].ItemName} | 공격력 +{StoreItem[i].Attack} | 낡은 도끼 | {StoreItem[i].Price} G");
            }
            Console.WriteLine();
            Console.WriteLine("    [1]. 아이템 구매");
            Console.WriteLine("    [2]. 아이템 판매");
            Console.WriteLine("    [0]. 나가기");
            do
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int output))
                {
                    switch (output)
                    {
                        case 1:
                            ProcessStoreBuy();
                            break;
                        case 2:
                            ProcessStoreSell();
                            break;
                        case 0:
                            ProcessTown();
                            break;
                    }
                }
                Console.WriteLine("올바른 값을 입력해주세요.");

            } while (true);

        }
        public void ProcessInventory()
        {
            if (Inventory.Count == 0)
            {
                Console.WriteLine("착용 가능한 아이템이 없습니다.");
            }
            else
            {
                for (int i = 0; i < Inventory.Count; i++)
                {
                    Console.WriteLine($" {i + 1}.  {Inventory[i]}");
                }

                while (true)
                {
                    Console.WriteLine("착용할 아이템 번호를 입력하세요. ([0] 돌아가기)");


                    string input = Console.ReadLine();
                    switch (input)
                    {
                        //case "1":
                        //    item = ItemType.Weapon0;
                        //    break;
                        //case "2":
                        //    item = ItemType.Weapon1;
                        //    break;
                        case "0":
                            ProcessTown();
                            break;

                    }
                }
            }

            Console.WriteLine("    [0]. 나가기");
            do
            {
                string input = Console.ReadLine();
                if (int.TryParse(input, out int output))
                {
                    switch (output)
                    {
                        case 0:
                            Console.WriteLine(3);
                            ProcessTown();
                            break;
                    }
                }
                Console.WriteLine("올바른 값을 입력해주세요.");

            } while (true);
        }
        public void ProcessStatus()
        {
            string input;
            Console.WriteLine($"Lv   : {player.Lv}");
            Console.WriteLine($"Name : {player.Name} ( Player )");
            Console.WriteLine($"Atk  : {player.Attack}");
            Console.WriteLine($"Def  : {player.Armor}");
            Console.WriteLine($"Hp   : {player.Hp}");
            Console.WriteLine($"Gold : {player.Money}G");

            do
            {
                Console.WriteLine("[0]. 나가기");
                input = Console.ReadLine();
            } while (input != "0");

            if (input == "0")
                ProcessTown();

        }
        public void ProcessTown()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
            Console.WriteLine("[1] 상태보기");
            Console.WriteLine("[2] 인벤토리");
            Console.WriteLine("[3] 상점");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ProcessStatus();
                    break;
                case "2":
                    ProcessInventory();
                    break;
                case "3":
                    ProcessStore();
                    break;
            }
        }
        public void ProcessLobby()
        {
            Console.WriteLine("SpartaRpg에 오신 것을 환영합니다...");
            Console.WriteLine("우선 당신의 이름을 알려주세요.");
            string name = Console.ReadLine();
            player.SetName(name);
            player.SetInfo(1, 100, 10, 5); // 직업 추가 시, 여긴 SetInfo 대신 SelectJob
            ProcessTown();



        }

        public enum GameMode
        {
            none,
            Lobby,
            Town,
            Status,
            Inventory,
            InventoryEquip,
            Store,
            StoreBuy,
            StoreSell,
            Dungeon
        }

        class Item
        {
            public string ItemName { get; set; } = "Ax";
            public int Attack { get; set; } = 0;
            public int Armor { get; set; } = 0;
            public int Price { get; set; } = 0;
            public int Sell { get; set; } = 0;

            public string ItemDescription { get; set; } = "None";


            public void ItemInfo(int attack, int armor, int price, int sell)
            {
                this.Attack = attack;
                this.Armor = armor;
                this.Price = price;
                this.Sell = sell;
            }

        }


        public class PlayerStatus
        {
            public string Name { get; set; } = "None";
            public int Lv { get; set; } = 0;
            public int Hp { get; set; } = 0;
            public int Attack { get; set; } = 0;
            public int Armor { get; set; } = 0;
            public int Money { get; set; } = 1500;

            public void SetName(string name)
            {
                this.Name = name;
            }
            public void SetInfo(int lv, int hp, int attack, int armor) // 직업 추가 시, abstract, override 사용 고려
            {
                this.Lv = lv;
                this.Hp = hp;
                this.Attack = attack;
                this.Armor = armor;
            }

        }
        class Player : PlayerStatus // 스킬보단, 공용기능 추가. 직업 추가 시, 세부 사항 조정
        {
            public int GetHp() { return Hp; }

            public int GetAttack() { return Attack; }

            public int GetArmor() { return Armor; }

            public int GetMoney() { return Money; }

            public bool IsDead() { return Hp <= 0; }

            public void OnDamaged(int damage, int armor)
            {
                Hp -= damage - armor;
                if (Hp < 0)
                    Hp = 0;

                if (damage < armor)
                    damage = armor;
            }

            List<int> Inventory;
            List<int> StoreItem;

            public void Buy(int item)
            {
                Inventory.Add(item);

            }

            public void Sell(int item)
            {
                Inventory.Remove(item);
            }

            public void Equip()
            {

            }

        }

        private GameMode mode = GameMode.Lobby;
        private Player player = new Player();

        List<Item> Inventory = new List<Item>();
        List<Item> StoreItem = new List<Item>();
        private Item ax = new Item();

        public void CreateItem_PutInStore()
        {
            ax.ItemInfo(5, 0, 1500, 1300);
            StoreItem.Add(ax);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Program game = new Program();
                game.Process();
            }
        }
    }

}