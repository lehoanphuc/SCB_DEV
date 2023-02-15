using System;
using System.Collections.Generic;

using System.Text;
using System.Data;
using SmartPortal.DAL;

namespace SmartPortal.Constant
{
    public static class IPC
    {
        public static class TRANSTATUS
        {
            public const string BEGIN = "0";
            public const string FINISH = "1";
            public const string ERROR = "2";
            public const string WAITING_APPROVE = "3";
            public const string REJECTED = "4";
            public const string PAYMENTFAIL = "9";
        }
        public static class APPROVESTATUS
        {
            public const string BEGIN = "0";
            public const string WAITTINGCUST = "1";
            public const string WAITTINGBANK = "2";
            public const string APPROVED = "3";
            public const string REJECT = "4";
            public const string REJECTED = "5";
            public const string DEPOSIT = "6";
        }
        public static class ERRORCODE
        {
            public const string BE = "1049";
            public const string IPC = "13";
            public const string NCLNS = "1050";
            public const string Quyen = "3979";
            public const string InvalidDestAccount = "3980";
            public const string DuplicateAccount = "3981";
            public const string ErrorTime = "3982";
            public const string AccountRegisted = "3983";
            public const string AccountNotRegisted = "1048";
            public const string BANKERROR = "1051";
            public const string LIMITERROR = "1052";
            public const string INSERTPLERROR = "1053";
            public const string UPDATEPLERROR = "1054";
            public const string FULLINFOERROR = "1055";
            public const string ADDUSERERROR = "1056";
            public const string UPDATEUSERERROR = "1057";
            public const string UNNAMEDTRANSFERTEMPLATE = "3984";
            public const string EXISTLEVEL = "3985";
            public const string OTPINVALID = "24";
            public const string NOTREGOTP = "25";
            public const string NOTREGEBANK = "-2";
            public const string AUTHENTYPEINVALID = "26";
            public const string WATTINGBANKAPPROVE = "9005";
            public const string WATTINGUSERAPPROVE = "9006";
            public const string WATTINGPARTOWNERAPPROVE = "9007";
            public const string TODATEGREATERTHANFROMDATE = "1058";
            public const string THREEMONTH = "1059";
            public const string CHANGEPASSFAILED = "3986";
            public const string DESTACCTNOINVALID = "1060";
            public const string CREATEACCOUNTFAILURE = "3987";
            public const string UNABLEMAPTRANSACTIONTYPE = "3988";
            public const string CITYNOTEXISTS = "3989";
            public const string BANKNOTEXISTS = "3990";
            public const string ERRORADDFEE = "3991";
            public const string NOTSETUPLADDERFEE = "3992";
            public const string EXISTFEEFROPRODUCT = "3993";
            public const string USERNOTREGSAV = "3994";
            public const string USERNOTREGDD = "3995";
            public const string USERNOTREGFD = "3996";
            public const string USERNOTREGLN = "3997";
            public const string EXISTDISTRICT = "3998";
            public const string INVALIDLIMIT = "3999";
            public const string GROUPEXISTS = "4000";
            public const string CUSTNOTHAVEACCTNO = "4001";
            public const string USINGPRODUCT = "4002";
            public const string ACTIVEBRANCH = "4003";
            public const string CUSTNOTEXIST = "4004";
            public const string ACTIVEDISTRICT = "4005";
            public const string EXISTATMID = "4006";
            public const string ACTIVEFEE = "4007";
            public const string ACTIVEUSERLEVEL = "4008";
            public const string MUSTADDGROUP = "4009";
            public const string ACTIVEUSER = "4010";
            public const string MUSTADDLEVEL = "4011";
            public const string OVERLIMIT = "9908";
            public const string SAMENAMETRANSFERTEMPLATE = "4013";
            public const string INVALIDAPPROVELIMIT = "4014";
            public const string INVALIDAPPROVELIMITLEVEL = "1111";
            public const string OTPAUTHENTYPEEXIST = "4016";
            public const string NOTEXISTCONTRACTNO = "4018";
            public const string EXISTCONTRACTFEE = "4019";
            public const string EXISTCONTRACTLIMIT = "4020";
            public const string MUSTSETAPPROVEPROCESS = "4021";
            public const string SELECTFILEUPLOAD = "4022";
            public const string STVQHMDG = "9008";
            public const string STVQHMGDTN = "9009";
            public const string SLGDVQSLGDTN = "9007";
            public const string NOTENOUGHINFO = "12000";
            public const string CMSUSEREXIST = "4027";

            //card
            public const string ADDCARDERROR = "1061";
            public const string UPDATECARDERROR = "1062";

        }

        /// <summary>
        /// IPC common
        /// </summary>
        /// 
        //HongNT add TRECORDCOUNT
        public const string TRECORDCOUNT = "TRECORDCOUNT";
        public const string IPCTRANCODE = "IPCTRANCODE";
        public const string TRANCODETORIGHT = "TRANCODETORIGHT";
        public const string SOURCEID = "SOURCEID";
        public const string DESTID = "DESTID";
        public const string SOURCETRANREF = "SOURCETRANREF";
        public const string TRANDESC = "TRANDESC";
        public const string REVERSAL = "REVERSAL";
        public const string IPCERRORCODE = "IPCERRORCODE";
        public const string IPCERRORDESC = "IPCERRORDESC";
        public const string SOURCEIDVALUE = "SEMS";
        public const string SOURCEIDSMS = "SMS";
        public const string SOURCEIDTOPUP = "TU";
        public const string SOURCEIBVALUE = "IB";
        public const string SOURCETRANREFVALUE = "010";
        public const string CUSTCODE = "CUSTCODE";
        public const string PCO = "PCO";
        public const string RP = "RP";
        public const string DATARESULT = "DATARESULT";
        public const string RVSTRANREF = "RVSTRANREF";

