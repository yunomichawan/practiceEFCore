using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbCommon.Database.Models
{
    /// <summary>
    /// 売上高（日別）
    /// </summary>
    [Table("T_SALES_DAILY")]
    public class TDailySales
    {
        #region Column

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// 店舗ID
        /// </summary>
        [Column("SHOP_ID")]
        public int ShopId { get; set; }

        /// <summary>
        /// 売上日（yyyy/MM/dd）
        /// </summary>
        [Column("SALES_DATE")]
        public DateTime SalesDate { get; set; }

        /// <summary>
        /// 売上高
        /// </summary>
        [Column("AMOUNT_OF_SALES")]
        public decimal AmountOfSales { get; set; }

        #endregion

        /// <summary>
        /// 店舗情報
        /// </summary>
        public MShop Shop { get; set; }
    }
}
