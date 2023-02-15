using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using System.Collections;
using System.Data.SqlClient;
using SmartPortal.DAL;

namespace SmartPortal.SEMS
{
    public class Card
    {
        public DataSet GetCardListByCustCode(string custCode, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSGETCRECARDLIST");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "GetCardListByCustCode");

                hasInput.Add(SmartPortal.Constant.IPC.CUSTCODE, custCode);


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

        public DataTable GetCardByContractNo(string contractno)
        {
            DataTable ContractExist = new DataTable();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@contractno";
            p1.Value = contractno;
            p1.SqlDbType = SqlDbType.Text;

            ContractExist = DataAccess.GetFromDataTable("SEMS_CONTRACTCARDHOLDER_SELECT", p1);


            return ContractExist;
        }
        public DataTable GetCardRoldeDetailByContractNoandCIF(string contractno, string holdercif)
        {
            DataTable ContractExist = new DataTable();

            SqlParameter p1 = new SqlParameter();
            p1.ParameterName = "@contractno";
            p1.Value = contractno;
            p1.SqlDbType = SqlDbType.Text;

            SqlParameter p2 = new SqlParameter();
            p2.ParameterName = "@HOLDERCFCODE";
            p2.Value = holdercif;
            p2.SqlDbType = SqlDbType.Text;

            ContractExist = DataAccess.GetFromDataTable("SEMS_GETCARDROLEDETAIL", p1,p2);


            return ContractExist;
        }
        public DataSet GetCardRoldeDetailForIU(string contractno, string holdercif, string flag, ref string errorcode, ref string errordesc)
        {
            return new SmartPortal.SEMS.Transactions().DoStored("SEMS_GETCARDROLEDETAIL_FORINSERT", new object[] { contractno, holdercif, flag }, ref errorcode, ref errordesc);
            //DataTable ContractExist = new DataTable();

            //SqlParameter p1 = new SqlParameter();
            //p1.ParameterName = "@CONTRACTNO";
            //p1.Value = contractno;
            //p1.SqlDbType = SqlDbType.Text;

            //SqlParameter p2 = new SqlParameter();
            //p2.ParameterName = "@HOLDERCIF";
            //p2.Value = holdercif;
            //p2.SqlDbType = SqlDbType.Text;

            //SqlParameter p3 = new SqlParameter();
            //p3.ParameterName = "@GETTYPE";
            //p3.Value = holdercif;
            //p3.SqlDbType = SqlDbType.Text;

            //ContractExist = DataAccess.GetFromReader("SEMS_GETCARDROLEDETAIL_FORINSERT", p1, p2, p3);


            //return ContractExist;
        }
        public DataSet InsertCard(string contractNo, string CardHolderCFCode, DataTable tblContractCardHolder, DataTable tblUserCard,DataTable tblUserCardRight, DataTable tblUserCardRightDetail, DataTable tblIbankUserRight,DataTable tblMbankUserRight,DataTable tblCTRight, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCARDINSERT");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Create link card");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.CARDHOLDERCUSTCODE, CardHolderCFCode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);


                #region Insert table ContractCardHolder
                object[] insertContractCardHolder = new object[2];
                insertContractCardHolder[0] = "SEMS_CONTRACT_CARDHOLDER_INSERT";

                insertContractCardHolder[1] = tblContractCardHolder;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTCARDHOLDER, insertContractCardHolder);
                #endregion

                #region Insert table EBA_UserCard
                object[] insertUserCard = new object[2];
                insertUserCard[0] = "SEMS_USER_CARD_INSERT";