        public const string FLAG = "FLAG";

        public const string AVAILABLEBALANCE = "AVAILABLEBALANCE";

        public const string OK = "0";
        public const string TOKENID = "TOKENID";
        public const string SERIALNUMBER = "SERIALNUMBER";
        public const string TOKENTYPE = "TOKENTYPE";
        public const string DEVICETYPE = "DEVICETYPE";
        public const string ACTIVECODE = "ACTIVECODE";
        public const string ACTIVEDATE = "ACTIVEDATE";

        public const string MAPOTP = "MAPOTP";
        public const string DELETEOTP = "DELETEOTP";

        public const string DATECREATED = "DATECREATED";
        public const string DATEDELETED = "DATEDELETED";
        public const string USERDELETED = "USERDELETED";
        public const string DATEAPPROVED = "DATEAPPROVED";
        public const string DATEAPPROVEDELETED = "DATEAPPROVEDELETED";
        public const string USERAPPROVEDELETED = "USERAPPROVEDELETED";
        public const string USER = "USER";
        public const string STATUSCURRENT = "STATUSCURRENT";

        public const string RESULT = "RESULT";
        public const string ISCUSTOMER = "ISCUSTOMER";
        public const string USERADMIN = "UA";

        //Customer
        public const string CUSTID = "CUSTID";
        public const string FULLNAME = "FULLNAME";
        public const string SHORTNAME = "SHORTNAME";
        public const string DOB = "DOB";
        public const string ADDR_RESIDENT = "ADDR_RESIDENT";
        public const string ADDR_TEMP = "ADDR_TEMP";
        public const string SEX = "SEX";
        public const string NATION = "NATION";
        public const string FAX = "FAX";
        public const string EMAIL = "EMAIL";
        public const string LICENSETYPE = "LICENSETYPE";
        public const string ISSUEDATE = "ISSUEDATE";
        public const string ISSUEPLACE = "ISSUEPLACE";
        public const string RECEIVERADD = "RECEIVERADD";
        public const string RECEIVERID = "RECEIVERID";
        public const string DESCRIPTION = "DESCRIPTION";
        public const string JOB = "JOB";
        public const string OFFICEADDR = "OFFICEADDR";
        public const string OFFICEPHONE = "OFFICEPHONE";
        public const string BRANCHID = "BRANCHID";
        public const string BRID = "BRID";
        public const string TEL = "TEL";
        public const string LICENSEID = "LICENSEID";
        public const string CFTYPE = "CFTYPE";
        public const string STATUS = "STATUS";
        public const string CUSTNAME = "CUSTNAME";
        public const string INSERTCUSTINFO = "INSERTCUSTINFO";
        public const string SNAME = "SNAME";
        public const string USERAPPROVE = "USERAPPROVE";
        public const string USERAPPROVED = "USERAPPROVED";

        public const string LICDATE = "LICDATE";
        public const string LICPLACE = "LICPLACE";
        public const string ORGNATION = "ORGNATION";
        // ATM
        public const string ATMID = "ATMID";
        // DISTRICT
        public const string DISTNAME = "DISTNAME";
        //EXCHANGERATE
        public const string EXCHANGEDATE = "EXCHANGEDATE";
        //Product
        public const string PRODUCT = "PRODUCT";
        public const string PRODUCTID = "PRODUCTID";
        public const string PRODUCTNAME = "PRODUCTNAME";
        public const string CUSTTYPE = "CUSTTYPE";
        public const string DESC = "DESC";
        public const string INSERTPRODUCT = "INSERTPRODUCT";
        public const string UPDATEPRODUCT = "UPDATEPRODUCT";
        public const string DELETEPRODUCTROLE = "DELETEPRODUCTROLE";
        public const string INSERTPRODUCTROLE = "INSERTPRODUCTROLE";
        //Product Limit
        public const string CCYID = "CCYID";
        public const string PASSDATE = "PASSDATE";
        public const string TRANLIMIT = "TRANLIMIT";
        public const string COUNTLIMIT = "COUNTLIMIT";
        public const string TOTALLIMITDAY = "TOTALLIMITDAY";

        public const string UPDATETRANRIGHTDETAIL = "UPDATETRANRIGHTDETAIL";
        public const string UPDATEUSERACCOUNT = "UPDATEUSERACCOUNT";

        public const string UPDATEIBANKUSERRIGHT = "UPDATEIBANKUSERRIGHT";
        public const string UPDATESMSUSERRIGHT = "UPDATESMSUSERRIGHT";
        public const string UPDATEMBUSERRIGHT = "UPDATEMBUSERRIGHT";
        public const string UPDATEPHOUSERRIGHT = "UPDATEPHOUSERRIGHT";

        public const string DELETEFIRST = "DELETEFIRST";

        public const string LETTER = "LETTER";

        public const string BANKCODE = "BANKCODE";
        //role
        public const string SERVICEID = "SERVICEID";
        public const string KEYWORD = "KEYWORD";

        public const string ACCOUNT = "ACCOUNT";
        public const string APPRSTS = "APPRSTS";
        public const string APPROVEDATE = "APPROVEDATE";
        public const string AMOUNT = "AMOUNT";
        public const string TERM = "TERM";
        public const string INSERTUSERAPPTRAN = "INSERTUSERAPPTRAN";
        public const string INSERTUSERAPPTRANDETAIL = "INSERTUSERAPPTRANDETAIL";
        public const string BRANCHRECEIVER = "BRANCHRECEIVER";

        public const string ISDELETED = "ISDELETED";
        public const string ISBATCH = "ISBATCH";

        //service
        public const string IB = "IB";
        public const string SMS = "SMS";
        public const string MB = "MB";
        public const string PHO = "PHO";
        public const string EW = "WL";
        public const string AM = "AM";
        public const string SEMSUSERSEARCH = "SEMSUSERSEARCH";
        public const string SEMSUSERINSERT = "SEMSUSERINSERT";
        public const string TRANCODE = "TRANCODE";
        public const string TRANID = "TRANID";
        public const string FROM = "FROM";
        public const string TO = "TO";
        public const string IBTRANSFEROUTBANK = "IB000206";

