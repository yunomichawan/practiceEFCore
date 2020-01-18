using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbCommon.Database.Models
{
    /// <summary>
    /// 売上高（月別）
    /// </summary>
    [Table("T_SALES_MONTHLY")]
    public class TMonthlySales
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
        /// 売上月（yyyyMM）
        /// </summary>
        [Column("SALES_MONTH")]
        [StringLength(6)]
        public string SalesMonth { get; set; }

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
