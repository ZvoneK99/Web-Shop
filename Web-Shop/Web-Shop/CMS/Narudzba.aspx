<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Narudzba.aspx.vb" Inherits="igre_ba.Narudzba" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print narudzbe</title>
    <script type="text/javascript">
        window.print();
    </script>
    <style type="text/css" media="print">
        @page {
            size: auto; /* auto is the initial value */
            margin: 1mm; /* this affects the margin in the printer settings */
            /*size: landscape;*/
        }

        /* A4 210x297 mm */
        @media print and (min-height: 179mm) and (max-height: 210mm) {
            .img_port {
                height: 167mm !important;
            }

            .noprint {
                display: none;
            }
        }

        body {
            font-size: 12px;
            width: 220mm;
            background-color: #FFFFFF;
            /*border: 1px dotted #ff0000;*/
            margin: 20px; /* this affects the margin on the content before sending to printer */
        }

        .tblZaglavlje {
            width: 220mm;
            /*border: 1px solid #000;*/
        }

        .divTbl {
            position: relative;
            width: 220mm;
        }

            .divTbl .kupac {
                width: 300px;
                height: 100px;
                border: 1px solid #000;
                padding: 10px;
                float: left;
            }

            .divTbl .prodavac {
                width: 300px;
                height: 100px;
                /*border: 1px solid #000;*/
                padding: 10px;
                float: right;
            }

        .tblStavke {
            width: 220mm;
            border-collapse: collapse;
        }

            .tblStavke tr th {
                background-color: #dfdfdf;
            }

            .tblStavke tr td {
                text-align: center;
            }

        .centar {
            text-align: center;
        }

        .istaknuto {
            font-size: 20px;
            font-weight: 600;
        }
    </style>
    <%--<style type="text/css">
        #bill {
            width:50%;
            margin:150px auto 0px auto;
            text-align:center;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
        <%=PrukaziPodatke()%>
        <%--<div id="bill">
            <h1>U PRIPREMI</h1>
            <button type="button" onclick="javascript:window.close()">Zatvori prozor</button>
        </div>--%>
    </form>
</body>
</html>
