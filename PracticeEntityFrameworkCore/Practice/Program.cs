using DbCommon.Database.Context;
using DbCommon.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramTest programTest = new ProgramTest();

            // スキーマ初期化
            programTest.InitSchema();

            // テストデータ登録
            programTest.AddTestData();

            // テストデータ削除
            programTest.RemoveTestData();

        }
    }

    /// <summary>
    /// プログラム検証用
    /// </summary>
    public class ProgramTest
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ProgramTest()
        {
        
        }

        /// <summary>
        /// スキーマ初期化
        /// </summary>
        public void InitSchema()
        {
            using(SampleEntities sampleEntities = new SampleEntities())
            {
                sampleEntities.Database.EnsureDeleted();
                sampleEntities.Database.EnsureCreated();
            }
        }

        /// <summary>
        /// テスト用データ登録
        /// </summary>
        public void AddTestData()
        {
            // エリア情報作成
            MArea area1 = new MArea { AreaName = "北海道" };
            MArea area2 = new MArea { AreaName = "東北" };
            MArea area3 = new MArea { AreaName = "関東" };
            MArea area4 = new MArea { AreaName = "中部" };
            MArea area5 = new MArea { AreaName = "近畿" };
            MArea area6 = new MArea { AreaName = "中国" };
            MArea area7 = new MArea { AreaName = "四国" };
            MArea area8 = new MArea { AreaName = "九州" };

            // 店舗情報作成
            MShop shopTokyo = new MShop { Area = area3 ,Address = "東京都xxxxxxxxxx", ShopName = "東京店舗" };
            MShop shopHokkaido = new MShop { Area = area1, Address = "北海道xxxxxxxxxx", ShopName = "北海道店舗" };

            // 売上情報追加：東京
            shopTokyo.SalesDailies = new List<TDailySales>();
            shopTokyo.SalesDailies.Add(new TDailySales { Shop = shopTokyo, SalesDate = DateTime.Parse("2019/12/1"), AmountOfSales = 10000 });
            shopTokyo.SalesDailies.Add(new TDailySales { Shop = shopTokyo, SalesDate = DateTime.Parse("2019/12/2"), AmountOfSales = 20000 });
            shopTokyo.SalesDailies.Add(new TDailySales { Shop = shopTokyo, SalesDate = DateTime.Parse("2019/12/3"), AmountOfSales = 30000 });

            shopTokyo.SalesMonthlies = new List<TMonthlySales>();
            shopTokyo.SalesMonthlies.Add(new TMonthlySales { Shop = shopTokyo, SalesMonth = "201912",  AmountOfSales = 60000 });

            // 売上情報追加：北海道
            shopHokkaido.SalesDailies = new List<TDailySales>();
            shopHokkaido.SalesDailies.Add(new TDailySales { Shop = shopHokkaido, SalesDate = DateTime.Parse("2019/12/1"), AmountOfSales = 1000 });
            shopHokkaido.SalesDailies.Add(new TDailySales { Shop = shopHokkaido, SalesDate = DateTime.Parse("2019/12/2"), AmountOfSales = 2000 });
            shopHokkaido.SalesDailies.Add(new TDailySales { Shop = shopHokkaido, SalesDate = DateTime.Parse("2019/12/3"), AmountOfSales = 3000 });

            shopHokkaido.SalesMonthlies = new List<TMonthlySales>();
            shopHokkaido.SalesMonthlies.Add(new TMonthlySales { SalesMonth = "201912", Shop = shopHokkaido, AmountOfSales = 6000 });

            using (SampleEntities sampleEntities = new SampleEntities())
            {
                // データの追加
                // この時点ではデータの保存はされておらず、AIの採番が行われておりません。
                sampleEntities.Areas.AddRange(new MArea[] { area1, area2, area3, area4, area5, area6, area7, area8 });
                sampleEntities.Shops.Add(shopTokyo);
                sampleEntities.Shops.Add(shopHokkaido);

                // 登録前：内容出力
                // AIの採番が行われていないことが確認できます。
                Console.WriteLine(Environment.NewLine);
                this.OutputObject(shopTokyo);
                this.OutputObject(shopHokkaido);

                // 登録内容保存
                sampleEntities.SaveChanges();
            }
            // 登録後：内容出力
            // AIの採番が行われたことが確認できます。
            this.OutputObject(shopTokyo);
            this.OutputObject(shopHokkaido);

        }
        
        /// <summary>
        /// テストデータ削除
        /// </summary>
        public void RemoveTestData()
        {
            using (SampleEntities sampleEntities = new SampleEntities())
            {
                Console.WriteLine(Environment.NewLine);
                Console.WriteLine("データの削除");
                
                // エリア、店舗、売上情報取得
                MArea area = sampleEntities.Areas
                    .Include(a => a.Shops)
                        .ThenInclude(shop => shop.SalesDailies)
                    .Include(a => a.Shops)
                        .ThenInclude(shop => shop.SalesMonthlies)
                        .FirstOrDefault();

                // 削除データの確認
                this.OutputObject(area);

                // DbSet経由の削除
                sampleEntities.Remove(area);

                // 削除内容の保存
                sampleEntities.SaveChanges();
            }

        }

        /// <summary>
        /// オブジェクト出力
        /// </summary>
        public void OutputObject(object obj)
        {
            List<string> outputs = new List<string>();
            Type type = obj.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                object value = propertyInfo.GetGetMethod().Invoke(obj, null);
                // List型の場合、中身を出力
                if (value is IList list)
                {
                    foreach (object item in list)
                    {
                        this.OutputObject(item);
                        Console.WriteLine(Environment.NewLine);
                    }
                }
                else
                {
                    outputs.Add(propertyInfo.Name + ":" + value);
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, outputs));
        }
    }
}
