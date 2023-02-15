using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;

namespace SmartPortal.SEMS
{
    public class ATM
    {
        #region Search product by condition
        public DataSet GetATMPlaceByCondition(string atmID, string cityCode, string distCode, string address, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000110");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Tìm kiếm địa điểm đặt máy ATM");
                //hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ATMID, atmID);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, cityCode);
                hasInput.Add(SmartPortal.Constant.IPC.DISTCODE, distCode);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);

                DataSet ds = new DataSet();

                if (hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString() == "0")
                {
                    ds = (DataSet)hasOutput[SmartPortal.Constant.IPC.DATASET];
                    errorCode = "0";
                }
                else
                {
                    errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                    errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Delete ATM
        public void DeleteATM(string ATMCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000111");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Xóa địa điểm đặt máy ATM");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ATMID, ATMCode);

                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);                          
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ADD ATM
        public void AddATM(string atmID, string cityCode, string distCode, string address,string PosX,string PosY, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000112");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Thêm địa điểm đặt máy ATM");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ATMID, atmID);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, cityCode);
                hasInput.Add(SmartPortal.Constant.IPC.DISTCODE, distCode);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONX, PosX);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONY, PosY);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region EDIT ATM
        public void EditATM(string atmID, string cityCode, string distCode, string address,string posX,string posY, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "IB000114");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Chỉnh sửa thông tin địa điểm đặt máy ATM");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.ATMID, atmID);
                hasInput.Add(SmartPortal.Constant.IPC.CITYCODE, cityCode);
                hasInput.Add(SmartPortal.Constant.IPC.DISTCODE, distCode);
                hasInput.Add(SmartPortal.Constant.IPC.ADDRESS, address);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONX, posX);
                hasInput.Add(SmartPortal.Constant.IPC.POSITIONY, posY);


                hasOutput = SmartPortal.RemotingServices.AT.DBTRAN().ProcessTransHAS(hasInput);
                errorCode = hasOutput[SmartPortal.Constant.IPC.IPCERRORCODE].ToString();
                errorDesc = hasOutput[SmartPortal.Constant.IPC.IPCERRORDESC].ToString();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
