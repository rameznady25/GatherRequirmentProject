using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gather_Requirement_Project.Models.SQLScript
{
    public class Queries
    {


        public string DB(string Name)
        {
            return
@"IF db_id('GatherRequirements1') is null
BEGIN
    CREATE DATABASE ["+ Name + @"]
END
ELSE
BEGIN
    USE [" + Name + @"];
END "  + ";\n";
        }


        public string DropDB(string Name)
        {
            return "DROP DATABASE " + Name + ";";
        }

        public string CreateTable(string NameTable, string[] qur1, string[] qur2)
        {
            string query = "";
            if (NameTable != null)
            {
                query += "CREATE TABLE " + NameTable.Replace(" ", "_") + "(";
                for (int i = 0; i < qur1.Length && qur1 != null && qur2 != null; i++)
                {

                    query += qur1[i].Replace(" ", "_") + " " + qur2[i];

                    if (qur1.Length != i + 1 )
                        query += ", ";

                }

                query += ");\n";

            }


            return query;
        }





    }
}
