using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WpfApp1
{
    class DBConnection
    {
        public static SqlConnection connection = new SqlConnection(
                "Data Source = DESKTOP-TEG5PUS\\SEX_MACHINE; " +
                " Initial Catalog = Prison; Persist Security Info = True;" +
                " User ID = sa; Password = \"s453153s\"");
        public DataTable dtPrisoner = new DataTable("Prisoner");
        public DataTable dtBlock = new DataTable("Block");
        public DataTable dtType_of_Weapon = new DataTable("Type_of_Weapon");
        public DataTable dtWeapon = new DataTable("Weapon");
        public DataTable dtGuardian = new DataTable("Guardian");
        public DataTable dtNakladnaya = new DataTable("Nakladnaya");
        public static string qrPrisoner = "SELECT [ID_Prisoner],[Surname_Prisoner] as \"Фамилия\"," +
           " [Name_Prisoner] as \"Имя\",[MiddleName_Prisoner] as \"Отчество\",[Prison_Block_ID],[ID_Prison_Block],[Name_of_block] as \"Название блока\",[ID_Guardian],[Guardians_ID],[Surname_Guardian] + ' ' +[Name_Guardian] + ' ' +[MiddleName_Guardian] as \"Информация об охраннике\" FROM" +
           " [dbo].[Prisoners] INNER JOIN [dbo].[Prison_Block]" +
           " ON [dbo].[Prisoners].[Prison_Block_ID]" +
           " =[dbo].[Prison_Block].[ID_Prison_Block]" +
            "INNER JOIN[dbo].[Guardians]" +
            "ON [dbo].[Prisoners].[Guardians_ID]" +
            "=[dbo].[Guardians].[ID_Guardian]",
        qrGuardian = "SELECT [ID_Guardian],[Surname_Guardian] as \"Фамилия\"," +
           " [Name_Guardian] as \"Имя\",[MiddleName_Guardian] as \"Отчество\",[Surname_Guardian] + ' ' +[Name_Guardian] + ' ' +[MiddleName_Guardian] as \"Информация об охраннике\" FROM" +
           " [dbo].[Guardians]",
        qrType_Of_Weapon = "SELECT[ID_Type_of_Weapon]," +
            "[Name_of_Type_of_Weapon] FROM [dbo].[Type_of_Weapon]",
        qrWeapon = "SELECT [ID_Weapon] ,[Name_Weapon] as \"Название\" ,[Type_of_Weapon_ID] as \"код типа оружия\"," +
            "[ID_Type_of_Weapon] as \"код оружия\",[Name_of_Type_of_Weapon] \"Тип оружия\",[Nakladnaya_ID],[ID_Nakladnaya],[Nomer_Nakladnaya] as \"Накладная\",[ID_Guardian],[Guardians_ID],[Surname_Guardian] + ' ' +[Name_Guardian] + ' ' +[MiddleName_Guardian] as \"Информация об охраннике\" FROM " +
            "[dbo].[Weapon] INNER JOIN [dbo].[Type_of_Weapon]" +
            "ON [dbo].[Weapon].[Type_of_Weapon_ID] " +
            "= [dbo].[Type_of_Weapon].[ID_Type_of_Weapon]" +
            "INNER JOIN[dbo].[Nakladnaya]" +
            "ON [dbo].[Weapon].[Nakladnaya_ID]" +
            "=[dbo].[Nakladnaya].[ID_Nakladnaya]" +
            "INNER JOIN[dbo].[Guardians]" +
            "ON [dbo].[Weapon].[Guardians_ID]" +
            "=[dbo].[Guardians].[ID_Guardian]",
        qrBlock = "SELECT[ID_Prison_Block]," +
            "[Name_of_block] FROM [dbo].[Prison_Block]",
        qrNakladnaya = "SELECT[ID_Nakladnaya]," +
            "[Nomer_Nakladnaya] FROM [dbo].[Nakladnaya]";
        private SqlCommand command = new SqlCommand("", connection);
        public static Int32 IDrecord, IDprisoner, IDguardian;
        private void dtFill(DataTable table, string query)
        {
            command.CommandText = query;
            connection.Open();
            table.Load(command.ExecuteReader());
            connection.Close();
        }

        public void Type_of_Weapon_Fill()
        {
            dtFill(dtType_of_Weapon, qrType_Of_Weapon);
        }

        public void Nakladnaya_Fill()
        {
            dtFill(dtNakladnaya, qrNakladnaya);
        }
        public void PrisonerFill()
        {
            dtFill(dtPrisoner, qrPrisoner);
        }

        public void GuardianFill()
        {
            dtFill(dtGuardian, qrGuardian);
        }

        public void WeaponFill()
        {
            dtFill(dtWeapon, qrWeapon);
        }

        public void BlockFill()
        {
            dtFill(dtBlock, qrBlock);
        }

        public string guardInfo(Int32 ID_record)
        {
            string guard = "";
            command.CommandText = "select '. Охранник '" +
                "+[Surname_Guardian]+' '+substring([Name_Guardian],1,1)" +
                "+'. '+substring([MiddleName_Guardian],1,1)+'.' " +
                "from [dbo].[Guardians] where [ID_Guardian]=" + IDguardian;
            connection.Open();
            guard = command.ExecuteScalar().ToString();
            connection.Close();
            return guard;
        }

        public string prisonerInfo(Int32 ID_record)
        {
            string prisoner = "";
            command.CommandText = "select '. Охранник '" +
                "+[Surname_Prisoner]+' '+substring([Name_Prisoner],1,1)" +
                "+'. '+substring([MiddleName_Prisoner],1,1)+'.' " +
                "from [dbo].[Prisoner] where [ID_Prisoner]=" + IDprisoner;
            connection.Open();
            prisoner = command.ExecuteScalar().ToString();
            connection.Close();
            return prisoner;
        }
    }
}