        //role
        public const string ROLEID = "ROLEID";
        public const string ROLETYPE = "ROLETYPE";
        public const string SCHEDULENAME = "SCHEDULENAME";

        //common
        public const string DATASET = "DATASET";
        public const string IPCTRANSID = "IPCTRANSID";
        public const string DATATABLE = "DATATABLE";

        public const string SEMS00024 = "SEMS00024";
        public const string SEMS00025 = "SEMS00025";
        public const string EXCESSSWEEPINGTRANCODE = "IB000607";

        //logtrandetail       
        public const string LICENSE = "LICENSE";
        public const string PLACE = "PLACE";

        //Authen
        public const string AUTHENTYPE = "AUTHENTYPE";
        public const string AUTHENCODE = "AUTHENCODE";

        // SMS DAT LICH THONG BAO
        public const string MSGCONTENT = "MSGCONTENT";
        public const string SENDTYPE = "SENDTYPE";
        public const string PHONENOLIST = "PHONENOLIST";

        //status
        public const string NEW = "N";
        public const string DELETE = "D";
        public const string ACTIVE = "A";
        public const string BLOCK = "B";
        public const string PENDING = "P";
        public const string REJECT = "R";
        public const string ALL = "ALL";
        public const string PENDINGFORDELETE = "G";
        public const string PENDINGFORACTIVE = "C";
        public const string PENDINGFORAPPROVE = "H";
        public const string REJECTFORMNEW = "K";

        public const string PERSONAL = "P";
        public const string CORPORATE = "O";
        public const string PERSONALLKG = "J";

        public const string PERSONALCONTRACT = "PCO";
        public const string CORPORATECONTRACT = "CCO";
        //Contract
        public const string INSERTCONTRACT = "INSERTCONTRACT";
        public const string CONTRACTNO = "CONTRACTNO";
        public const string NEWPRODUCT = "NEWPRODUCT";
        public const string NEWLEVEL = "NEWLEVEL";
        public const string USERCREATE = "USERCREATE";
        public const string CREATEDATE = "CREATEDATE";
        public const string ENDDATE = "ENDDATE";
        public const string CONTRACTTYPE = "CONTRACTTYPE";
        public const string CUSTCODECORE = "CUSTCODECORE";
        public const string LEVEL = "LEVEL";
        public const string NEXTEXECUTE = "NEXTEXECUTE";
        public const string DAYNO = "DAYNO";
        public const string TIMEEXCUTE = "TIMEEXCUTE";
        public const string SCHEDULETIME = "SCHEDULETIME";

        //ACCOUNT

        public const string ACCTTYPE = "ACCTTYPE";
        public const string ACCTNO = "ACCTNO";
        public const string DDACCOUNT = "DDACCOUNT";
        public const string LACCTNO = "LACCTNO";
        public const string LTT = "LTT";
        public const string LDH = "LDH";
        public const string APPDESC = "APPDESC";
        public const string SENDERNAME = "SENDERNAME";
        public const string RECEIVERNAME = "RECEIVERNAME";
        public const string RECEIVERACCOUNT = "RECEIVERACCOUNT";
        public const string FROMDATE = "FROMDATE";
        public const string TODATE = "TODATE";
        public const string TRANSFERTYPE = "TRANSFERTYPE";
        public const string BANKNAME = "BANKNAME";
        public const string BALANCE = "BALANCE";
        public const string LASTTRANSDATE = "LASTTRANSDATE";
        public const string OPENDATE = "OPENDATE";
        public const string EXPIREDATE = "EXPIREDATE";
        public const string INTERESTRATE = "INTERESTRATE";
        public const string BLOCKBALANCE = "BLOCKBALANCE";
        public const string ACCRUEDCRINTEREST = "ACCRUEDCRINTEREST";
        public const string ACCRUEDODINTEREST = "ACCRUEDODINTEREST";
        public const string ODLIMIT = "ODLIMIT";
        public const string CUSTOMERNAME = "CUSTOMERNAME";
        public const string CCYCD = "CCYCD";
        public const string CITYNAME = "CITYNAME";
        public const string FEE = "FEE";
        public const string SEDFEE = "feeSenderAmt";
        public const string REVFEE = "feeReceiverAmt";
        public const string ACTIONDATE = "ACTIONDATE";
        public const string TOTALAMOUNT = "TOTALAMOUNT";
        public const string CREDITBRACHID = "CREDITBRACHID";
        public const string DEBITBRACHID = "DEBITBRACHID";
        public const string CREFEE = "CREFEE";
        public const string DEBITFEE = "DEBITFEE";

        //Users
        public const string USERID = "USERID";
        public const string CUSRAPPID = "CUSRAPPID";
        public const string USERTYPE = "USERTYPE";
        public const string USERLEVEL = "USERLEVEL";
        public const string BIRTHDAY = "BIRTHDAY";
        public const string INSERTUSER = "INSERTUSER";
        public const string UPDATEUSER = "UPDATEUSER";
        public const string DEPTID = "DEPTID";
        public const string BATCHREF = "BATCHREF";
        public const string BATCHTABLE = "BATCHTABLE";
        public const string CHECKNO = "CHECKNO";

        public const string ISLEVEL = "ISLEVEL";

        public const string UPDATEUSERIB = "UPDATEUSERIB";
        public const string UPDATEUSERSMS = "UPDATEUSERSMS";
        public const string UPDATEUSERMB = "UPDATEUSERMB";
        public const string UPDATEUSERPHO = "UPDATEUSERPHO";

