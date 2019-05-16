using EVaccAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVaccAPI.Services
{
    public class FieldStaffService
    {
        DbService dbService;
        public FieldStaffService()
        {
            dbService = DbService.GetDbService();
        }

        public bool AddFieldStaffComment(CommentRequest infData)
        {
            try
            {
                var query = string.Format(@"INSERT INTO StaffNote (FieldStaffID,Date,Comments,InfantID)
                                            VALUES({0},'{1}','{2}',{3})", infData.Userid,DateTime.Now,infData.Comment,infData.infantId);
                dbService.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteFieldStaffComment(int userid)
        {
            try
            {
                var query = string.Format(@"DELETE FROM StaffNote where FieldStaffID = {0}", userid);
                dbService.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}