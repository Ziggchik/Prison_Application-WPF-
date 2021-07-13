using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WpfApp1
{
    class DBProcedures
    {
        private SqlCommand command
           = new SqlCommand("", DBConnection.connection);

        private void commandConfig(string config)
        {
            command.CommandType =
                System.Data.CommandType.StoredProcedure;
            command.CommandText = "[dbo].[" + config + "]";
            command.Parameters.Clear();
        }


        public void spPrisoners_insert(string Surname_Prisoner,
            string Name_Prisoner, string MiddleName_Prisoner, Int32 Guardians_ID, Int32 Prison_Block_ID)
        {
            commandConfig("Prisoners_insert");
            command.Parameters.AddWithValue("@Name_Prisoner",
                Name_Prisoner);
            command.Parameters.AddWithValue("@MiddleName_Prisoner",
                MiddleName_Prisoner);
            command.Parameters.AddWithValue("@Surname_Prisoner",
                Surname_Prisoner);
            command.Parameters.AddWithValue("@Guardians_ID",
              Guardians_ID);
            command.Parameters.AddWithValue("@Prison_Block_ID",
               Prison_Block_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spPrisoners_update(string Surname_Prisoner,
            string Name_Prisoner, string MiddleName_Prisoner,
            Int32 ID_Prisoner, Int32 Guardians_ID, Int32 Prison_Block_ID)
        {
            commandConfig("Prisoners_update");
            command.Parameters.AddWithValue("@ID_Prisoner", ID_Prisoner);
            command.Parameters.AddWithValue("@Name_Prisoner", Name_Prisoner);
            command.Parameters.AddWithValue("@MiddleName_Prisoner", MiddleName_Prisoner);
            command.Parameters.AddWithValue("@Surname_Prisoner", Surname_Prisoner);
            command.Parameters.AddWithValue("@Guardians_ID", Guardians_ID);
            command.Parameters.AddWithValue("@Prison_Block_ID", Prison_Block_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spPrisoners_delete(Int32 ID_Prisoner)
        {
            commandConfig("Prisoners_delete");
            command.Parameters.AddWithValue("@ID_Prisoner", ID_Prisoner);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spGuardians_insert(string Surname_Guardian,
           string Name_Guardian, string MiddleName_Guardian)
        {
            commandConfig("Guardians_insert");
            command.Parameters.AddWithValue("@Name_Guardian",
                Name_Guardian);
            command.Parameters.AddWithValue("@MiddleName_Guardian",
                MiddleName_Guardian);
            command.Parameters.AddWithValue("@Surname_Guardian",
                Surname_Guardian);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void spGuardians_update(string Surname_Guardian,
                  string Name_Guardian, string MiddleName_Guardian,
                  Int32 ID_Guardian)
        {
            commandConfig("Guardians_update");
            command.Parameters.AddWithValue("@ID_Guardian", ID_Guardian);
            command.Parameters.AddWithValue("@Name_Guardian", Name_Guardian);
            command.Parameters.AddWithValue("@MiddleName_Guardian", MiddleName_Guardian);
            command.Parameters.AddWithValue("@Surname_Guardian", Surname_Guardian);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spGuardians_delete(Int32 ID_Guardian)
        {
            commandConfig("Guardians_delete");
            command.Parameters.AddWithValue("@ID_Guardian", ID_Guardian);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spWeapon_insert(string Name_Weapon, Int32 Type_of_Weapon_ID, Int32 Guardians_ID,Int32 Nakladnaya_ID)
        {
            commandConfig("Weapon_insert");
            command.Parameters.AddWithValue("@Name_Weapon",
                Name_Weapon);
            command.Parameters.AddWithValue("@Type_of_Weapon_ID", Type_of_Weapon_ID);
            command.Parameters.AddWithValue("@Guardians_ID", Guardians_ID);
            command.Parameters.AddWithValue("Nakladnaya_ID", Nakladnaya_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spWeapon_update(Int32 ID_Weapon, string Name_Weapon, Int32 Type_of_Weapon_ID, Int32 Guardians_ID, Int32 Nakladnaya_ID)
        {
            commandConfig("Weapon_update");
            command.Parameters.AddWithValue("@ID_Weapon",
               ID_Weapon);
            command.Parameters.AddWithValue("@Name_Weapon",
                Name_Weapon);
            command.Parameters.AddWithValue("@Type_of_Weapon_ID",
              Type_of_Weapon_ID);
            command.Parameters.AddWithValue("@Guardians_ID",
               Guardians_ID);
            command.Parameters.AddWithValue("@Nakladnaya_ID",
              Nakladnaya_ID);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spWeapon_delete(Int32 ID_Weapon)
        {
            commandConfig("Weapon_delete");
            command.Parameters.AddWithValue("@ID_Weapon",
               ID_Weapon);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spType_of_Weapon_insert(string
           Name_of_Type_of_Weapon)
        {
            commandConfig("Type_of_weapon_insert");
            command.Parameters.AddWithValue("@Name_of_Type_of_Weapon",
                Name_of_Type_of_Weapon);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spType_of_Weapon_update(Int32 ID_Type_of_Weapon,
            string Name_of_Type_of_Weapon)
        {
            commandConfig("Type_of_Weapon_update");
            command.Parameters.AddWithValue("@ID_Type_of_Weapon",
                ID_Type_of_Weapon);
            command.Parameters.AddWithValue("@Name_of_Type_of_Weapon",
                Name_of_Type_of_Weapon);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void spType_of_Weapon_delete(Int32 ID_Type_of_Weapon)
        {
            commandConfig("Type_of_Weapon_delete");
            command.Parameters.AddWithValue("@ID_Type_of_Weapon", ID_Type_of_Weapon);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spNakladnaya_insert(string
          Nomer_Nakladnaya)
        {
            commandConfig("Nakladnaya_insert");
            command.Parameters.AddWithValue("@Nomer_Nakladnaya",
              Nomer_Nakladnaya);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spNakladnaya_update(Int32 ID_Nakladnaya,
            string Nomer_Nakladnaya)
        {
            commandConfig("Nakladnaya_update");
            command.Parameters.AddWithValue("@ID_Nakladnaya",
               ID_Nakladnaya);
            command.Parameters.AddWithValue("@Nomer_Nakladnaya",
               Nomer_Nakladnaya);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spNakladnaya_delete(Int32 ID_Nakladnaya)
        {
            commandConfig("Nakladnaya_delete");
            command.Parameters.AddWithValue("@ID_Nakladnaya", ID_Nakladnaya);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
        public void spPrison_Block_insert(string
        Name_of_block)
        {
            commandConfig("Prison_Block_insert");
            command.Parameters.AddWithValue("@Name_of_block",
              Name_of_block);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spPrison_Block_update(Int32 ID_Prison_Block,
            string Name_of_block)
        {
            commandConfig("Prison_Block_update");
            command.Parameters.AddWithValue("@ID_Prison_Block",
              ID_Prison_Block);
            command.Parameters.AddWithValue("@Name_of_block",
               Name_of_block);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }

        public void spPrison_Block_delete(Int32 ID_Prison_Block)
        {
            commandConfig("Prison_Block_delete");
            command.Parameters.AddWithValue("@ID_Prison_Block", ID_Prison_Block);
            DBConnection.connection.Open();
            command.ExecuteNonQuery();
            DBConnection.connection.Close();
        }
    }
}