        public const string INSERTIBANKUSER = "INSERTIBANKUSER";
        public const string INSERTSMSUSER = "INSERTSMSUSER";
        public const string INSERTMBUSER = "INSERTMBUSER";
        public const string INSERTPHOUSER = "INSERTPHOUSER";
        public const string INSERTIBANKUSERRIGHT = "INSERTIBANKUSERRIGHT";
        public const string INSERTSMSUSERRIGHT = "INSERTSMSUSERRIGHT";
        public const string INSERTMBUSERRIGHT = "INSERTMBUSERRIGHT";
        public const string INSERTPHOUSERRIGHT = "INSERTPHOUSERRIGHT";

        public const string INSERTCONTRACTROLEDETAIL = "INSERTCONTRACTROLEDETAIL";
        public const string INSERTCONTRACTACCOUNT = "INSERTCONTRACTACCOUNT";
        public const string INSERTTRANRIGHTDETAIL = "INSERTTRANRIGHTDETAIL";
        public const string INSERTUSERACCOUNT = "INSERTUSERACCOUNT";
        public const string INSERTDEPTDEFAULT = "INSERTDEPTDEFAULT";
        public const string INSERTROLEDEFAULT = "INSERTROLEDEFAULT"; 
        public const string INSERTUSERINROLE = "INSERTUSERINROLE";
        public const string INSERTUSERPASSWORD = "INSERTUSERPASSWORD";
        public const string PARENTTYPE = "PARENTTYPE";

        //date default
        public const string DATEDEFAULT = "01/01/1900";

        //action
        public const string ADD = "add";
        public const string EDIT = "edit";
        public const string VIEWDETAIL = "viewdetail";

        //contract
        public const string CONTRACTNOPREFIX = "CT";
        //user 
        public const string CUSNOPREFIX = "CU";
        public const string CUSINDIVIDUALTYPEPREFIX = "P";
        public const string CUSCOPORATETYPEPREFIX = "C";
        //contract
        public const string BRANCHNAME = "BRANCHNAME";
        public const string ADDRESS = "ADDRESS";
        public const string CITYCODE = "CITYCODE";
        public const string DISTCODE = "DISTCODE";
        public const string PHONE = "PHONE";
        public const string BRANCHDESC = "BRANCHDESC";
        public const string BANKID = "BANKID";
        public const string ISOPENFD = "ISOPENFD";
        public const string POSITIONX = "POSITIONX";
        public const string POSITIONY = "POSITIONY";

        public const string USERNAME = "USERNAME";
        public const string PASSWORD = "PASSWORD";

        public const string ROLENAME = "ROLENAME";
        public const string ROLEDESCRIPTION = "ROLEDESCRIPTION";
        public const string USERCREATED = "USERCREATED";
        public const string USERMODIFIED = "USERMODIFIED";
        public const string DATEMODIFIED = "DATEMODIFIED";
        //dept
        public const string DEPTIDPREFIX = "D";
        public const string DEPTNAME = "DEPTNAME";

        //Teller Approve Transaction
        public const string TELLER = "TELLER";
        public const string APPTRANID = "APPTRANID";
        public const string FROMLIMIT = "FROMLIMIT";
        public const string TOLIMIT = "TOLIMIT";
        public const string YES = "Y";
        public const string NO = "N";
        public const string INSERTTELLERAPPTRAN = "INSERTTELLERAPPTRAN";
        public const string INSERTTELLERAPPTRANDETAIL = "INSERTTELLERAPPTRANDETAIL";
        public const string UPDATETELLERAPPTRAN = "UPDATETELLERAPPTRAN";
        public const string UPDATETELLERAPPTRANDETAIL = "UPDATETELLERAPPTRANDETAIL";
        public const string PROCESSAPPROVEPREFIX = "PA";
        //Set limit for teller approve
        public const string LIMITAPPROVE = "LIMITAPPROVE";


        //CORP USER Approve Transaction

        public const string UPDATEUSERAPPTRAN = "UPDATEUSERAPPTRAN";
        public const string UPDATEUSERAPPTRANDETAIL = "UPDATEUSERAPPTRANDETAIL";
        //DBO.Page_Name
        public const string ISSCHEDULE = "ISSCHEDULE";
        public const string ISTEMPLATE = "ISTEMPLATE";
        public const string PARAM = "PARAM";
        public const string ISRECEIVE = "ISRECEIVE";
        public const string ISNOTIFICATION = "ISNOTIFICATION";
        public const string ISREPORT = "ISREPORT";
        public const string ISVIEWREPORT = "ISVIEWREPORT";

        //Schedule
        public const string DAILY = "DAILY";
        public const string WEEKLY = "WEEKLY";
        public const string MONTHLY = "MONTHLY";
        public const string ONETIME = "ONETIME";
        public const string SCHEDULEPREFIX = "SCH";
        public const string SCHEDULEID = "SCHEDULEID";
        public const string SCHEDULEACTION = "SCHEDULEACTION";
        public const string SCHEDULETYPE = "SCHEDULETYPE";
        public const string RECIEVERACCTNO = "RECIEVERACCTNO";
        public const string SENDERACCTNO = "SENDERACCTNO";
        public const string IPCSCHEDULESINSERT = "IPCSCHEDULESINSERT";
        public const string IPCSCHEDULEDAYINSERT = "IPCSCHEDULEDAYINSERT";
        public const string IPCSCHEDULEDETAILINSERT = "IPCSCHEDULEDETAILINSERT";
        public const string BANKRECEIVE = "BANKRECEIVE";
        public const string CHILDBANK = "CHILDBANK";
        public const string PROVINCE = "PROVINCE";
        //Template Transfer Name
        public const string TEMPLATENAME = "TEMPLATENAME";
        public const string TEMPLATEID = "TEMPLATEID";
        public const string ID = "ID";
        public const string TIB = "IB000208";
        public const string TOB = "IB000206";
        public const string BAC = "IB000201";
        public const string DD = "DD";
        public const string FD = "FD";
        public const string FDACCOUNT = "FDACCOUNT";
        public const string IN = "I";
        public const string OUT = "O";
        public const string TRANFERNAME = "TRANFERNAME";
        public const string TRANFERVALUE = "TRANFERVALUE";
        public const string SENDERACCOUNT = "SENDERACCOUNT";
        public const string EXECNOW = "EXECNOW";
        public const string EXECDATE = "EXECDATE";
        public const string COUNTRYCODE = "COUNTRYCODE";
        public const string IDENTIFYNO = "IDENTIFYNO";
        public const string CHARGEFEE = "CHARGEFEE";

