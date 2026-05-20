using SQLite;
using System;
using UnityEngine;

public class GameData
{
    [Table("User")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int ID { get; set; }

        [NotNull]
        [Column("Nickname")]
        public string Nickname { get; set; }
    }

    [Table("Statistics")]
    public class Statistics
    {
        [PrimaryKey, AutoIncrement]
        [Column("ID")]
        public int ID { get; set; }

        [NotNull]
        [Column("IDUser")]
        public int IDUser { get; set; }

        [NotNull]
        [Column("CountCast")]
        public int CountCast { get; set; }

        [NotNull]
        [Column("Score")]
        public int Score { get; set; }

        [NotNull]
        [Column("Date")]
        public String Date { get; set; }
    }
}
