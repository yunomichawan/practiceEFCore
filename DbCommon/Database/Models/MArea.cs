using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbCommon.Database.Models
{
    /// <summary>
    /// エリアマスタ
    /// </summary>
    [Table("M_AREA")]
    public class MArea
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// エリア名
        /// </summary>
        [Column("AREA_NAME")]
        public string AreaName { get; set; }

        /// <summary>
        /// 店舗情報
        /// </summary>
        public List<MShop> Shops { get; set; }
    }
}
