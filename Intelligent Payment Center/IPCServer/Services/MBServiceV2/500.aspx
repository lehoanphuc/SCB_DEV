<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="500.aspx.cs" Inherits="MBService._500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <style type="text/css">
        @font-face {
            font-family: "Arial MT Std Extra Bold Cond";
            src: url("fonts/Arial_MT_Std_Extra_Bold_Cond.eot"); /* IE9*/
            src: url("fonts/Arial_MT_Std_Extra_Bold_Cond.eot?#iefix") format("embedded-opentype"), /* IE6-IE8 */
                 url("fonts/Arial_MT_Std_Extra_Bold_Cond.woff2") format("woff2"), /* chrome、firefox */
                 url("fonts/Arial_MT_Std_Extra_Bold_Cond.ttf") format("truetype"); /* chrome、firefox、opera、Safari, Android, iOS 4.2+*/
        }
        html, body {
            font-family: "Arial MT Std Extra Bold Cond";
        }
        body {
            background: #3f3fb3;
            height: 100vh;
            margin: 0;
            color: white;
            text-align: center;
        }

        h1 {
            margin: .8em 3rem;
            font: 50px Roboto;
        }

        h5 {
            margin: .2em 3rem;
            font: 30px Roboto;
            padding-bottom: 50px;
        }

        .btn {
            border-radius: 300px;
            text-transform: uppercase;
            font-size: 3vh;
            padding: 1.5vh 6vh;
            font-weight: bold;
            text-align: center;
            white-space: nowrap;
            vertical-align: middle;
            cursor: pointer;
            border: 1px solid transparent;
            line-height: 30px;
            border-color: #0e0265;
            color: #fff;
            background-color: #0e0265;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Unexpected Error <b>:(</b></h1>
        <h5>Something went wrong</h5>
        <button id="buttonclick" class="btn" type="button" onclick="invokeCSCode()">Back to Home</button>

        <script type="text/javascript">
            function invokeCSCode() {
                var data = { event: 'CLICK', data: '' };
                try {
                    invokeCSharpAction(JSON.stringify(data));
                }
                catch (err) {
                    invokeCSharpActionAnd(JSON.stringify(data));
                }
            }
            function invokeCSharpActionAnd(data) { jsBridge.invokeAction(data); };

        </script>
    </form>
</body>
</html>