        //Map trantype
        public const string TRANTYPECORE = "TRANTYPECORE";
        public const string LANGID = "LANGID";
        //FEE
        public const string FEEID = "FEEID";
        public const string FEENAME = "FEENAME";
        public const string FEETYPE = "FEETYPE";
        public const string FIXAMOUNT = "FIXAMOUNT";
        public const string ISLADDER = "ISLADDER";
        public const string INSERTFEE = "INSERTFEE";
        public const string UPDATEFEE = "UPDATEFEE";
        public const string INSERTFEEDETAILS = "INSERTFEEDETAILS";
        //Product FEE
        public const string ISPRODUCTFEE = "ISPRODUCTFEE";
        public const string PAYER = "PAYER";
        public const string ISLOCAL = "ISLOCAL";
        //exchange
        public const string DATE = "DATE";
        //TypeID trong EBA_Users
        public const string CHUTAIKHOAN = "CTK";
        public const string NGUOIUYQUYEN = "NUY";
        public const string QUANTRIHETHONG = "QTHT";
        public const string NGUOIDUNGCAP2 = "C2";
        public const string QUANLYTAICHINH = "QLTC";
        public const string KETOAN = "KT";
        public const string DONGCHUTAIKHOAN = "DCKT";
        public const string CHECKER = "CK";
        public const string MAKER = "MK";
        public const string ADMIN = "AD";

        //Report management
        public const string RPTID = "RPTID";
        public const string VIVN = "vi-VN";
        public const string ENUS = "en-US";
        public const string DROPDOWNLIST = "DropDownList";
        public const string RPTNAME = "RPTNAME";
        public const string SERVICE = "SERVICE";
        public const string ISSHOW = "ISSHOW";
        public const string INSERTREPORT = "INSERTREPORT";
        public const string UPDATEREPORT = "UPDATEREPORT";
        public const string INSERTREPORTDETAILS = "INSERTREPORTDETAILS";
        //Water bill payment
        public const string PHUONGNAM = "PHUONGNAM";
        public const string PAYMENTPROVIDERID = "PAYMENTPROVIDERID";
        public const string PAYMENTTYPEID = "PAYMENTTYPEID";
        public const string DIRECTNUM = "DIRECTNUM";
        public const string ADDRESSCHOLON = "ADDRESSCHOLON";
        public const string KEYID = "KEYID";
        public const string BILLINFO = "BILLINFO";
        //
        public const string LAK = "LAK";
        public const string MMK = "MMK";
        public const string CREATEID = "CREATEID";
        public const string NAME = "NAME";
        //Poster
        public const string POSTERID = "POSTERID";
        public const string FILENAME = "FILENAME";
        public const string PATH = "PATH";
        public const string WIDTH = "WIDTH";
        public const string HEIGHT = "HEIGHT";
        public const string POSITION = "POSITION";
        public const string IDX = "IDX";
        public const string PUBLISH = "PUBLISH";
        public const string TYPE = "TYPE";
        //topup
        public const string TELCO = "TELCO";
        public const string CARDTYPE = "CARDTYPE";
        public const string SERIALNO = "SERIALNO";
        public const string SOFTPIN = "SOFTPIN";
        public const string RPTPATH = "RPTPATH";
        public const string BUYRATE = "BUYRATE";
        public const string SELLRATE = "SELLRATE";

        //fast banking
        public const string ORDERCODE = "ORDERCODE";
        public const string SHOPCODE = "SHOPCODE";
        public const string RETURNURL = "RETURNURL";
        public const string SESSIONID = "SESSIONID";
        public const string SECRETCODE = "SECRETCODE";

        //bill payment
        public const string CORPLIST = "CORPLIST";
        public const string CORPID = "CORPID";
        public const string SERVICELIST = "SERVICELIST";
        public const string SERVICEINFO = "SERVICEINFO";
        public const string CATID = "CATID";
        public const string REFVA1 = "REFVA1";
        public const string REFVA2 = "REFVA2";
        public const string REFVA3 = "REFVA3";
        public const string MISINFOR = "MISINFOR";

        //Exess sweeping
        public const string CENACCTNO = "CENACCTNO";
        public const string SUBACCTNO = "SUBACCTNO";
        public const string SURLEVEL = "SURLEVEL";
        public const string SHRLEVEL = "SHRLEVEL";

        //send contract infor by sms
        public const string IBUSER = "IBUSER";
        public const string IBPASS = "IBPASS";
        public const string SMSPHONE = "SMSPHONE";
        public const string SMSDEFAULTACC = "SMSDEFAULTACC";
        public const string SMSPINCODE = "SMSPINCODE";
        public const string PHOUSER = "PHOUSER";
        public const string PHOPASS = "PHOPASS";
        public const string PHOPINCODE = "PHOPINCODE";
        public const string PHONENO = "PHONENO";
        public const string BOTH = "Both";

        public const string RESETINFO = "RESETINFO";

        public const string USSDCode = "USSDCode";

        public const string CMSUSER = "CMSUSER";
        public const string LIMITTYPE = "LIMITTYPE";
        public const string NOR = "Normal";

