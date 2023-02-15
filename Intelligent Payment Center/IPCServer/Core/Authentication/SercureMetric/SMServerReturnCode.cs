using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authentication.SercureMetric
{
    class SMServerReturnCode
    {
        public const uint OTPR_CORE_WS_SUIT = 0x80000100;
        public const uint OTPR_CORE_NOT_INIT = 0x80000101;
        public const uint OTPR_CORE_CREATE_INSTANCE_FAILED = 0x80000102;
        public const uint OTPR_CORE_POLICY_CALL_FUNC_WRONG = 0x80000103;
        public const uint OTPR_CORE_POLICY_INVALID_POLICYTYPE = 0x80000104;
        public const uint OTPR_CORE_GROUPNAME_EMPTY = 0x80000105;
        public const uint OTPR_CORE_DB_GROUPNAME_EXIST = 0x80000106;
        public const uint OTPR_CORE_DB_GROUPNAME_NOT_EXIST = 0x80000107;
        public const uint OTPR_CORE_INVALID_PARAM = 0x80000108;
        public const uint OTPR_CORE_INVALID_PARAM_STATUS = 0x80000109;
        public const uint OTPR_CORE_INVALID_PARAM_USERNAME = 0x80000110;
        public const uint OTPR_CORE_INVALID_PARAM_TOKEN = 0x80000111;
        public const uint OTPR_CORE_INVALID_PARAM_GROUPNAME = 0x80000112;
        public const uint OTPR_CORE_INVALID_PARAM_PIN = 0x80000113;
        public const uint OTPR_CORE_INVALID_PARAM_NEEDPIN = 0x80000114;
        public const uint OTPR_CORE_INVALID_PARAM_OTP = 0x80000115;
        public const uint OTPR_CORE_INVALID_PARAM_FLAG = 0x80000116;
        public const uint OTPR_CORE_INVALID_PARAM_SIGN = 0x80000117;
        public const uint OTPR_CORE_INVALID_PARAM_SIGNDATA = 0x80000118;
        public const uint OTPR_CORE_AUTH_FAILED = 0x80000119;
        public const uint OTPR_CORE_SYNC_FAILED = 0x80000120;
        public const uint OTPR_CORE_SIGN_EMPTY = 0x80000121;
        public const uint OTPR_CORE_SIGN_DATA_EMPTY = 0x80000122;
        public const uint OTPR_CORE_VERIFY_FAILED = 0x80000123;
        public const uint OTPR_CORE_OTP_API_UNKNOW_ERROR = 0x80000125;
        public const uint OTPR_CORE_WS_ACCESS_DENIED = 0x80000126;
        public const uint OTPR_CORE_USER_SOURCE_NOT_SUPPORT = 0x80000127;
        public const uint OTPR_CORE_USER_SOURCE_USER_EXISTS = 0x80000128;
        public const uint OTPR_CORE_USER_SOURCE_USER_NOTEXISTS = 0x80000129;
        public const uint OTPR_CORE_COMM_SUIT = 0x80001000;
        public const uint OTPR_CORE_COMM_FAIL = 0x80001001;
        public const uint OTPR_CORE_COMM_INVALID_PARAMETER = 0x80001002;
        public const uint OTPR_CORE_COMM_INVALID_LICENSE = 0x80001003;
        public const uint OTPR_CORE_COMM_EXPIRED_LICENSE = 0x80001004;
        public const uint OTPR_CORE_COMM_EXPIRED_LOG4J = 0x80001077;
        public const uint OTPR_CORE_AUTH_SUIT = 0x80002000;
        public const uint OTPR_CORE_UID_EMPTY = 0x80002001;
        public const uint OTPR_CORE_OTP_EMPTY = 0x80002002;
        public const uint OTPR_CORE_PIN_EMPTY = 0x80002003;
        public const uint OTPR_CORE_TSN_EMPTY = 0x80002004;
        public const uint OTPR_CORE_INVALID_UID = 0x80002005;
        public const uint OTPR_CORE_INVALID_OTP = 0x80002006;
        public const uint OTPR_CORE_INVALID_TOKENKEY = 0x80002007;
        public const uint OTPR_CORE_INVALID_AUTHNUM = 0x80002008;
        public const uint OTPR_CORE_NEED_SYNC = 0x80002009;
        public const uint OTPR_CORE_INVALID_TIMEDRIFT = 0x8000200a;
        public const uint OTPR_CORE_INVALID_USERPIN = 0x8000200b;
        public const uint OTPR_CORE_INVALID_DBUPIN = 0x8000200c;
        public const uint OTPR_CORE_ERR_GETUSER = 0x8000200d;
        public const uint OTPR_CORE_ERR_GETTOKEN = 0x8000200e;
        public const uint OTPR_CORE_TOKEN_LOCKED = 0x8000200f;
        public const uint OTPR_CORE_LOGIN_LOCKED = 0x80002010;
        public const uint OTPR_CORE_PIN_NOTINIT = 0x80002011;
        public const uint OTPR_CORE_INVALID_TOKENTYPE = 0x80002012;
        public const uint OTPR_CORE_INVALID_TOKENSN = 0x80002013;
        public const uint OTPR_CORE_ERR_GETASSIGNED = 0x80002014;
        public const uint OTPR_CORE_ERR_GETTOKENNUM = 0x80002015;
        public const uint OTPR_CORE_TOKEN_BINDED = 0x80002016;
        public const uint OTPR_CORE_BIND_EXCEED = 0x80002017;
        public const uint OTPR_CORE_NEED_VERIFYPIN = 0x80002018;
        public const uint OTPR_CORE_USER_INACTIVE = 0x80002019;
        public const uint OTPR_CORE_AGENT_EMPTY = 0x80002020;
        public const uint OTPR_CORE_HOST_EMPTY = 0x80002021;
        public const uint OTPR_CORE_SHARE_KEY_EMPTY = 0x80002022;
        public const uint OTPR_CORE_INVALID_TOKEN_TYPE = 0x80002023;
        public const uint OTPR_CORE_TOKEN_NOTENABLED = 0x80002024;
        public const uint OTPR_CORE_TOKEN_LOGOUT = 0x80002025;
        public const uint OTPR_CORE_DB_SUIT = 0x80003000;
        public const uint OTPR_CORE_DB_INVALID_CONFIGFILE = 0x8000300f;
        public const uint OTPR_CORE_DB_INVALID_DBTYPE = 0x80003010;
        public const uint OTPR_CORE_DB_ERROR_CONNECT = 0x80003011;
        public const uint OTPR_CORE_DB_DATASOURCE_NOTFOUND = 0x80003012;
        public const uint OTPR_CORE_DB_DATABASE_NOTFOUND = 0x80003013;
        public const uint OTPR_CORE_DB_SERVER_NOTFOUND = 0x80003014;
        public const uint OTPR_CORE_DB_INVALID_AUTH = 0x80003015;
        public const uint OTPR_CORE_DB_NOTBEUSED_CONNECT = 0x80003016;
        public const uint OTPR_CORE_DB_USER_EXISTS = 0x80003019;
        public const uint OTPR_CORE_DB_USER_NOTEXISTS = 0x8000301a;
        public const uint OTPR_CORE_DB_RECORD_NOTEXISTS = 0x8000301b;
        public const uint OTPR_CORE_DB_TOKEN_EXISTS = 0x8000301c;
        public const uint OTPR_CORE_DB_TOKEN_NOTEXISTS = 0x8000301d;
        public const uint OTPR_CORE_DB_AGENT_EXISTS = 0x8000301e;
        public const uint OTPR_CORE_DB_AGENT_NOTEXISTS = 0x8000301f;
        public const uint OTPR_CORE_DB_HOST_EXISTS = 0x80003020;
        public const uint OTPR_CORE_DB_HOST_NOTEXISTS = 0x80003021;
        public const uint OTPR_CORE_DB_ADD_USER_FAILED = 0x80003025;
        public const uint OTPR_CORE_DB_DEL_USER_FAILED = 0x80003026;
        public const uint OTPR_CORE_DB_QUERY_USER_FAILED = 0x80003027;
        public const uint OTPR_CORE_DB_UPDATE_USER_FAILED = 0x80003028;
        public const uint OTPR_CORE_DB_ADD_TOKEN_FAILED = 0x80003029;
        public const uint OTPR_CORE_DB_DEL_TOKEN_FAILED = 0x80003030;
        public const uint OTPR_CORE_DB_QUERY_TOKEN_FAILED = 0x80003031;
        public const uint OTPR_CORE_DB_UPDATE_TOKEN_FAILED = 0x80003032;
        public const uint OTPR_CORE_DB_BIND_USER_TOKEN_FAILED = 0x80003033;
        public const uint OTPR_CORE_DB_UNBIND_USER_TOKEN_FAILED = 0x80003034;
        public const uint OTPR_CORE_DB_USER_TOKEN_UNBIND = 0x80003035;
        public const uint OTPR_CORE_DB_ADD_AGENT_FAILED = 0x80003036;
        public const uint OTPR_CORE_DB_DEL_AGENT_FAILED = 0x80003037;
        public const uint OTPR_CORE_DB_CHANGE_AGENT_FAILED = 0x80003038;
        public const uint OTPR_CORE_DB_QUERY_AGENT_FAILED = 0x80003039;
        public const uint OTPR_CORE_DB_ADD_HOST_FAILED = 0x80003040;
        public const uint OTPR_CORE_DB_DEL_HOST_FAILED = 0x80003041;
        public const uint OTPR_CORE_DB_CHANGE_HOST_FAILED = 0x80003042;
        public const uint OTPR_CORE_DB_QUERY_HOST_FAILED = 0x80003043;
        public const uint OTPR_CORE_DB_ADD_AGENT_HOST_FAILED = 0x80003044;
        public const uint OTPR_CORE_DB_DEL_AGENT_HOST_FAILED = 0x80003045;
        public const uint OTPR_CORE_DB_CHANGE_AGENT_HOST_FAILED = 0x80003046;
        public const uint OTPR_CORE_DB_QUERY_AGENT_HOST_FAILED = 0x80003047;
        public const uint OTPR_CORE_DB_ADD_LOG_FAILED = 0x80003048;
        public const uint OTPR_CORE_DB_DEL_LOG_FAILED = 0x80003049;
        public const uint OTPR_CORE_DB_CHANGE_LOG_FAILED = 0x80003050;
        public const uint OTPR_CORE_DB_QUERY_LOG_FAILED = 0x80003051;
        public const uint OTPR_CORE_DB_ADD_CONFIG_FAILED = 0x80003052;
        public const uint OTPR_CORE_DB_DEL_CONFIG_FAILED = 0x80003053;
        public const uint OTPR_CORE_DB_CHANGE_CONFIG_FAILED = 0x80003054;
        public const uint OTPR_CORE_DB_QUERY_CONFIG_FAILED = 0x80003055;
        public const uint OTPR_CORE_DB_ADD_ADMIN_GROUP_FAILED = 0x80003056;
        public const uint OTPR_CORE_DB_DEL_ADMIN_GROUP_FAILED = 0x80003057;
        public const uint OTPR_CORE_DB_CHANGE_ADMIN_GROUP_FAILED = 0x80003058;
        public const uint OTPR_CORE_DB_QUERY_ADMIN_GROUP_FAILED = 0x80003059;
        public const uint OTPR_CORE_DB_ADD_ADMIN_LOG_FAILED = 0x80003060;
        public const uint OTPR_CORE_DB_DEL_ADMIN_LOG_FAILED = 0x80003061;
        public const uint OTPR_CORE_DB_CHANGE_ADMIN_LOG_FAILED = 0x80003062;
        public const uint OTPR_CORE_DB_QUERY_ADMIN_LOG_FAILED = 0x80003063;
        public const uint OTPR_CORE_DB_ADD_ADMIN_USER_FAILED = 0x80003064;
        public const uint OTPR_CORE_DB_DEL_ADMIN_USER_FAILED = 0x80003065;
        public const uint OTPR_CORE_DB_CHANGE_ADMIN_USER_FAILED = 0x80003066;
        public const uint OTPR_CORE_DB_QUERY_ADMIN_USER_FAILED = 0x80003067;
        public const uint OTPR_CORE_DB_ADD_DOMAIN_FAILED = 0x80003068;
        public const uint OTPR_CORE_DB_DEL_DOMAIN_FAILED = 0x80003069;
        public const uint OTPR_CORE_DB_CHANGE_DOMAIN_FAILED = 0x80003070;
        public const uint OTPR_CORE_DB_QUERY_DOMAIN_FAILED = 0x80003071;
        public const uint OTPR_CORE_DB_ADD_TEMP_USER_FAILED = 0x80003072;
        public const uint OTPR_CORE_DB_DEL_TEMP_USER_FAILED = 0x80003073;
        public const uint OTPR_CORE_DB_CHANGE_TEMP_USER_FAILED = 0x80003074;
        public const uint OTPR_CORE_DB_QUERY_TEMP_USER_FAILED = 0x80003075;
        public const uint OTPR_CORE_DB_RELOAD_CONNECTION_FAILED = 0x80003076;
        public const uint OTPR_CORE_DB_ADD_APPGROUP_FAILED = 0x80003201;
        public const uint OTPR_CORE_DB_DEL_APPGROUP_FAILED = 0x80003202;
        public const uint OTPR_CORE_DB_CHANGE_APPGROUP_FAILED = 0x80003203;
        public const uint OTPR_CORE_DB_QUERY_APPGROUP_FAILED = 0x80003204;
        public const uint OTPR_CORE_DB_ADD_USER_GROUP_FAILED = 0x80003205;
        public const uint OTPR_CORE_DB_DEL_USER_GROUP_FAILED = 0x80003206;
        public const uint OTPR_CORE_DB_CHANGE_USER_GROUP_FAILED = 0x80003207;
        public const uint OTPR_CORE_DB_QUERY_USER_GROUP_FAILED = 0x80003208;
        public const uint OTPR_CORE_DB_ADD_ZONE_FAILED = 0x80003212;
        public const uint OTPR_CORE_DB_DEL_ZONE_FAILED = 0x80003213;
        public const uint OTPR_CORE_DB_CHANGE_ZONE_FAILED = 0x80003214;
        public const uint OTPR_CORE_DB_QUERY_ZONE_FAILED = 0x80003215;
        public const uint OTPR_CORE_DB_ADD_LICENSE_FAILED = 0x80003216;
        public const uint OTPR_CORE_DB_DEL_LICENSE_FAILED = 0x80003217;
        public const uint OTPR_CORE_DB_CHANGE_LICENSE_FAILED = 0x80003218;
        public const uint OTPR_CORE_DB_QUERY_LICENSE_FAILED = 0x80003219;
        public const uint OTPR_CORE_DB_CLEANUP_USERDATA_FAILED = 0x80003222;
        public const uint OTPR_CORE_DB_ADD_TOKEN_EXT_FAILED = 0x80003223;
        public const uint OTPR_CORE_DB_QUERY_TOKEN_EXT_FAILED = 0x80003224;
        public const uint OTPR_CORE_DB_CHANGE_TOKEN_EXT_FAILED = 0x80003225;
        public const uint OTPR_CORE_DB_LDAP_INIT_FAILED = 0x80003226;
        public const uint OTPR_CORE_SUCCESS = 0x0;
        public const uint OTPR_CORE_FAILED = 0x1;

        private static String OTPCore_getErrorInfo(uint nCode)
        {
            switch (nCode)
            {
                //case OTPR_CORE_WS_SUIT                          : return "unknown";
                case OTPR_CORE_NOT_INIT: return "OTP API is not initialized";
                case OTPR_CORE_CREATE_INSTANCE_FAILED: return "can not create OTP CORE instance";
                case OTPR_CORE_POLICY_CALL_FUNC_WRONG: return "call function checkParam() with wrong rule";
                case OTPR_CORE_POLICY_INVALID_POLICYTYPE: return "unknow policy rule";
                case OTPR_CORE_GROUPNAME_EMPTY: return "group name is required";
                case OTPR_CORE_DB_GROUPNAME_EXIST: return "group name is existing";
                case OTPR_CORE_DB_GROUPNAME_NOT_EXIST: return "group name does not exist";
                case OTPR_CORE_INVALID_PARAM: return "invalid parameter (null/empty)";
                case OTPR_CORE_INVALID_PARAM_STATUS: return "invalid status (valid 0/1)";
                case OTPR_CORE_INVALID_PARAM_USERNAME: return "invalid user parameter";
                case OTPR_CORE_INVALID_PARAM_TOKEN: return "invalid token parameter";
                case OTPR_CORE_INVALID_PARAM_GROUPNAME: return "invalid group parameter";
                case OTPR_CORE_INVALID_PARAM_PIN: return "invalid PIN parameter";
                case OTPR_CORE_INVALID_PARAM_NEEDPIN: return "invalid NEEDPIN parameter";
                case OTPR_CORE_INVALID_PARAM_OTP: return "invalid OTP parameter";
                case OTPR_CORE_INVALID_PARAM_FLAG: return "invalid flag parameter";
                case OTPR_CORE_INVALID_PARAM_SIGN: return "invalid signature parameter";
                case OTPR_CORE_INVALID_PARAM_SIGNDATA: return "invalid sign data parameter";
                case OTPR_CORE_AUTH_FAILED: return "authentication failed";
                case OTPR_CORE_SYNC_FAILED: return "synchronization failed";
                case OTPR_CORE_SIGN_EMPTY: return "signature is required";
                case OTPR_CORE_SIGN_DATA_EMPTY: return "signing data is required";
                case OTPR_CORE_VERIFY_FAILED: return "Verify the signature of a transaction failed";
                case OTPR_CORE_OTP_API_UNKNOW_ERROR: return "OTP Server API throws an unknow exception";
                case OTPR_CORE_WS_ACCESS_DENIED: return "Access denied.";
                case OTPR_CORE_USER_SOURCE_NOT_SUPPORT: return "User source is NOT supported (by this function)";
                case OTPR_CORE_USER_SOURCE_USER_EXISTS: return "User name is existing in external user source";
                case OTPR_CORE_USER_SOURCE_USER_NOTEXISTS: return "User name does not exist in external user source";
                case OTPR_CORE_COMM_SUIT: return "authentication successful";
                case OTPR_CORE_COMM_FAIL: return "failed";
                case OTPR_CORE_COMM_INVALID_PARAMETER: return "the parameter is invalid";
                case OTPR_CORE_COMM_INVALID_LICENSE: return "the license file is invalid";
                case OTPR_CORE_COMM_EXPIRED_LICENSE: return "the license file is over expiry date";
                case OTPR_CORE_COMM_EXPIRED_LOG4J: return "load log4j file failed";
                case OTPR_CORE_AUTH_SUIT: return "Authentication is successful.";
                case OTPR_CORE_UID_EMPTY: return "the user name is empty";
                case OTPR_CORE_OTP_EMPTY: return "the otp is empty";
                case OTPR_CORE_PIN_EMPTY: return "the user's pin is empty";
                case OTPR_CORE_TSN_EMPTY: return "the token serial number is empty";
                case OTPR_CORE_INVALID_UID: return "the username is invalid";
                case OTPR_CORE_INVALID_OTP: return "the otp is invalid";
                case OTPR_CORE_INVALID_TOKENKEY: return "the token key is invalid";
                case OTPR_CORE_INVALID_AUTHNUM: return "the authentication base number is invalid";
                case OTPR_CORE_NEED_SYNC: return "the token need synchronization";
                case OTPR_CORE_INVALID_TIMEDRIFT: return "the time drift is invalid";
                case OTPR_CORE_INVALID_USERPIN: return "the user's pin is invalid";
                case OTPR_CORE_INVALID_DBUPIN: return "Invalid database user PIN.";
                case OTPR_CORE_ERR_GETUSER: return "get user info failed";
                case OTPR_CORE_ERR_GETTOKEN: return "get token info failed";
                case OTPR_CORE_TOKEN_LOCKED: return "the token is lost";
                case OTPR_CORE_LOGIN_LOCKED: return "the token is locked";
                case OTPR_CORE_PIN_NOTINIT: return "No PIN configured in database.";
                case OTPR_CORE_INVALID_TOKENTYPE: return "the token type is invalid";
                case OTPR_CORE_INVALID_TOKENSN: return "the token serial number is invalid";
                case OTPR_CORE_ERR_GETASSIGNED: return "get token status failed";
                case OTPR_CORE_ERR_GETTOKENNUM: return "get token count failed";
                case OTPR_CORE_TOKEN_BINDED: return "the token for by binding";
                case OTPR_CORE_BIND_EXCEED: return "beyond the maximum number of binding token";
                case OTPR_CORE_NEED_VERIFYPIN: return "PIN is required.";
                case OTPR_CORE_USER_INACTIVE: return "users in the inactivity";
                case OTPR_CORE_AGENT_EMPTY: return "the agent is empty";
                case OTPR_CORE_HOST_EMPTY: return "the host is empty";
                case OTPR_CORE_SHARE_KEY_EMPTY: return "the share key is empty";
                case OTPR_CORE_INVALID_TOKEN_TYPE: return "invalid token type";
                case OTPR_CORE_TOKEN_NOTENABLED: return "the token is not enabled";
                case OTPR_CORE_TOKEN_LOGOUT: return "the token is log off";
                //case OTPR_CORE_DB_SUIT                          : return "unknown";
                case OTPR_CORE_DB_INVALID_CONFIGFILE: return "error config file";
                case OTPR_CORE_DB_INVALID_DBTYPE: return "error database type";
                case OTPR_CORE_DB_ERROR_CONNECT: return "error connection";
                case OTPR_CORE_DB_DATASOURCE_NOTFOUND: return "can not find data source or driver";
                case OTPR_CORE_DB_DATABASE_NOTFOUND: return "can not find database";
                case OTPR_CORE_DB_SERVER_NOTFOUND: return "can not connect to server or access denied";
                case OTPR_CORE_DB_INVALID_AUTH: return "the username or password is wrong,login failed";
                case OTPR_CORE_DB_NOTBEUSED_CONNECT: return "no available connections";
                case OTPR_CORE_DB_USER_EXISTS: return "users already exists";
                case OTPR_CORE_DB_USER_NOTEXISTS: return "User does not exist";
                case OTPR_CORE_DB_RECORD_NOTEXISTS: return "record does not exist";
                case OTPR_CORE_DB_TOKEN_EXISTS: return "token already exist";
                case OTPR_CORE_DB_TOKEN_NOTEXISTS: return "token does not exist";
                case OTPR_CORE_DB_AGENT_EXISTS: return "agent already exist";
                case OTPR_CORE_DB_AGENT_NOTEXISTS: return "agent does not exist";
                case OTPR_CORE_DB_HOST_EXISTS: return "server already exist";
                case OTPR_CORE_DB_HOST_NOTEXISTS: return "server does not exist";
                case OTPR_CORE_DB_ADD_USER_FAILED: return "add user info failed";
                case OTPR_CORE_DB_DEL_USER_FAILED: return "delete user info failed";
                case OTPR_CORE_DB_QUERY_USER_FAILED: return "query user info failed";
                case OTPR_CORE_DB_UPDATE_USER_FAILED: return "change user info failed";
                case OTPR_CORE_DB_ADD_TOKEN_FAILED: return "add token info failed";
                case OTPR_CORE_DB_DEL_TOKEN_FAILED: return "delete token info failed";
                case OTPR_CORE_DB_QUERY_TOKEN_FAILED: return "query token info failed";
                case OTPR_CORE_DB_UPDATE_TOKEN_FAILED: return "change token info failed";
                case OTPR_CORE_DB_BIND_USER_TOKEN_FAILED: return "bind user token failed";
                case OTPR_CORE_DB_UNBIND_USER_TOKEN_FAILED: return "unbind user token failed";
                case OTPR_CORE_DB_USER_TOKEN_UNBIND: return "the token unbind by user";
                case OTPR_CORE_DB_ADD_AGENT_FAILED: return "add agent info failed";
                case OTPR_CORE_DB_DEL_AGENT_FAILED: return "delete agent info failed";
                case OTPR_CORE_DB_CHANGE_AGENT_FAILED: return "change agent info failed";
                case OTPR_CORE_DB_QUERY_AGENT_FAILED: return "get agent info failed";
                case OTPR_CORE_DB_ADD_HOST_FAILED: return "add host info failed";
                case OTPR_CORE_DB_DEL_HOST_FAILED: return "delete host info failed";
                case OTPR_CORE_DB_CHANGE_HOST_FAILED: return "change host info failed";
                case OTPR_CORE_DB_QUERY_HOST_FAILED: return "get host info failed";
                case OTPR_CORE_DB_ADD_AGENT_HOST_FAILED: return "add agent host map info failed";
                case OTPR_CORE_DB_DEL_AGENT_HOST_FAILED: return "delete agent host map info failed";
                case OTPR_CORE_DB_CHANGE_AGENT_HOST_FAILED: return "change agent host map info failed";
                case OTPR_CORE_DB_QUERY_AGENT_HOST_FAILED: return "get agent host map info failed";
                case OTPR_CORE_DB_ADD_LOG_FAILED: return "add log info failed";
                case OTPR_CORE_DB_DEL_LOG_FAILED: return "delete log info failed";
                case OTPR_CORE_DB_CHANGE_LOG_FAILED: return "change log info failed";
                case OTPR_CORE_DB_QUERY_LOG_FAILED: return "get log info failed";
                case OTPR_CORE_DB_ADD_CONFIG_FAILED: return "add config info failed";
                case OTPR_CORE_DB_DEL_CONFIG_FAILED: return "delete config info failed";
                case OTPR_CORE_DB_CHANGE_CONFIG_FAILED: return "change config info failed";
                case OTPR_CORE_DB_QUERY_CONFIG_FAILED: return "get config info failed";
                case OTPR_CORE_DB_ADD_ADMIN_GROUP_FAILED: return "add admin group info failed";
                case OTPR_CORE_DB_DEL_ADMIN_GROUP_FAILED: return "delete admin group info failed";
                case OTPR_CORE_DB_CHANGE_ADMIN_GROUP_FAILED: return "change admin group info failed";
                case OTPR_CORE_DB_QUERY_ADMIN_GROUP_FAILED: return "get admin group info failed";
                case OTPR_CORE_DB_ADD_ADMIN_LOG_FAILED: return "add admin log info failed";
                case OTPR_CORE_DB_DEL_ADMIN_LOG_FAILED: return "delete admin log info failed";
                case OTPR_CORE_DB_CHANGE_ADMIN_LOG_FAILED: return "change admin log info failed";
                case OTPR_CORE_DB_QUERY_ADMIN_LOG_FAILED: return "get admin log info failed";
                case OTPR_CORE_DB_ADD_ADMIN_USER_FAILED: return "add admin user info failed";
                case OTPR_CORE_DB_DEL_ADMIN_USER_FAILED: return "delete admin user info failed";
                case OTPR_CORE_DB_CHANGE_ADMIN_USER_FAILED: return "change admin user info failed";
                case OTPR_CORE_DB_QUERY_ADMIN_USER_FAILED: return "get admin user info failed";
                case OTPR_CORE_DB_ADD_DOMAIN_FAILED: return "add domain info failed";
                case OTPR_CORE_DB_DEL_DOMAIN_FAILED: return "delete domain info failed";
                case OTPR_CORE_DB_CHANGE_DOMAIN_FAILED: return "change domain info failed";
                case OTPR_CORE_DB_QUERY_DOMAIN_FAILED: return "get domain info failed";
                case OTPR_CORE_DB_ADD_TEMP_USER_FAILED: return "add temp user failed";
                case OTPR_CORE_DB_DEL_TEMP_USER_FAILED: return "delete temp user failed";
                case OTPR_CORE_DB_CHANGE_TEMP_USER_FAILED: return "change temp user failed";
                case OTPR_CORE_DB_QUERY_TEMP_USER_FAILED: return "get temp user failed";
                case OTPR_CORE_DB_RELOAD_CONNECTION_FAILED: return "reload connection failed";
                case OTPR_CORE_DB_ADD_APPGROUP_FAILED: return "Failed to add application group.";
                case OTPR_CORE_DB_DEL_APPGROUP_FAILED: return "Failed to deleteapplication group.";
                case OTPR_CORE_DB_CHANGE_APPGROUP_FAILED: return "Failed to update application group.";
                case OTPR_CORE_DB_QUERY_APPGROUP_FAILED: return "Failed to get application group.";
                case OTPR_CORE_DB_ADD_USER_GROUP_FAILED: return "Failed to add userand application group.";
                case OTPR_CORE_DB_DEL_USER_GROUP_FAILED: return "Failed to deleteuser and application group.";
                case OTPR_CORE_DB_CHANGE_USER_GROUP_FAILED: return "Failed to updateuser and application group.";
                case OTPR_CORE_DB_QUERY_USER_GROUP_FAILED: return "Failed to get user and application group.";
                case OTPR_CORE_DB_ADD_ZONE_FAILED: return "Failed to add zone.";
                case OTPR_CORE_DB_DEL_ZONE_FAILED: return "Failed to delete zone.";
                case OTPR_CORE_DB_CHANGE_ZONE_FAILED: return "Failed to update zone.";
                case OTPR_CORE_DB_QUERY_ZONE_FAILED: return "Failed to get zone.";
                case OTPR_CORE_DB_ADD_LICENSE_FAILED: return "Failed to add license.";
                case OTPR_CORE_DB_DEL_LICENSE_FAILED: return "Failed to delete license.";
                case OTPR_CORE_DB_CHANGE_LICENSE_FAILED: return "Failed to update license.";
                case OTPR_CORE_DB_QUERY_LICENSE_FAILED: return "Failed to get license.";
                case OTPR_CORE_DB_CLEANUP_USERDATA_FAILED: return "clean up user datas failed";
                case OTPR_CORE_DB_ADD_TOKEN_EXT_FAILED: return "add token extension info failed";
                case OTPR_CORE_DB_QUERY_TOKEN_EXT_FAILED: return "query token extension info failed";
                case OTPR_CORE_DB_CHANGE_TOKEN_EXT_FAILED: return "update token extension info failed";
                case OTPR_CORE_DB_LDAP_INIT_FAILED: return "init ldap failed";
                case OTPR_CORE_SUCCESS: return "Error code:success/Return value: false";
                case OTPR_CORE_FAILED: return "Error code:failed/Return value: true";
            }
            return null;
        }

        public static String getReturnCodeInfo(int nCode)
        {
            uint unCode = (uint)nCode;
            return getReturnCodeInfo(unCode);
        }

        public static String errorCode2Str(uint nErrorCode)
        {
            return "0x" + nErrorCode.ToString("X");
        }

        private static String getReturnCodeInfo(uint nCode)
        {
            String strRet = "[" + errorCode2Str(nCode) + "] ";
            String strMsg = OTPCore_getErrorInfo(nCode);

            if (strMsg == null)
            {
                if ((nCode & 0x7FFFFFFF) <= (OTPR_CORE_WS_SUIT & 0x7FFFFFFF))
                {
                    strMsg = "(Return value of some functions, check function details for return value)";
                }
                else
                {
                    strMsg = "Uknown error";
                }
            }

            return strRet + strMsg;
        }
    }
}
