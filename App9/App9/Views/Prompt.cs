using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
namespace App9.Views
{
   [Table("Prompt")]
    public class Prompt
            
    {
        
        private int id;
        private String fio;
        private String organization;
        private String direction;
        private String workName;
        private  String agreed;
        private String linkFile;
        private double x;
        private double y;
        private String titleSatus;


        [PrimaryKey, AutoIncrement, Column("_id")]
     
        public int Id { get => id; set => id = value; }
        public string Fio { get => fio; set => fio = value; }
        public string Organization { get => organization; set => organization = value; }
        public string Direction { get => direction; set => direction = value; }
        public string WorkName { get => workName; set => workName = value; }
        public string Agreed { get => agreed; set => agreed = value; }
        public string LinkFile { get => linkFile; set => linkFile = value; }
        public double X { get => x; set => x = value; }
        public double Y { get => y; set => y = value; }
        public string TitleSatus { get => titleSatus; set => titleSatus = value; }
    }
}