                insertUserCard[1] = tblUserCard;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERCARD, insertUserCard);
                #endregion

                #region Insert table EBA_UserCardRigtht
                object[] insertUserCardRight = new object[2];
                insertUserCardRight[0] = "SEMS_USER_CARDRIGHT_INSERT";

                insertUserCardRight[1] = tblUserCardRight;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERCARDRIGHT, insertUserCardRight);
                #endregion

                #region Insert table EBA_UserCardRigthtDetail
                object[] insertUserCardRigthtDetail = new object[2];
                insertUserCardRigthtDetail[0] = "SEMS_USER_CARDRIGHTDETAIL_INSERT";

                insertUserCardRigthtDetail[1] = tblUserCardRightDetail;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTUSERCARDRIGHTDETAIL, insertUserCardRigthtDetail);
                #endregion

                #region Insert table IBS_Userinrole
                object[] insertIBSUserRight = new object[2];
                insertIBSUserRight[0] = "SEMS_IBS_USERINROLE_INSERT";

                insertIBSUserRight[1] = tblIbankUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTIBANKUSERRIGHT, insertIBSUserRight);
                #endregion

                #region Insert table MB_Userinrole
                object[] insertMBSUserRight = new object[2];
                insertMBSUserRight[0] = "SEMS_MB_USERINROLE_INSERT";

                insertMBSUserRight[1] = tblMbankUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTMBUSERRIGHT, insertMBSUserRight);
                #endregion

                #region Insert quyền cho Contract
                object[] insertContractRoleDetail = new object[2];
                insertContractRoleDetail[0] = "SEMS_EBA_CONTRACTROLEDETAIL_INSERT";

                //add vao phan tu thu 2 mang object
                insertContractRoleDetail[1] = tblCTRight;

                hasInput.Add(SmartPortal.Constant.IPC.INSERTCONTRACTROLEDETAIL, insertContractRoleDetail);
                #endregion

                //#region Insert bang TranrightDetail
                ////remove dong rong

                //object[] insertTranrightDetail = new object[2];
                //insertTranrightDetail[0] = "SEMS_EBA_TRANRIGHTDETAIL_INSERT";

                ////add vao phan tu thu 2 mang object
                //insertTranrightDetail[1] = TranrightDetail;

                //hasInput.Add(SmartPortal.Constant.IPC.INSERTTRANRIGHTDETAIL, insertTranrightDetail);
                //#endregion

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


        public DataSet UpdateCard(string contractNo, string CardHolderCFCode, DataTable tblContractCardHolder, DataTable tblUserCard, DataTable tblUserCardRight, DataTable tblUserCardRightDetail, DataTable tblIbankUserRight, DataTable tblMbankUserRight, DataTable tblCTRight, string userID, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCARDUPDATE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "Update link card");
                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.CARDHOLDERCUSTCODE, CardHolderCFCode);
                hasInput.Add(SmartPortal.Constant.IPC.USERID, userID);


                #region update table ContractCardHolder
                object[] insertContractCardHolder = new object[2];
                insertContractCardHolder[0] = "SEMS_CONTRACT_CARDHOLDER_UPDATE";

                insertContractCardHolder[1] = tblContractCardHolder;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATECONTRACTCARDHOLDER, insertContractCardHolder);
                #endregion

                #region delete dữ liệu cũ
                //remove dong rong
                DataTable tblDeleteFirst = new DataTable();
                DataColumn colDelContractNo = new DataColumn("colDelContractNo");
                DataColumn colDelHolderCFCode = new DataColumn("colDelHolderCFCode");
                DataColumn colDelUserID = new DataColumn("colDelUserID");

                tblDeleteFirst.Columns.AddRange(new[] { colDelContractNo, colDelHolderCFCode, colDelUserID });

                DataRow rowDel = tblDeleteFirst.NewRow();
                rowDel["colDelContractNo"] = contractNo;
                rowDel["colDelHolderCFCode"] = CardHolderCFCode;
                rowDel["colDelUserID"] = CardHolderCFCode;

                tblDeleteFirst.Rows.Add(rowDel);

                object[] deleteFirst = new object[2];
                deleteFirst[0] = "SEMS_USER_CARD_UPDATE_DELETEBYID";

                //add vao phan tu thu 2 mang object
                deleteFirst[1] = tblDeleteFirst;

                hasInput.Add(SmartPortal.Constant.IPC.DELETEFIRST, deleteFirst);
                #endregion

                #region Insert table EBA_UserCard
                object[] insertUserCard = new object[2];
                insertUserCard[0] = "SEMS_USER_CARD_UPDATE";

                insertUserCard[1] = tblUserCard;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERCARD, insertUserCard);
                #endregion


                #region Insert table EBA_UserCardRigtht
                object[] insertUserCardRight = new object[2];
                insertUserCardRight[0] = "SEMS_USER_CARDRIGHT_UPDATE";

                insertUserCardRight[1] = tblUserCardRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERCARDRIGHT, insertUserCardRight);
                #endregion

                #region Insert table EBA_UserCardRigthtDetail
                object[] insertUserCardRigthtDetail = new object[2];
                insertUserCardRigthtDetail[0] = "SEMS_USER_CARDRIGHTDETAIL_INSERT";

                insertUserCardRigthtDetail[1] = tblUserCardRightDetail;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEUSERCARDRIGHTDETAIL, insertUserCardRigthtDetail);
                #endregion

                #region Insert table IBS_Userinrole
                object[] insertIBSUserRight = new object[2];
                insertIBSUserRight[0] = "SEMS_EBA_USERINROLE_UPDATE";

                insertIBSUserRight[1] = tblIbankUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEIBANKUSERRIGHT, insertIBSUserRight);
                #endregion

                #region Insert table MB_Userinrole
                object[] insertMBSUserRight = new object[2];
                insertMBSUserRight[0] = "SEMS_MB_USERINROLE_UPDATE";

                insertMBSUserRight[1] = tblMbankUserRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATEMBUSERRIGHT, insertMBSUserRight);
                #endregion




                #region insert quyền cho contract
                object[] insertContractRoleDetail = new object[2];
                insertContractRoleDetail[0] = "SEMS_EBA_CONTRACTROLEDETAIL_UPDATE";

                //add vao phan tu thu 2 mang object
                insertContractRoleDetail[1] = tblCTRight;

                hasInput.Add(SmartPortal.Constant.IPC.UPDATECONTRACTROLEDETAIL, insertContractRoleDetail);
                #endregion

                //#region Insert bang TranrightDetail
                ////remove dong rong

                //object[] insertTranrightDetail = new object[2];
                //insertTranrightDetail[0] = "SEMS_EBA_TRANRIGHTDETAIL_INSERT";

                ////add vao phan tu thu 2 mang object
                //insertTranrightDetail[1] = TranrightDetail;

                //hasInput.Add(SmartPortal.Constant.IPC.INSERTTRANRIGHTDETAIL, insertTranrightDetail);
                //#endregion

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

        public DataSet DeleteCardByContractNoAndHoldlerCIF(string contractNo, string holderCFCode, string status, ref string errorCode, ref string errorDesc)
        {
            try
            {
                //Show hastable result from IPC
                Hashtable hasInput = new Hashtable();
                Hashtable hasOutput = new Hashtable();

                //add key into input
                hasInput.Add(SmartPortal.Constant.IPC.IPCTRANCODE, "SEMSCARDDELETE");
                hasInput.Add(SmartPortal.Constant.IPC.SOURCEID, SmartPortal.Constant.IPC.SOURCEIDVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.SOURCETRANREF, SmartPortal.Constant.IPC.SOURCETRANREFVALUE);
                hasInput.Add(SmartPortal.Constant.IPC.TRANDESC, "DELETE CARD BY CONTRACT NO AND HOLDER CFCODE");
                hasInput.Add(SmartPortal.Constant.IPC.REVERSAL, "N");

                hasInput.Add(SmartPortal.Constant.IPC.CONTRACTNO, contractNo);
                hasInput.Add(SmartPortal.Constant.IPC.CARDHOLDERCUSTCODE, holderCFCode);
                hasInput.Add(SmartPortal.Constant.IPC.STATUS, status);


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
    }
}
