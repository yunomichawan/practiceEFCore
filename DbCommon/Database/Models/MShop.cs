using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbCommon.Database.Models
{
    /// <summary>
    /// 店舗マスタ
    /// </summary>
    [Table("M_SHOP")]
    public class MShop
    {
        #region Column

        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// 店名
        /// </summary>
        [Column("SHOP_NAME")]
        [StringLength(100)]
        public string ShopName { get; set; }

        /// <summary>
        /// 住所
        /// </summary>
        [Required]
        [Column("ADDRESS")]
        [StringLength(500)]
        public string Address { get; set; }

        /// <summary>
        /// エリアID
        /// </summary>
        [Column("AREA_ID")]
        public int AreaId { get; set; }

        #endregion

        /// <summary>
        /// エリア
        /// </summary>
        public MArea Area { get; set; }

        /// <summary>
        /// 売上高（日別）
        /// </summary>
        public List<TDailySales> SalesDailies { get; set; }

        /// <summary>
        /// 売上高（月別）
        /// </summary>
        public List<TMonthlySales> SalesMonthlies { get; set; }
    }
}
