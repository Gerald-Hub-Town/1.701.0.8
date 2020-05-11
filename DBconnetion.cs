using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.OracleClient;
using System.Drawing;
using System.Windows.Forms;
using SQLBuilder.Repositories;
using System.Data;

namespace LCRTest
{
    class DBconnetion
    {
        public  OracleRepository repository = new OracleRepository("Data Source = (DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = ksmesh-scan.luxshare.com.cn)(PORT = 1521)) (CONNECT_DATA = (SERVER = DEDICATED) (SERVICE_NAME = ksmesh)  )  );Persist Security Info=True;User ID=lcr;Password=lcr;")
        {
            IsEnableFormat = false,
            SqlIntercept = (sql, parameter) =>
            {
                return null;
            }
        };


        public bool insertqrcode(string ReelID,string lotNo, string PartNo, string DateCode, int Qty, string VendorCode,string remark ,string username,out string  message)
        {
            message = null;
            bool flag = true;
            try
            {
                string cmd = string.Empty;
                cmd = @"SELECT REEL_ID,PART_NO,DATE_CODE,QTY,LOTNO,VENDOR_CODE,PO FROM LCR_BASE_REEL  WHERE REEL_ID=:REELID";

                DataTable dt = repository.FindTable(cmd, new { REELID = ReelID });

                if (dt.Rows.Count >0)//已有数据，存入历史表
                {
                    cmd = "delete  from  lcr.lcr_base_reel where  reel_id=:reelid";
                    repository.ExecuteBySql(cmd, new
                    {
                        reelid = ReelID
                    });

                     
                }
                
                
                 cmd = "insert into lcr.LCR_BASE_REEL(reel_id,part_no,DATE_CODE,QTY,LOTNO,VENDOR_CODE,remark1,createuserid,createdate) values " +
                        "(:reelid,:partno,:datecode,:qty,:lotno,:vendorcode,:lotno1,:userid,:createdate)";

                    repository.ExecuteBySql(cmd, new
                    {
                        reelid = ReelID,
                        partno = PartNo,
                        datecode = DateCode,
                        qty = Qty,
                        lotno = lotNo,
                        lotno1= remark,////由于标签规格不标准，批次存在两个栏位
                        vendorcode = VendorCode,
                        userid = username,
                        createdate = DateTime.Now
                    });

                    message = "REEL ID 注册成功";
                
              
            }
            catch (Exception ex)
            {
                message= ex.Message;
                flag = false;
            }
            return flag;


        }

        /// <summary>
        /// 查询员工权限
        /// </summary>
        /// <param name="Userid"></param>
        /// <returns></returns>
        public bool Getuser(string Userid)
        {
          
           
            try
            {
                string cmd = string.Empty;
                cmd = @" select userid from lcr.lcr_user where userid=:userid  and role='super' and role_level='2'" ;

                DataTable dt = repository.FindTable(cmd, new { userid = Userid });

                if (dt.Rows.Count > 0)//已有数据，存入历史表
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
               
                return false;
            }
            

        }

        /// <summary>
        /// 检查料号是否是LCR物料
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public bool CheckItemiISlcr(string item)
        {
            try
            {
                string cmd = string.Empty;

                cmd = @" SELECT T.PART_NO FROM LCR.LCR_PN T WHERE T.PART_NO=:ITEM  ";

                DataTable dt = repository.FindTable(cmd, new { ITEM = item });

                if (dt.Rows.Count > 0)//已有数据，存入历史表
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                return false;
            }

        }



    }
}