        public const string NORMAL = "NOR";
        public const string DEB = "Debit limit for each branch";

        #region Get Working Date
        public static string LoadWorkingDate()
        {
            try
            {
                DataTable iRead = new DataTable();
                iRead = DataAccess.GetFromDataTable("IPC_GETWORKINGDATE", null);
                if (iRead.Rows.Count != 0)
                {
                    //DateTime.ParseExact(datetime, formatInput, null).ToString(formatOutput);
                    return iRead.Rows[0]["VARVALUE"].ToString();
                }
                else
                {
                    return DateTime.Now.ToString("dd/MM/yyyy");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion
        //minh add 12.10.2015

        public const string FINISHED = "F";
        public const string OVERDATE = "O";
        public const string ISAPPROVED = "ISAPPROVED";
        public const string stAPPROVE = "APPROVE";
        public const string stReject = "REJECT";
        //minh add 11.11.2015
        public const string SYSTEMDATE = "SYSTEMDATE";
        public const string SYSTEMTIME = "SYSTEMTIME";
        public const string WYEAR = "WYEAR";
        public const string WMONTH = "WMONTH";
        public const string WDAY = "WDAY";
        public const string WHOUR = "WHOUR";
        public const string WMINUTE = "WMINUTE";
        public const string WSECOND = "WSECOND";
        public const string CONTRACTINDIVIDUAL = "IND";
        public const string CONTRACTCORPADVANCE = "ADV";
        public const string CONTRACTCORPSIMPLE = "SIM";

        //quangtv
    
        public const string CONTRACTAGENTMERCHANT = "AM";

        public const string PRCAGENTMERCHANT = "AM";
        public const string PRCTYPECONSUMER = "C";
        //ib,mb notification
        public const string VALUE5 = "VALUE5";
        public const string VALUE6 = "VALUE6";
        public const string VALUE7 = "VALUE7";
        public const string VALUE8 = "VALUE8";
        public const string VALUE9 = "VALUE9";
        public const string VALUE10 = "VALUE10";
        public const string CONTENT = "CONTENT";
        public const string VARNAME = "VARNAME";
        public const string LINK = "LINK";
        public const string LOOPTIME = "LOOPTIME";
        //public const string DEVICETYPE = "DEVICETYPE";
        public const string MBVERSION = "MBVERSION";
        public const string STARTTIME = "STARTTIME";
        public const string ENDTIME = "ENDTIME";
        public const string STARTDATE = "STARTDATE";

        //sms notification
        public const string INSERTSMSNO = "INSERTSMSNO";
        public const string INSERTSMSNODETAILS = "INSERTSMSNODETAILS";
        public const string REGIONID = "REGIONID";
        public const string REGIONNAME = "REGIONNAME";
        public const string REGIONCODE = "REGIONCODE";
        public const string REGIONTYPE = "REGIONTYPE";
        public const string USERMODIFY = "USERMODIFY";
        //credit card
        public const string MESSAGEID = "messageId";
        public const string HOSTID = "hostId";
        public const string CARDNO = "cardNo";
        public const string CARDPLASTICCODE = "cardPlasticCode";
        public const string DEBITACCTNO = "DEBITACCTNO";
        public const string GLACNO = "GLACNO";
        public const string TRANREF = "TRANREF";
        public const string cifNo = "cifNo";
        public const string adjAmt = "adjAmt";
        public const string currId = "currId";
        public const string txnType = "txnType";
        public const string desc = "desc";
        public const string respCode = "respCode";
        public const string respDetail = "respDetail";
        public const string OTH = "OTH";
        public const string OWN = "OWN";

        //card managerment
        public const string CARDHOLDERCUSTCODE = "CARDHOLDERCUSTCODE";
        public const string INSERTCONTRACTCARDHOLDER = "INSERTCONTRACTCARDHOLDER";
        public const string INSERTUSERCARD = "INSERTUSERCARD";
        public const string INSERTUSERCARDRIGHT = "INSERTUSERCARDRIGHT";
        public const string INSERTUSERCARDRIGHTDETAIL = "INSERTUSERCARDRIGHTDETAIL";

        public const string UPDATECONTRACTCARDHOLDER = "UPDATECONTRACTCARDHOLDER";
        public const string UPDATEUSERCARD = "UPDATEUSERCARD";
        public const string UPDATEUSERCARDRIGHT = "UPDATEUSERCARDRIGHT";
        public const string UPDATEUSERCARDRIGHTDETAIL = "UPDATEUSERCARDRIGHTDETAIL";
        public const string UPDATECONTRACTROLEDETAIL = "UPDATECONTRACTROLEDETAIL";
        public const string RIGHTTYPE = "RIGHTTYPE";
        public const string CARD = "CARD";

        //ELoad
        public const string SessionID = "SessionID";
        //policy:
        public const string FAILNUMBER = "FAILNUMBER";

        public const string EXPIRETIME = "EXPIRETIME";

        public const string UUID = "UUID";

        public const string DATEEXPIRE = "DATEEXPIRE";

        public const string MULTILOGIN = "MULTILOGIN";

        public const string ISLOGIN = "ISLOGIN";


        public const string policyid = "POLICYID";

        public const string policydesc = "POLICYDESC";

        public const string effrom = "EFFROM";

        public const string effto = "EFFTO";

        public const string pwdhis = "PWDHIS";

        public const string pwdagemax = "PWDAGEMAX";

        public const string minpwdlen = "MINPWDLEN";

        public const string pwdcplx = "PWDCPLX";

        public const string pwdcplxlc = "PWDCPLXLC";

        public const string pwdcplxuc = "PWDCPLXUC";

        public const string pwdcplxsc = "PWDCPLXSC";

        public const string pwdcplxnc = "PWDCPLXNC";

        public const string timelginrq = "TIMELGINRQ";

        public const string timelginfr = "TIMELGINFR";

        public const string timelginto = "TIMELGINTO";

        public const string lkoutthrs = "LKOUTTHRS";

        public const string resetlkout = "RESETLKOUT";

        public const string registerpolicy = "REGISTERPOLICY";

        public const string ISDEFAULT = "ISDEFAULT";

        public const string SEMS = "SEMS";

        public const string PAGESIZE = "PAGESIZE";

        public const string PAGEINDEX = "PAGEINDEX";
        public const string PASSRESET = "PASSRESET";
        public const string CLIENTINFO = "CLIENTINFO";

        //e wallet
        public const string EWCODE = "EWCODE";
        public const string EWNAME = "EWNAME";


        public const string STARTROWINDEX = "STARTROWINDEX";
        public const string MAXIMUMROWS = "MAXIMUMROWS";
        public const string CORPNAME = "CORPNAME";

        //APPROVAL STRUCTURE AND WORKFLOW
        public const string STRUCTURENAME = "STRUCTURENAME";
        public const string STRUCTUREID = "STRUCTUREID";
        public const string SHORTDATEFORMAT = "MM/dd/yyyy";
        public const string GROUPID = "GROUPID";
        public const string WORKFLOWID = "WORKFLOWID";
        public const string ISAOT = "ISAOT";


        public readonly static Dictionary<string, string> DICGROUPID = new Dictionary<string, string>()
        {
            {"A","A" },
            {"B","B" },
            {"C","C" },
            {"D","D" },
            {"E","E" },
            {"F","F" },
            {"G","G" },
            {"H","H" },
            {"I","I" },
            {"K","K" },
            {"L","L" },
            {"M","M" },
            {"N","N" },
            {"O","O" },
            {"P","P" },
            {"Q","Q" },
            {"R","R" },
            {"S","S" },
            {"T","T" },
            {"U","U" },
            {"V","V" },
            {"X","X" },
            {"Y","Y" }
        };

        public const string RIPPLEURI = "RIPPLEURI";
        public const string MT103ACCEPTPATH = "MT103ACCEPTPATH";
        public const string MT103REJECTPATH = "MT103REJECTPATH";
        public const string ISSENDER = "ISSENDER";
        public const string AUTOGETQUOTE = "AUTOGETQUOTE";
        public const string AUTOACCEPTQUOTE = "AUTOACCEPTQUOTE";
        public readonly static Dictionary<string, string> DICSTATUS = new Dictionary<string, string>()
        {
            {"A","Actived" },
            {"D","Deleted" }
        };

        public const string TRANSACTION = "TRANSACTION";
        public const string CURRENCY = "CURRENCY";
        public const string PAGENAME = "PAGENAME";
        public const string APPROVALLIMIT = "APPROVALLIMIT";



        public const string CONTRACTCORPMATRIX = "MTR";
        public const string MTRUSER = "MTR";


        public const string INSERTUSERGROUP = "INSERTUSERGROUP";
        public const string DELETEUSERGROUP = "DELETEUSERGROUP";

        public const string FROMCCYID = "FROMCCYID";
        public const string TOCCYID = "TOCCYID";
        public const string TRANSID = "TRANSID";
        public const string DATETO = "DATETO";
        public const string DATEFROM = "DATEFROM";
        public const string REALMONEY = "REALMONEY";
        public const string CHANNELNAME = "CHANNELNAME";
        public const string SUNSACCNO = "SUNSACCNO";
        public const string SUSPACCNO = "SUSPACCNO";
        public const string INCOMACCNO = "INCOMACCNO";
        public const string CHANNELCODE = "CHANNELCODE";
        public const string CARDID = "CARDID";
        public const string SUNDRIESACCN = "SUNDRIESACCN";
        public const string SUSPENDACCNO = "SUSPENDACCNO";
        public const string ETOPUP = "ETOPUP";
        public const string ELOADSUNDRIESACCNO = "ELOADSUNDRIESACCNO";
		public const string ELOADSUSPENDACCNO = "ELOADSUSPENDACCNO";

		public const string ELOADINCOMACCNO = "ELOADINCOMACCNO";

		public const string DATAPACK = "DATAPACK";
		public const string CARDAMOUNT = "CARDAMOUNT";
		public const string REALAMOUNT = "REALAMOUNT";
		public const string SUPPLIERID = "SUPPLIERID";
		public const string PREFIX = "PREFIX";
		public const string COUNTRYPREFIX = "COUNTRYPREFIX";
		public const string TELCOID = "TELCOID";
		public const string COUNTRYNAME = "COUNTRYNAME";
		public const string PHONELEN = "PHONELEN";
        
        //QuanNN - 20190918 - push notification
        public const string NOTIFICATIONTYPE = "NOTIFICATIONTYPE";
        public const string TITLE = "TITLE";
        public const string BODY = "BODY";
        public const string DETAILS = "DETAILS";
        public const string VALUE = "VALUE";
        public const string TYPENOTIFY = "TYPENOTIFY";
        public const string USERECREATED = "USERECREATED";
        public const string APPROVER = "APPROVER";
        public const string EBA_PUSHNOTIFICATIONADD = "EBA_PUSHNOTIFICATIONADD";
        public const string EBA_PUSHNOTIFICATIONEDIT = "EBA_PUSHNOTIFICATIONEDIT";
        public const string IPC_SCHEDULEDAYDELETE = "IPC_SCHEDULEDAYDELETE";
        public const string IPC_SCHEDULESUPDATE = "IPC_SCHEDULESUPDATE";
        public const string ALLOWFOREIGN = "ALLOWFOREIGN";
        public const string COUNTRYID = "COUNTRYID";

        //TAXPAYMENT
        public const string CRBANKBIC = "CRBANKBIC";
        public const string SENDERADDRESS = "SENDERADDRESS";
        public const string SENDERPHONE = "SENDERPHONE";
        public const string TAXTYPE = "TAXTYPE";
        public const string PAYMENTTYPE = "PAYMENTTYPE";
        public const string REVACCOUNT = "REVACCOUNT";
        public const string TAXPERIOD = "TAXPERIOD";
        public const string INCOMEYEAR = "INCOMEYEAR";
        public const string TINNO = "TINNO";
        public const string IRDREFNO = "IRDREFNO";
        public const string TAXTYPENAME = "TAXTYPENAME";
        public const string PAYMENTTYPENAME = "PAYMENTTYPENAME";
        public const string TAXPERIODNAME = "TAXPERIODNAME";
        public const string INCOMEYEARNAME = "INCOMEYEARNAME";
        public static class ACTIONPAGE
        {
            public const string ADD = "ADD";
            public const string EDIT = "EDIT";
            public const string DELETE = "DELETE";
            public const string DETAILS = "DETAILS";
            public const string EXPORT = "EXPORT";
            public const string REVIEW = "REVIEW";
            public const string CLOSE = "CLOSE";
            public const string UPLOAD = "UPLOAD";
            public const string ROLLBACK = "ROLLBACK";
            public const string LIST = "LIST";
            public const string NOACTION = "NOACTION";
            public const string PRINT = "PRINT";
            public const string APPROVE = "APPROVE";
            public const string REJECT = "REJECT";
        }


        public const string CORPCODE = "CORPCODE";
        public const string BILLER = "BILLER";
        public const string DEFAULTFORMATDATE = "dd/MM/yyyy";

	   public const string WAL = "WAL";
	   public const string MBA = "MBA";
	   public const string EAM = "EAM";
        public const string CURRENCYNAME = "CURRENCYNAME";
        public const string SCURRENCYID = "SCURRENCYID";
        public const string CURRENCYNUMBER = "CURRENCYNUMBER";
        public const string MASTERNAME = "MASTERNAME";
        public const string DECIMALDIGITS = "DECIMALDIGITS";
        public const string ROUNDINGDIGIT = "ROUNDINGDIGIT";
        public const string ORDER = "ORDER";


        public const string AGENTMERCHANT = "AM";
        public const string CONSUMER = "C";
        public const string AGENT = "A";
        public const string MERCHANT = "M";


        public const string TAXCODE = "TAXCODE";
        public const string BICCODE = "BICCODE";
        public const string SWIFTCODE = "SWIFTCODE";
        public const string TIMEOPEN = "TIMEOPEN";
        public const string TIMECLOSE = "TIMECLOSE";

        public const string ATMCODE = "ATMCODE";

        public const string MCOUNTRYNAME = "MCOUNTRYNAME";
        public const string PHONECOUNTRYCODE = "PHONECOUNTRYCODE";
        public const string CAPITAL = "CAPITAL";
        public const string LANGUAGE = "LANGUAGE";
        public const string TIMEZONE = "TIMEZONE";
        public const string LASTMODIFY = "LASTMODIFY";

        //Parameter
        public static class PARAMETER
        {
            public static string CONSUMER = "IND";
            public static string MERCHANT_AGENT = "AM";
        }

        //Biller Dũng
        public const string BILLERID = "BILLERID";
        public const string BILLERCODE = "BILLERCODE";
        public const string BILLERNAME = "BILLERNAME";
        public const string LOGOBIN = "LOGOBIN";
        public const string LOGOTYPE = "LOGOTYPE";
        public const string SHOWASBILL = "SHOWASBILL";
        public const string BASE64 = "BASE64";
        public const string URI = "URI";
        public const string INACTIVE = "I";
        public const string CITYID = "CITYID";
        public const string WEBSITE = "WEBSITE";
        public const string RECINDEX = "RECINDEX";
        public const string RECPERPAGE = "RECPERPAGE";


        public const string ELOADCODE = "ELOADCODE";
        public const string EPINCODE = "EPINCODE";
        public const string ELOADBILLERCODE = "ELOADBILLERCODE";
        public const string EPINBILLERCODE = "EPINBILLERCODE";
        public const string EPIN = "EPIN";
        public const string ELOAD = "ELOAD";
        public enum TOP_CARD_PREFIX_TYPE
        {
            PREFIX = 1,
            CARD = 2
        }

        public const string VALUECODE = "VALUECODE";
        public const string VALUENAME = "VALUENAME";
        public const string CAPTION = "CAPTION";
        public const string GROUP = "GROUP"; 
        public const string SUNDRYACCTNOBANK = "SUNDRYACCTNOBANK";
        public const string INCOMEACCTNOBANK = "INCOMEACCTNOBANK";
        public const string SUNDRYACCTNOWALLET = "SUNDRYACCTNOWALLET";
        public const string INCOMEACCTNOWALLET = "INCOMEACCTNOWALLET";

        public const string ISOTC = "IsOtc";
        public const string bittrue = "1";
        public const string bitfalse = "0";
        public const string LOGINMETHOD = "LOGINMETHOD";
        public const string SUBUSERCODE = "SUBUSERCODE";
        public const string IsUse = "IsUse";
        public const string TOWNSHIPCODE = "TOWNSHIPCODE";
        public const string TOWNSHIPNAME = "TOWNSHIPNAME";
        public const string TOWNSHIPNAMEMM = "TOWNSHIPNAMEMM";
        public const string DISTNAMEMM = "DISTNAMEMM";
        public const string CITYNAMEMM = "CITYNAMEMM";

        public const string DETERMINATION = "DETERMINATION";
        public const string ISMANUAL = "ISMANUAL";
        public const string PARTNERBANKID = "PARTNERBANKID";
        public const string BRANCHCODE = "BRANCHCODE";
        public const string ORIGINALBRANCHCODE = "ORIGINALBRANCHCODE";
        //hunglt ipc region
        public const string REGIONSPECIAL = "REGIONSPECIAL";
    }
}
