namespace ConsoleApplication1
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ZD223_istbm00
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(8)]
        public string tabl_type { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(8)]
        public string tabl_code { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool tabl_sts { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool tabl_sts1 { get; set; }

        [Key]
        [Column(Order = 4)]
        public byte tabl_order { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(10)]
        public string tabl_dname { get; set; }

        [StringLength(40)]
        public string tabl_cname { get; set; }

        [StringLength(50)]
        public string tabl_ename { get; set; }

        [StringLength(8)]
        public string tabl_typekind { get; set; }

        [StringLength(200)]
        public string tabl_desc { get; set; }

        [StringLength(50)]
        public string tabl_stfn { get; set; }

        [StringLength(50)]
        public string tabl_use { get; set; }

        [StringLength(10)]
        public string tabl_mstfn { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? tabl_mdate { get; set; }
    }
}
